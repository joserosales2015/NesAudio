using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{
	public static class SongParser
	{
		/// <summary>
		/// Asume 4 cuadrículas por beat.
		/// </summary>
		private const int GridsPerBeat = 4;

		public static Song ParseFile(string filePath)
		{
			string text = File.ReadAllText(filePath);

			Song song = Parse(text);

			song.Name = Path.GetFileNameWithoutExtension(filePath);

			return song;
		}

		public static Song Parse(string text)
		{
			var song = new Song();

			string[] blocks = text.Split(';', StringSplitOptions.RemoveEmptyEntries);

			foreach (string rawBlock in blocks)
			{
				string block = rawBlock.Trim();

				if (block.StartsWith("SEQ:"))
				{
					song.SequenceId = int.Parse(block.Substring(4));
				}
				else if (block.StartsWith("BPM:"))
				{
					song.BPM = int.Parse(block.Substring(4));
				}
			}

			song.GridDurationMs = 60000.0 / song.BPM / GridsPerBeat;

			foreach (string rawBlock in blocks)
			{
				string block = rawBlock.Trim();

				if (block.StartsWith("SEQ:"))
					continue;

				if (block.StartsWith("BPM:"))
					continue;

				string[] parts = block.Split(' ', StringSplitOptions.RemoveEmptyEntries);

				if (parts.Length != 4)
					continue;

				double position = double.Parse(parts[0]);
				string note = parts[1];
				double length = double.Parse(parts[2]);
				int instrument = int.Parse(parts[3]);

				double frequency = MusicTheory.GetFrequency(note);

				double startMs = position * song.GridDurationMs;

				double durationMs = length * song.GridDurationMs;

				song.Notes.Add(new NoteEvent
				{
					Position = position,
					Note = note,
					Frequency = frequency,
					Length = length,
					Instrument = instrument,
					StartTimeMs = startMs,
					DurationMs = durationMs
				});
			}

			song.Notes.Sort((a, b) => a.Position.CompareTo(b.Position));

			return song;
		}
	}
}
