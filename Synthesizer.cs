using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{
	public class Synthesizer
	{
		public void PlayNote(string note, int instrumentId, int durationMs)
		{
			Task.Run(() =>
			{
				Song song = new Song
				{
					BPM = 240
				};

				song.Notes.Add(
					new NoteEvent
					{
						Note = note,
						Frequency = MusicTheory.GetFrequency(note),
						Instrument = instrumentId,
						StartTimeMs = 0,
						DurationMs = durationMs
					});

				var player = new SongPlayer(song);

				var waveOut = new WaveOutEvent();

				waveOut.Init(player);

				waveOut.Play();

				Thread.Sleep(durationMs + 100);

				waveOut.Stop();

				waveOut.Dispose();
			});
		}
	}
}
