using System;
using Gtk;
using SoundboxConfigGUI;

public partial class MainWindow: Gtk.Window
{
	Soundbox soundbox;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		soundbox = new Soundbox ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	protected void Connect (object sender, EventArgs e)
	{
		soundbox.Connect ();
		statusLabel.LabelProp = "Connected";
		progressbar.Pulse ();
	}		
	protected void Disconnect (object sender, EventArgs e)
	{
		statusLabel.LabelProp = "Not connected";
	}
}
