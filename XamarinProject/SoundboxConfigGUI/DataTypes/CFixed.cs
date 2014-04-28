using System;
using System.Collections;

namespace SoundboxConfigGUI
{
	public class CFixed : ConfigurationValue
	{
		public CFixed(UInt16 address, double value, int fractionalBits, bool lsb) : base(address) {
			// Magic!
			int i = RoundZero (value*Math.Pow(2.0, fractionalBits));
			BitArray bits = new BitArray (new int[] { i });

			if (lsb) {
				BitsToValue (bits, 0);
			} else {
				BitsToValue (bits, 16);
			}
		}

		int RoundZero(double v) {
			if (v < 0) {
				return (int) Math.Ceiling (v);
			}
			return (int) Math.Floor (v);
		}
	}
}

