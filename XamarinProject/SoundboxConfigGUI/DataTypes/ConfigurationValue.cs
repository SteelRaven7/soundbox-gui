using System;

namespace SoundboxConfigGUI
{
	public class ConfigurationValue
	{
		protected UInt16 value;

		public ConfigurationValue () {

		}

		public virtual UInt16 Value() {
			return value;
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
	}
}
	
