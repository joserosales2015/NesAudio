using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{
	public class Song
	{
		public string Name { get; set; }

		public int SequenceId { get; set; }

		public int BPM { get; set; }

		public double GridDurationMs { get; set; }

		public List<NoteEvent> Notes { get; } = new();

		public double TotalDurationMs => Notes.Count == 0 ? 0 : Notes.Max(n => n.EndTimeMs);

	}
}
