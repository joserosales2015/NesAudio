using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{
	public enum WaveType
	{
		Pulse12,
		Pulse25,
		Pulse50,
		Pulse75,

		Triangle,

		Noise
	}

	public class Instrument
	{
		public int Id { get; set; }

		public WaveType WaveType { get; set; }

		public float Volume { get; set; } = 1.0f;

		public NoiseGenerator Noise { get; set; }
	}
}
