using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{ 
	public class NoiseGenerator
	{
		private ushort _lfsr = 1;

		public double NextSample()
		{
			int bit =
				(_lfsr >> 0 ^
				 _lfsr >> 1) & 1;

			_lfsr =
				(ushort)(_lfsr >> 1 |
						 bit << 14);

			return (_lfsr & 1) == 0
				? -1.0
				: 1.0;
		}
	}
}
