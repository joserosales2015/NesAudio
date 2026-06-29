using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{
	public class NotePlayer : ISampleProvider
	{
		private readonly double _frequency;

		private readonly Instrument _instrument;

		private readonly WaveFormat _waveFormat;

		private long _samplePosition;

		public WaveFormat WaveFormat => _waveFormat;

		public NotePlayer(double frequency, Instrument instrument)
		{
			_frequency = frequency;
			_instrument = instrument;

			_waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);
		}

		public int Read(
			float[] buffer,
			int offset,
			int count)
		{
			int sampleRate =
				_waveFormat.SampleRate;

			for (int i = 0; i < count; i++)
			{
				double t =
					(double)_samplePosition /
					sampleRate;

				double sample =
					WaveGenerator.Generate(
						_frequency,
						_instrument,
						t);

				buffer[offset + i] =
					(float)(sample * 0.3);

				_samplePosition++;
			}

			return count;
		}

		public static void PlayNote(string note, int instrumentId, int durationMs)
		{
			double frequency = MusicTheory.GetFrequency(note);

			var instrument = InstrumentBank.Get(instrumentId);

			var player = new NotePlayer(frequency, instrument);

			var waveOut = new WaveOutEvent();

			waveOut.Init(player);

			waveOut.Play();

			Task.Delay(durationMs).ContinueWith(_ =>
			{
				waveOut.Stop();
				waveOut.Dispose();
			});
		}
	}
}
