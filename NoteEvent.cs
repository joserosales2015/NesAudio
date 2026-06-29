using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesAudio
{
	public class NoteEvent
	{
		/// <summary>
		/// Posición en la cuadrícula.
		/// </summary>
		public double Position { get; set; }

		/// <summary>
		/// F6, D#6, etc.
		/// </summary>
		public string Note { get; set; }

		/// <summary>
		/// Frecuencia calculada.
		/// </summary>
		public double Frequency { get; set; }

		/// <summary>
		/// Longitud en cuadrículas.
		/// </summary>
		public double Length { get; set; }

		public int Instrument { get; set; }

		/// <summary>
		/// Tiempo de inicio en milisegundos.
		/// </summary>
		public double StartTimeMs { get; set; }

		/// <summary>
		/// Duración real en milisegundos.
		/// </summary>
		public double DurationMs { get; set; }

		/// <summary>
		/// Tiempo de finalización.
		/// </summary>
		public double EndTimeMs => StartTimeMs + DurationMs;
	}
}
