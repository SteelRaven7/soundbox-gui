﻿using System;
using System.IO.Ports;
using System.Diagnostics;
using System.Collections.Generic;

namespace SoundboxConfigGUI
{
	public class Soundbox
	{
		byte readyByte = (byte) '?'; // YOLO

		SerialPort serial = null;

		public Soundbox () {

		}

		List<ConfigurationValue> configurationValues;

		public void SetValues(List<ConfigurationValue> configurationValues) {
			this.configurationValues = configurationValues;
		}

		public void SetPort(string portName) {
			Trace.WriteLine ("SetPort: " + portName);
			//MainWindow.Log ("Setting port to " + portName);
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
			//MainWindow.Log ("Connecting to Soundbox...");
			if (!HasSerialPort ()) {
				//MainWindow.SetStatusText ("No port is available");
				//MainWindow.Log ("Cannot connect: No COM port is available.");
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

			//MainWindow.Log ("Clearing flash memory...");
			SendClearFlashMessage ();
			//SendCommand (Command.Handshake);
			//SendCommand (Command.Test, new byte[] {0xaa, 0xaa});

			return true;
		}

		public void Disconnect() {
			serial.Close ();
		}

		public void SendClearFlashMessage() {
			SendMessage (0, 0);
		}

		public void SendConfigurationValue(ConfigurationValue value) {
			SendMessage (value.address, value.value);
		}

		void WriteConfiguration() {
			/*//MainWindow.Log ("Writing configuration.");
			//MainWindow.SetStatusText ("Writing configuration");*/

			foreach (ConfigurationValue value in configurationValues) {
				SendConfigurationValue(value);
				System.Threading.Thread.Sleep (5);
			}
		}

		public void SendMessage(ushort address, ushort payload) {
		
			byte[] message = new byte[4];

			byte[] addressBytes = BitConverter.GetBytes (address);
			byte[] payloadBytes = BitConverter.GetBytes (payload);

			// The bytes are sent in swapped order.
			message [0] = addressBytes [1];
			message [1] = addressBytes [0];
			message [2] = payloadBytes [1];
			message [3] = payloadBytes [0];

			Trace.WriteLine ("Sending message, address: " + address + ", payload: " + payload);
			Trace.Write("Byte array:");
			foreach(byte b in message) {
				Trace.Write(b+", ");
			}
			Trace.WriteLine("");

			serial.Write (message, 0, message.Length);
		}

		public void DataReceived(object sender, SerialDataReceivedEventArgs e) {
			string data = serial.ReadExisting ();

			byte[] message = GetBytes(data);
			byte command = message[0];
			Trace.WriteLine ("Data received: " + data + " interpreted as command " + command);

			if(command == readyByte) {
				////MainWindow.ClearDone ();
				/*SendMessage (1, 0xffff);
				SendMessage (2, 0xf0f0);*/
				WriteConfiguration ();
			} else {
				Trace.WriteLine ("Unrecognized command received: " + command);
			}
		}

		public bool HasSerialPort() {
			return serial != null;
		}

		public bool Connected() {
			return HasSerialPort() && serial.IsOpen;
		}

		static byte[] GetBytes(string str) {
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}
	}
}

