using System;
using System.Diagnostics;
using Gtk;
using SoundboxConfigGUI;
using System.Timers;

public partial class MainWindow: Gtk.Window
{
	public static void SetStatusText(string text) {
		instance.statusLabel.LabelProp = text;
	}

	public static void ConnectionEstablished() {
		MainWindow.Log ("Connection to Soundbox established.");
		SetStatusText("Connected");
		EndProgress ();
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
		//instance.logLabel.Text += message + "\n";
	}

	static MainWindow instance;
	Soundbox soundbox;
	Timer pulseTimer;
	string logText = "";

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		instance = this;
		soundbox = new Soundbox ();
		PopulatePortList ();
		Log ("Application started");

		pulseTimer = new Timer (50);
		pulseTimer.Elapsed += OnProgressIncrement;
		pulseTimer.Enabled = false;
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
			Log ("No available COM ports detected, plug in UART at press refresh.");
		} else {
			foreach (string p in ports) {
				comPortNames.AppendValues (p);
			}
			portList.Sensitive = true;
		}

		portList.Model = comPortNames;
		portList.Active = 0;
	}

	protected void Connect (object sender, EventArgs e)
	{
		if (soundbox.Connect ()) {
			Log ("Connecting...");
			SetStatusText ("Connecting...");
			StartProgress ();
		}
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
}
