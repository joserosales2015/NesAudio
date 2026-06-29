using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{
	public static class WaveGenerator
	{
		public static double Generate(double frequency, Instrument instrument, double t)
		{
			switch (instrument.WaveType)
			{
				case WaveType.Pulse12:
					return PulseWave(frequency, 0.125, t);

				case WaveType.Pulse25:
					return PulseWave(frequency, 0.25, t);

				case WaveType.Pulse50:
					return PulseWave(frequency, 0.50, t);

				case WaveType.Pulse75:
					return PulseWave(frequency, 0.75, t);

				case WaveType.Triangle:
					return TriangleWave(frequency, t);

				case WaveType.Noise:
					return instrument.Noise.NextSample();

				default:
					return 0;
			}
		}

		private static double PulseWave(double frequency, double duty, double t)
		{
			double phase = t * frequency % 1.0;

			return phase < duty ? 1.0 : -1.0;
		}

		private static double TriangleWave(double frequency, double t)
		{
			double phase = t * frequency % 1.0;

			return 2.0 * Math.Abs(2.0 * phase - 1.0) - 1.0;
		}
	}
}
