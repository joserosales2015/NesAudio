using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{
	public static class EnvelopeGenerator
	{
		public static double Simple(NoteEvent note, double currentTimeMs)
		{
			double elapsed = currentTimeMs - note.StartTimeMs;

			const double attack = 5;
			const double release = 10;

			if (elapsed < attack)
				return elapsed / attack;

			double remaining = note.EndTimeMs - currentTimeMs;

			if (remaining < release)
				return remaining / release;

			return 1.0;
		}
	}
}
