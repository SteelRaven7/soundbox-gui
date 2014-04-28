using System;

namespace SoundboxConfigGUI
{
	public class CUnsignedInteger : ConfigurationValue {
		public CUnsignedInteger (UInt16 address, UInt16 v) : base(address) {
			value = v;
		}
	}
}

