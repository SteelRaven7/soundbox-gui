using System;
using System.Collections.Generic;
using System.Diagnostics;
using Gtk;
using SoundboxConfigGUI;
using System.Timers;

public partial class MainWindow: Gtk.Window
{
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
	}

	protected void Program (object sender, EventArgs e)
	{
		Log ("Listing coefficients...");
		PopulateValueList ();

		soundbox.SetValues (configurationValues);

		if (soundbox.Connect ()) {
			SetStatusText ("Clearing flash memory");
			StartProgress ();
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
		return s.Replace ('.', ',');
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

		/*for(int i = 1; i < 1000; i++) {
			AddValue (new CUnsignedInteger ((UInt16)i, (UInt16)(1100-i)));
		}*/

		EchoValues ();
		FlangerValues ();
		ReverbValues ();
		DistortionValues ();
	}

	void EchoValues() {
		float delay = ParseFloat (echoDelay);
		AddValue (new CUnsignedInteger (1, (UInt16)(delay * 44100)));

		AddValue (new CFixed (2, ParseFloat (echoFeedback, -1f, 1f), 15, true));
		AddValue (new CFixed (3, ParseFloat (echoDryGain, -1f, 1f), 15, true));
		AddValue (new CFixed (4, ParseFloat (echoWetGain, -1f, 1f), 15, true));
	}

	void FlangerValues() {
		float delay = ParseFloat (flangerDelay, 10, 45);
		AddValue (new CUnsignedInteger (5, (UInt16)(delay * 44.1f)));
		AddValue (new CUnsignedInteger (6, (UInt16)ParseInt (flangerSweep, 500, 2500)));
	}

	void ReverbValues() {
		AddValue (new CUnsignedInteger (7, (UInt16)ParseInt (reverbDelay1, 0, 1000))); // Check values
		AddValue (new CUnsignedInteger (8, (UInt16)ParseInt (reverbDelay2, 0, 1000))); // Check values
		AddValue (new CUnsignedInteger (9, (UInt16)ParseInt (reverbDelay3, 0, 1000))); // Check values
		AddValue (new CUnsignedInteger (10, (UInt16)ParseInt (reverbDelay4, 0, 1000))); // Check values
		AddValue (new CFixed (11, ParseFloat (reverbDryGain, -1f, 1f), 15, true));
		AddValue (new CFixed (12, ParseFloat (reverbWetGain, -1f, 1f), 15, true));

	}

	void DistortionValues() {
		float distortion = ParseFloat (distortionCutoff);
		AddValue (new CUnsignedInteger (13, (UInt16)(distortion * 0.1526))); // This needs revising. (100/2^16)*100 = 0.1526
	}
}
