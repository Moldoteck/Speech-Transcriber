using System;
using System.IO;
using NAudio.Wave;

namespace AudioFileConverter
{
    public class AudioFileConverter : IAudioFileConverter
    {
        public int ConvertToFormat(string filePath, string outputFilePath)
        {
            string inputFormat = Path.GetExtension(filePath); // .mp3 sau .wav sau ...
            string outputFormat = Path.GetExtension(outputFilePath);
            if (inputFormat != ".mp3" || outputFormat != ".wav")
                return -1;
            using (Mp3FileReader mp3 = new Mp3FileReader(filePath))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(outputFilePath, pcm);
                }
            }
            return 1;
        }

        public int StereoToMono(string filePath, string outputFilePath)
        {
            string inputFormat = Path.GetExtension(filePath); // .mp3 sau .wav sau ...
            string outputFormat = Path.GetExtension(outputFilePath);
            if (inputFormat != ".wav" || outputFormat != ".wav")
                return -1;
            using (var waveFileReader = new WaveFileReader(filePath))
            {
                var toMono = new StereoToMonoProvider16(waveFileReader);
                WaveFileWriter.CreateWaveFile(outputFilePath, toMono);
            }
            return 1;
        }
    }
}
