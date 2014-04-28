using System;
using System.Collections;

namespace SoundboxConfigGUI
{
	public class CFixed : ConfigurationValue
	{
		BitArray bits;

		public CFixed(float R, int W, int F, bool lsb)
		{
			//std_logic_vector(to_signed(integer(round_zero(R * (2.0**(F)))),W));

			int i = RoundZero ((float) (R*Math.Pow(2.0, F)));
			bits = new BitArray (new int[] { i });

			if (lsb) {
				BitsToValue (0);
			} else {
				BitsToValue(16);
			}
		}

		int RoundZero(float v) {
			if (v < 0) {
				return (int) Math.Ceiling (v);
			}
			return (int) Math.Floor (v);
		}

		void BitsToValue(int offset) {
			ResetBits ();

			int N = offset + 16;
			for (int i = offset; i < N; i++) {
				SetBit (i, bits[i]);
			}
		}
	}
}

