using System;
using System.Collections;

namespace SoundboxConfigGUI
{
	public class CInteger : ConfigurationValue {
		public CInteger (UInt16 address, Int16 v) : base(address) {
			BitArray bits = new BitArray (BitConverter.GetBytes(v));
			BitsToValue (bits, 0);
		}
	}
}
	
