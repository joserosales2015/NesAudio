# NesAudio

Una librería de síntesis de audio en C# que emula el sistema de sonido del Nintendo Entertainment System (NES). Genera música de 8 bits con formas de onda características del NES: pulse waves, triangle waves y generador de ruido.

## Características

- **Síntesis de ondas NES**: Genera ondas pulse (12%, 25%, 50%, 75% duty cycle), triangle waves y ruido blanco
- **Generador de envolvente**: Aplica envolventes ADSR simples a las notas
- **Reproductor de canciones**: Lee y reproduce secuencias de notas con instrumentos predefinidos
- **Parser de canciones**: Carga canciones desde archivos de texto con formato personalizado
- **Teoría musical integrada**: Conversión de notas (C4, D#5, etc.) a frecuencias

## Requisitos

- **.NET 8.0** o superior
- **NAudio 2.3.0** para reproducción de audio

## Instalación

```bash
git clone https://github.com/joserosales2015/NesAudio.git
cd NesAudio
```

## Uso

### Reproducir una nota individual

```csharp
var synthesizer = new Synthesizer();
synthesizer.PlayNote("C4", 25, 500);  // Nota C4, instrumento 25, 500ms
```

### Crear y reproducir una canción

```csharp
var song = new Song 
{ 
    BPM = 240 
};

song.Notes.Add(new NoteEvent
{
    Note = "C4",
    Frequency = MusicTheory.GetFrequency("C4"),
    Instrument = 25,
    StartTimeMs = 0,
    DurationMs = 250
});

song.Notes.Add(new NoteEvent
{
    Note = "E4",
    Frequency = MusicTheory.GetFrequency("E4"),
    Instrument = 25,
    StartTimeMs = 250,
    DurationMs = 250
});

var player = new SongPlayer(song);
var waveOut = new WaveOutEvent();
waveOut.Init(player);
waveOut.Play();
```

### Cargar una canción desde archivo

```csharp
var song = SongParser.ParseFile("mi_cancion.txt");
var player = new SongPlayer(song);
var waveOut = new WaveOutEvent();
waveOut.Init(player);
waveOut.Play();
```

### Formato de archivo de canciones

Las canciones se definen en archivos de texto con el siguiente formato:

```
SEQ:1;
BPM:240;
0 C4 1 25;
1 E4 1 25;
2 G4 1 25;
3 C5 2 25;
```

Campos:
- **SEQ**: ID de secuencia (opcional)
- **BPM**: Beats por minuto
- **Posición**: Posición en grillas (4 grillas por beat)
- **Nota**: Nota musical (C4, D#5, etc.)
- **Duración**: Duración en grillas
- **Instrumento**: ID del instrumento

## Instrumentos predefinidos

| ID | Tipo | Duty Cycle | Volumen |
|----|------|-----------|---------|
| 25 | Pulse | 25% | 0.8 |
| 26 | Triangle | - | 0.6 |
| 27 | Noise | - | 0.5 |

## Estructura del proyecto

```
NesAudio/
├── Synthesizer.cs          Interfaz principal para reproducir notas
├── SongPlayer.cs           Reproductor de canciones (ISampleProvider)
├── SongParser.cs           Parser de archivos de canciones
├── WaveGenerator.cs        Generador de formas de onda
├── EnvelopeGenerator.cs    Envolventes de amplitud
├── WaveType.cs             Tipos de ondas soportadas
├── Instrument.cs           Definición de instrumentos
├── InstrumentBank.cs       Banco de instrumentos predefinidos
├── MusicTheory.cs          Conversión nota → frecuencia
├── NoteEvent.cs            Evento de nota
├── NotePlayer.cs           Reproductor de notas individuales
├── Song.cs                 Definición de canción
├── NoiseGenerator.cs       Generador de ruido blanco
└── NesAudio.csproj         Configuración del proyecto .NET
```

## Arquitectura

1. **Parser**: `SongParser` lee archivos de canciones y crea estructuras de datos
2. **Síntesis**: `WaveGenerator` genera las ondas digitales basadas en el tipo de instrumento
3. **Envolvente**: `EnvelopeGenerator` modula la amplitud de cada nota
4. **Reproductor**: `SongPlayer` implementa `ISampleProvider` de NAudio para la reproducción en tiempo real
5. **Salida de audio**: NAudio maneja la reproducción mediante `WaveOutEvent`

## Ejemplo completo

```csharp
using NesAudio;
using NAudio.Wave;

// Crear una canción
var song = new Song { BPM = 120 };

// Agregar notas
for (int i = 0; i < 4; i++)
{
    song.Notes.Add(new NoteEvent
    {
        Note = new[] { "C4", "E4", "G4", "C5" }[i],
        Frequency = MusicTheory.GetFrequency(new[] { "C4", "E4", "G4", "C5" }[i]),
        Instrument = 25,
        StartTimeMs = i * 250,
        DurationMs = 250
    });
}

// Reproducir
var player = new SongPlayer(song);
var waveOut = new WaveOutEvent();
waveOut.Init(player);
waveOut.Play();
Thread.Sleep(2000);
waveOut.Stop();
waveOut.Dispose();
```

## Dependencias

- **NAudio**: Librería para reproducción y síntesis de audio en .NET
  - Proporciona `ISampleProvider` para síntesis en tiempo real
  - Maneja la inicialización y reproducción de audio

## Licencia

Este proyecto no tiene licencia especificada. Consulta el repositorio para más información.

## Autor

[@joserosales2015](https://github.com/joserosales2015)

---

**Nota**: Esta es una librería educativa para emular la síntesis de audio retro del NES. Ideal para proyectos de juegos retro, demoscene y síntesis experimental.
