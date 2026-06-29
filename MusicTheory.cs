using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{
	public static class MusicTheory
	{
		private static readonly Dictionary<string, int> NoteOffsets = new()
	{
		{ "C", 0 },
		{ "C#", 1 },
		{ "D", 2 },
		{ "D#", 3 },
		{ "E", 4 },
		{ "F", 5 },
		{ "F#", 6 },
		{ "G", 7 },
		{ "G#", 8 },
		{ "A", 9 },
		{ "A#", 10 },
		{ "B", 11 }
	};

		public static double GetFrequency(string note)
		{
			note = note.Trim().ToUpper();

			string noteName;
			int octave;

			if (note.Length == 2)
			{
				noteName = note.Substring(0, 1);
				octave = int.Parse(note.Substring(1));
			}
			else
			{
				noteName = note.Substring(0, 2);
				octave = int.Parse(note.Substring(2));
			}

			int semitone = NoteOffsets[noteName];

			int midi = (octave + 1) * 12 + semitone;

			return 440.0 * Math.Pow(2, (midi - 69) / 12.0);
		}
	}
}




