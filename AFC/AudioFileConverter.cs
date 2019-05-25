﻿/**************************************************************************
 *                                                                        *
 *  File:        AudioFileConverter.cs                                                *
 *  Copyright:   (c) 2019, Octavian Oprișan                               *
 *  Description: Implementation of IAudioFileConverter interface          *
 *               AudioFileConverter object can perform:                   *
 *               * Convert MP3 format to WAV format                       *
 *               * Convert Stereo audio file to Mono audio file           *
 *               * Get the frame rate of audio file                       *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;
using System.IO;
using NAudio.Wave;

namespace Speech_Transcriber
{
    public class AudioFileConverter : IAudioFileConverter
    {
        #region Implemented interface methods
        public int ConvertToFormat(string filePath, string outputFilePath)
        {
            string inputFormat = Path.GetExtension(filePath); // .mp3 sau .wav sau ...
            string outputFormat = Path.GetExtension(outputFilePath);
            if (inputFormat != ".mp3" || outputFormat != ".wav")
                return -1;

            try
            {
                using (Mp3FileReader mp3 = new Mp3FileReader(filePath))
                {
                    using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                    {
                        WaveFileWriter.CreateWaveFile(outputFilePath, pcm);
                    }
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int StereoToMono(string filePath, string outputFilePath)
        {
            string inputFormat = Path.GetExtension(filePath); // .mp3 sau .wav sau ...
            string outputFormat = Path.GetExtension(outputFilePath);
            if (inputFormat != ".wav" || outputFormat != ".wav")
                return -1;
            if (!File.Exists(filePath))
                return -1;
            try
            {
                using (var waveFileReader = new WaveFileReader(filePath))
                {
                    if (waveFileReader.WaveFormat.Channels == 1)
                        return 0;
                    if (waveFileReader.WaveFormat.Channels > 2)
                        return -1;
                    var toMono = new StereoToMonoProvider16(waveFileReader);
                    WaveFileWriter.CreateWaveFile(outputFilePath, toMono);
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }

        public int Rate(string filePath)
        {
            if (!File.Exists(filePath))
                return -1;
            try
            {
                using (var waveFileReader = new WaveFileReader(filePath))
                {
                    return waveFileReader.WaveFormat.SampleRate;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion
    }
}