using System;
using System.Collections;

namespace SoundboxConfigGUI
{
	public class ConfigurationValue
	{
		public UInt16 value;
		public UInt16 address;

		public ConfigurationValue (UInt16 address) {
			this.address = address;
		}

		protected void ResetBits() {
			value = 0;
		}

		protected void SetBit(int index, bool v) {
			if(v) {
				int tempValue = (UInt16) value;
				tempValue = tempValue | (1 << index);
				value = (UInt16) tempValue;
			}
		}

		protected void BitsToValue(BitArray bits, int offset) {
			ResetBits ();

			for (int i = 0; i < 16; i++) {
				SetBit (i, bits[i+offset]);
			}
		}
	}
}
	
