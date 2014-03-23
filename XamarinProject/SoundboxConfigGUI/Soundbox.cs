using System;
using System.IO.Ports;
using System.Diagnostics;

namespace SoundboxConfigGUI
{
	public class Soundbox
	{
		public enum Command : byte {
			Handshake = 0x00,
			Handshake2 = 0x01
		}

		enum State {
			Disconnected,
			HandshakePending,
			Ready
		}

		SerialPort serial;
		State state = State.Disconnected;

		public Soundbox ()
		{
			serial = new SerialPort ("COM1", 9600, Parity.Even, 8, StopBits.One);
			serial.DataReceived += DataReceived;
		}

		public void Connect() {
			serial.Open ();
			state = State.HandshakePending;
			SendCommand (Command.Handshake);
		}

		public void Disconnect() {
			state = State.Disconnected;
			serial.Close ();
		}

		public void SendCommand(Command c, byte[] payload = null) {

			if (payload == null) {
				payload = new byte[] {};
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
			Trace.WriteLine ("Data received: " + data);

			byte[] message = GetBytes(data);
			byte command = message[0];

			switch (command) {
			case (byte) Command.Handshake2:
				if (state == State.HandshakePending) {
					state = State.Ready;
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

