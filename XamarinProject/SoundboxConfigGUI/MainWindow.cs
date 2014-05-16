using System;
using System.Collections.Generic;
using System.Diagnostics;
using Gtk;
using SoundboxConfigGUI;
using System.Timers;

public partial class MainWindow: Gtk.Window
{
	bool swedishFloatStrings;

	public static void SetStatusText(string text) {
		instance.statusLabel.LabelProp = text;
	}

	public static void ClearDone() {
		MainWindow.Log ("Flash memory cleared.");
	}

	public static void StartProgress(bool useAbsoluteProgress = false) {
		instance.pulseTimer.Enabled = true;
	}

	public static void SetProgress(float progress) {
		instance.progressbar.Fraction = progress;
	}

	public static void EndProgress() {
		instance.pulseTimer.Enabled = false;
	}

	public static void Log(string message) {
		instance.log.Buffer.Text += message + "\n";
	}

	public static void AddValue(ConfigurationValue v) {
		instance.configurationValues.Add (v);
	}

	static MainWindow instance;
	Soundbox soundbox;
	Timer pulseTimer;
	List<ConfigurationValue> configurationValues;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		instance = this;
		configurationValues = new List<ConfigurationValue> ();
		soundbox = new Soundbox ();
		Log ("Application started");
		PopulatePortList ();

		pulseTimer = new Timer (50);
		pulseTimer.Elapsed += OnProgressIncrement;
		pulseTimer.Enabled = false;

		swedishFloatStrings = (0.1f).ToString () [1] == ',';

		ConfigurationValue v = new ConfigurationValue (1);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	void PopulatePortList() {
		string[] ports = System.IO.Ports.SerialPort.GetPortNames ();

		ListStore comPortNames = new ListStore (typeof(string));

		if (ports.Length == 0) {
			comPortNames.AppendValues ("No COM ports available");
			portList.Sensitive = false;
			Log ("No available COM ports detected, plug in UART and press refresh.");
		} else {
			foreach (string p in ports) {
				comPortNames.AppendValues (p);
			}
			portList.Sensitive = true;
		}

		portList.Model = comPortNames;
		portList.Active = 0;

		if (ports.Length != 0) {
			soundbox.SetPort (portList.ActiveText);
		}
	}

	protected void Program (object sender, EventArgs e)
	{
		Log ("Listing coefficients...");
		PopulateValueList ();

		soundbox.SetValues (configurationValues);

		if (soundbox.Connect ()) {
			SetStatusText ("Clearing flash memory");
			// We don't need no animations
			//StartProgress ();
		}
	}

	protected void Connect (object sender, EventArgs e)
	{
		// Unused
	}

	protected void Disconnect (object sender, EventArgs e)
	{
		SetStatusText("Not connected");
	}

	protected void RefreshPorts (object sender, EventArgs e)
	{
		PopulatePortList ();
	}

	protected void PortChanged (object sender, EventArgs e)
	{
		if (portList.Sensitive) {
			soundbox.SetPort (portList.ActiveText);
		} else {
			soundbox.DisablePort ();
		}
	}

	void OnProgressIncrement(object source, ElapsedEventArgs e) {
		progressbar.Pulse ();
	}

	public string FullStopsToCommas(string s) {
		if(swedishFloatStrings) {
			return s.Replace ('.', ',');
		}

		return s;
	}

	float ParseFloat(Entry e, float min = -9999f, float max = 9999f) {
		float f = 0;
		try {
			f = float.Parse(FullStopsToCommas(e.Text));
		} catch(Exception ex) {
			MainWindow.Log(e.Name+": Wrong format");
			Trace.WriteLine (ex);
			return 0f;
		}

		MainWindow.Log (e.Name + ": " + f);

		if(f > max) {
			f = max;
			MainWindow.Log (e.Name + " is more than " + max);
		}
		else if(f < min) {
			f = min;
			MainWindow.Log (e.Name + " is less than " + min);
		}

		return f;
	}

	int ParseInt(Entry e, int min = -9999, int max = 9999) {
		return (int)ParseFloat (e, min, max);
	}

	void PopulateValueList() {
		configurationValues.Clear ();

		EchoValues ();
		FlangerValues ();
		ReverbValues ();
		DistortionValues ();
		BypassValues ();
	}

	void BypassValues() {
		if (Equalizercheckbox.Active) {
			AddValue (new CUnsignedInteger (12, 1));
		} else {
			AddValue (new CUnsignedInteger (12, 0));
		}

		if (Echocheckbox.Active) {
			AddValue (new CUnsignedInteger (13, 1));
		} else {
			AddValue (new CUnsignedInteger (13, 0));
		}

		if (Flangercheckbox.Active) {
			AddValue (new CUnsignedInteger (14, 1));
		} else {
			AddValue (new CUnsignedInteger (14, 0));
		}

		if (Reverbcheckbox.Active) {
			AddValue (new CUnsignedInteger (15, 1));
		} else {
			AddValue (new CUnsignedInteger (15, 0));
		}

		if (Distortioncheckbox.Active) {
			AddValue (new CUnsignedInteger (16, 1));
		} else {
			AddValue (new CUnsignedInteger (16, 0));
		}

	}

	void EchoValues() {
		float delay = ParseFloat (echoDelay, 0f, 1.45f);
		AddValue (new CUnsignedInteger (1, (UInt16)(delay * 44100)));

		AddValue (new CFixed (2, ParseFloat (echoFeedback, -1f, 0.9999f), 15, true));
		AddValue (new CFixed (3, ParseFloat (echoDryGain, -1f, 0.9999f), 15, true));
		AddValue (new CFixed (4, ParseFloat (echoWetGain, -1f, 0.9999f), 15, true));
	}

	void FlangerValues() {
		float delay = ParseFloat (flangerDelay, 10, 45);
		AddValue (new CUnsignedInteger (5, (UInt16)(delay * 44f)));
		AddValue (new CUnsignedInteger (6, (UInt16)ParseInt (flangerSweep, 500, 2500)));
	}

	void ReverbValues() {


		int reverb1 = ParseInt (reverbDelay1); 
		reverb1 = GetLastPrime (reverb1);

		reverbDelay1.Text = ""+reverb1;

		int reverb2 = ParseInt (reverbDelay2); 
		reverb2 = GetLastPrime (reverb2);

		reverbDelay2.Text = ""+reverb2;

		int reverb3 = ParseInt (reverbDelay3); 
		reverb3 = GetLastPrime (reverb3);

		reverbDelay3.Text = ""+reverb3;

		int reverb4 = ParseInt (reverbDelay4); 
		reverb4 = GetLastPrime (reverb4);

		reverbDelay4.Text = ""+reverb4;

		AddValue (new CUnsignedInteger (7, (UInt16) reverb1)); // Check values
		AddValue (new CUnsignedInteger (8, (UInt16) reverb2)); // Check values
		AddValue (new CUnsignedInteger (9, (UInt16) reverb3)); // Check values
		AddValue (new CUnsignedInteger (10, (UInt16) reverb4)); // Check values
		// AddValue (new CFixed (11, ParseFloat (reverbDryGain, -1f, 1f), 15, true));
		// AddValue (new CFixed (12, ParseFloat (reverbWetGain, -1f, 1f), 15, true));

	}

	void DistortionValues() {
		float distortion = ParseFloat (distortionCutoff);
		AddValue (new CUnsignedInteger (11, (UInt16)(distortion * (((2^15)-1)/100)))); // 2^16/100 = 655.36 : ((2^15)-1)/100 = 327.67
	}


	int GetLastPrime(int v) {
		for (int i = 0; i < primeNumbers.Length; i++) {
			if (v < primeNumbers[i]) {
				return primeNumbers [i];
			}
		}

		return 4093;
	}

	int[] primeNumbers = new int[] {
		101,
		103,
		107,
		109,
		113,
		127,
		131,
		137,
		139,
		149,
		151,
		157,
		163,
		167,
		173,
		179,
		181,
		191,
		193,
		197,
		199,
		211,
		223,
		227,
		229,
		233,
		239,
		241,
		251,
		257,
		263,
		269,
		271,
		277,
		281,
		283,
		293,
		307,
		311,
		313,
		317,
		331,
		337,
		347,
		349,
		353,
		359,
		367,
		373,
		379,
		383,
		389,
		397,
		401,
		409,
		419,
		421,
		431,
		433,
		439,
		443,
		449,
		457,
		461,
		463,
		467,
		479,
		487,
		491,
		499,
		503,
		509,
		521,
		523,
		541,
		547,
		557,
		563,
		569,
		571,
		577,
		587,
		593,
		599,
		601,
		607,
		613,
		617,
		619,
		631,
		641,
		643,
		647,
		653,
		659,
		661,
		673,
		677,
		683,
		691,
		701,
		709,
		719,
		727,
		733,
		739,
		743,
		751,
		757,
		761,
		769,
		773,
		787,
		797,
		809,
		811,
		821,
		823,
		827,
		829,
		839,
		853,
		857,
		859,
		863,
		877,
		881,
		883,
		887,
		907,
		911,
		919,
		929,
		937,
		941,
		947,
		953,
		967,
		971,
		977,
		983,
		991,
		997,
		1009,
		1013,
		1019,
		1021,
		1031,
		1033,
		1039,
		1049,
		1051,
		1061,
		1063,
		1069,
		1087,
		1091,
		1093,
		1097,
		1103,
		1109,
		1117,
		1123,
		1129,
		1151,
		1153,
		1163,
		1171,
		1181,
		1187,
		1193,
		1201,
		1213,
		1217,
		1223,
		1229,
		1231,
		1237,
		1249,
		1259,
		1277,
		1279,
		1283,
		1289,
		1291,
		1297,
		1301,
		1303,
		1307,
		1319,
		1321,
		1327,
		1361,
		1367,
		1373,
		1381,
		1399,
		1409,
		1423,
		1427,
		1429,
		1433,
		1439,
		1447,
		1451,
		1453,
		1459,
		1471,
		1481,
		1483,
		1487,
		1489,
		1493,
		1499,
		1511,
		1523,
		1531,
		1543,
		1549,
		1553,
		1559,
		1567,
		1571,
		1579,
		1583,
		1597,
		1601,
		1607,
		1609,
		1613,
		1619,
		1621,
		1627,
		1637,
		1657,
		1663,
		1667,
		1669,
		1693,
		1697,
		1699,
		1709,
		1721,
		1723,
		1733,
		1741,
		1747,
		1753,
		1759,
		1777,
		1783,
		1787,
		1789,
		1801,
		1811,
		1823,
		1831,
		1847,
		1861,
		1867,
		1871,
		1873,
		1877,
		1879,
		1889,
		1901,
		1907,
		1913,
		1931,
		1933,
		1949,
		1951,
		1973,
		1979,
		1987,
		1993,
		1997,
		1999,
		2003,
		2011,
		2017,
		2027,
		2029,
		2039,
		2053,
		2063,
		2069,
		2081,
		2083,
		2087,
		2089,
		2099,
		2111,
		2113,
		2129,
		2131,
		2137,
		2141,
		2143,
		2153,
		2161,
		2179,
		2203,
		2207,
		2213,
		2221,
		2237,
		2239,
		2243,
		2251,
		2267,
		2269,
		2273,
		2281,
		2287,
		2293,
		2297,
		2309,
		2311,
		2333,
		2339,
		2341,
		2347,
		2351,
		2357,
		2371,
		2377,
		2381,
		2383,
		2389,
		2393,
		2399,
		2411,
		2417,
		2423,
		2437,
		2441,
		2447,
		2459,
		2467,
		2473,
		2477,
		2503,
		2521,
		2531,
		2539,
		2543,
		2549,
		2551,
		2557,
		2579,
		2591,
		2593,
		2609,
		2617,
		2621,
		2633,
		2647,
		2657,
		2659,
		2663,
		2671,
		2677,
		2683,
		2687,
		2689,
		2693,
		2699,
		2707,
		2711,
		2713,
		2719,
		2729,
		2731,
		2741,
		2749,
		2753,
		2767,
		2777,
		2789,
		2791,
		2797,
		2801,
		2803,
		2819,
		2833,
		2837,
		2843,
		2851,
		2857,
		2861,
		2879,
		2887,
		2897,
		2903,
		2909,
		2917,
		2927,
		2939,
		2953,
		2957,
		2963,
		2969,
		2971,
		2999,
		3001,
		3011,
		3019,
		3023,
		3037,
		3041,
		3049,
		3061,
		3067,
		3079,
		3083,
		3089,
		3109,
		3119,
		3121,
		3137,
		3163,
		3167,
		3169,
		3181,
		3187,
		3191,
		3203,
		3209,
		3217,
		3221,
		3229,
		3251,
		3253,
		3257,
		3259,
		3271,
		3299,
		3301,
		3307,
		3313,
		3319,
		3323,
		3329,
		3331,
		3343,
		3347,
		3359,
		3361,
		3371,
		3373,
		3389,
		3391,
		3407,
		3413,
		3433,
		3449,
		3457,
		3461,
		3463,
		3467,
		3469,
		3491,
		3499,
		3511,
		3517,
		3527,
		3529,
		3533,
		3539,
		3541,
		3547,
		3557,
		3559,
		3571,
		3581,
		3583,
		3593,
		3607,
		3613,
		3617,
		3623,
		3631,
		3637,
		3643,
		3659,
		3671,
		3673,
		3677,
		3691,
		3697,
		3701,
		3709,
		3719,
		3727,
		3733,
		3739,
		3761,
		3767,
		3769,
		3779,
		3793,
		3797,
		3803,
		3821,
		3823,
		3833,
		3847,
		3851,
		3853,
		3863,
		3877,
		3881,
		3889,
		3907,
		3911,
		3917,
		3919,
		3923,
		3929,
		3931,
		3943,
		3947,
		3967,
		3989,
		4001,
		4003,
		4007,
		4013,
		4019,
		4021,
		4027,
		4049,
		4051,
		4057,
		4073,
		4079,
		4091,
		4093
	};
}
