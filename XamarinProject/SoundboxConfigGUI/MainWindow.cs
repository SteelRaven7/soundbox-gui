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
		instance.WriteConfiguration ();
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

	void WriteConfiguration() {
		Log ("Writing configuration.");
		SetStatusText ("Writing configuration");

		foreach (ConfigurationValue value in configurationValues) {
			soundbox.SendConfigurationValue(value);
		}
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

	protected void DelayChanged (object sender, EventArgs e)
	{
		Entry delay = (Entry) sender;

		float delayTime;
		try {
			delayTime = float.Parse(FullStopsToCommas(delay.Text));
		} catch(Exception ex) {
			MainWindow.Log("Echo delay: Wrong format");
			return;
		}

		if(delayTime > 1.5f) {
			MainWindow.Log("Echo delay is more than 1.5 seconds");
		}
		else if(delayTime < 0f) {
			MainWindow.Log("Echo delay is less than 0 seconds");
			delayTime = 0f;
		}

		if(delayTime > 1.48f) {
			delayTime = 1.48f;
		}

		UInt16 samples = (UInt16) (delayTime*44100);

		Trace.WriteLine("Samples: " + samples);

		CUnsignedInteger value = new CUnsignedInteger(1, samples);
		AddValue(value);
	}
}
