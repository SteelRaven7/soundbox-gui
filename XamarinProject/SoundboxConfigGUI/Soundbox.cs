using System;
using System.IO.Ports;
using System.Diagnostics;

namespace SoundboxConfigGUI
{
	public class Soundbox
	{
		public enum Command : byte {
			Handshake = 0x00,
			Handshake2 = 0x01,
			Test = 0x00,
		}

		enum State {
			Disconnected,
			HandshakePending,
			Ready
		}

		SerialPort serial = null;
		State state = State.Disconnected;

		public Soundbox () {

		}

		public void SetPort(string portName) {
			Trace.WriteLine ("SetPort: " + portName);
			MainWindow.Log ("Setting port to " + portName);
			serial = new SerialPort(portName, 9600, Parity.Even, 8, StopBits.One);
			serial.DataReceived += DataReceived;
		}

		public void DisablePort() {
			if (HasSerialPort ()) {
				if (serial.IsOpen) {
					serial.Close ();
				}
				serial = null;
			}
		}

		public bool Connect() {
			if (!HasSerialPort ()) {
				MainWindow.SetStatusText ("No port is available");
				MainWindow.Log ("Cannot connect: No COM port is available.");
				return false;
			}

			try {
				if (!serial.IsOpen) {
					serial.Open ();
				}
			}
			catch(Exception e) {
				Trace.WriteLine (e.Message);
				DisablePort();
				return false;
			}

			state = State.HandshakePending;
			MainWindow.Log ("Sending handshake...");
			SendCommand (Command.Handshake);
			//SendCommand (Command.Test, new byte[] {0xaa, 0xaa});

			return true;
		}

		public void Disconnect() {
			state = State.Disconnected;
			serial.Close ();
		}

		public void SendCommand(Command c, byte[] payload = null) {

			if (payload == null) {
				payload = new byte[] {0x00, 0x00};
			}

#if DEBUG
			Trace.Write ("Sending command " + c + ", payload: ");

			foreach(byte p in payload) {
				Trace.Write(p+", ");
			}
			Trace.WriteLine("");
#endif

			byte[] message = new byte[payload.Length + 1];

			message [0] = (byte)c;
			payload.CopyTo (message, 1);

			serial.Write (message, 0, message.Length);
		}

		public void DataReceived(object sender, SerialDataReceivedEventArgs e) {
			string data = serial.ReadExisting ();

			byte[] message = GetBytes(data);
			byte command = message[0];
			Trace.WriteLine ("Data received: " + data + " interpreted as command " + command);

			switch (command) {
			case (byte) Command.Handshake2:
				MainWindow.Log ("Handshake received.");

				if (state == State.HandshakePending) {
					state = State.Ready;
					MainWindow.ConnectionEstablished ();
				} else {
					UnknownState ();
				}
				break;
			default:
				Trace.WriteLine ("Unrecognized command: " + command);
				UnknownState ();
				break;
			}
		}

		public bool HasSerialPort() {
			return serial != null;
		}

		public bool Connected() {
			return HasSerialPort() && serial.IsOpen && state != State.Disconnected;
		}

		void UnknownState() {
			Trace.WriteLine ("Soundbox is in an unknown state, disconnecting...");
			Disconnect();
		}

		static byte[] GetBytes(string str) {
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}
	}
}

