using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace NesAudio	
{
	public class SongPlayer : ISampleProvider
	{
		private readonly Song _song;

		private readonly WaveFormat _waveFormat;

		private readonly List<NoteEvent> _activeNotes = new();

		private int _nextNoteIndex;

		private long _samplePosition;

		public WaveFormat WaveFormat => _waveFormat;

		public SongPlayer(Song song)
		{
			_song = song;

			_song.Notes.Sort((a, b) => a.StartTimeMs.CompareTo(b.StartTimeMs));

			_waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);
		}

		public int Read(float[] buffer, int offset, int count)
		{
			int sampleRate = _waveFormat.SampleRate;

			for (int i = 0; i < count; i++)
			{
				double currentTimeMs = _samplePosition * 1000.0 / sampleRate;

				while (_nextNoteIndex < _song.Notes.Count)
				{
					var note = _song.Notes[_nextNoteIndex];

					if (note.StartTimeMs > currentTimeMs)
					{
						break;
					}

					_activeNotes.Add(note);

					_nextNoteIndex++;
				}

				_activeNotes.RemoveAll(n => currentTimeMs >= n.EndTimeMs);

				double t =	(double)_samplePosition / sampleRate;
				double sample = 0;

				foreach (var note in _activeNotes)
				{
					sample += GenerateInstrument(note, currentTimeMs, t);
				}

				if (_activeNotes.Count > 0)
				{
					sample /= _activeNotes.Count;
				}

				buffer[offset + i] = (float)(sample * 0.30);

				_samplePosition++;
			}

			return count;
		}

		private double GenerateInstrument(NoteEvent note, double currentTimeMs, double t)
		{
			Instrument instrument = InstrumentBank.Get(note.Instrument);

			double sample = WaveGenerator.Generate(note.Frequency, instrument, t);

			sample *= EnvelopeGenerator.Simple(note, currentTimeMs);

			return sample;
		}

		public void Reset()
		{
			_samplePosition = 0;

			_nextNoteIndex = 0;

			_activeNotes.Clear();
		}
	}
}
