using NesAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{
	public static class InstrumentBank
	{
		private static readonly Dictionary<int, Instrument> _instruments =
			new()
			{
				{
					25, 
					new Instrument
					{
						Id = 25,
						WaveType = WaveType.Pulse25,
						Volume = 0.8f
					}
				},

				{ 
					26, 
					new Instrument
					{
						Id = 26,
						WaveType = WaveType.Triangle,
						Volume = 0.6f
					}
				},

				{ 
					27, 
					new Instrument
					{
						Id = 27,
						WaveType = WaveType.Noise,
						Volume = 0.5f,
						Noise = new NoiseGenerator()
					}
				}
			};

		public static Instrument Get(int id)
		{
			return _instruments[id];
		}
	}
}
