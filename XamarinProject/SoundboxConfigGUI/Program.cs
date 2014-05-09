using System;
using Gtk;

namespace SoundboxConfigGUI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			try {
				Application.Run ();
			}
			catch(System.Exception e) {
				System.Diagnostics.Trace.WriteLine (e.Message);
			}
		}
	}
}
