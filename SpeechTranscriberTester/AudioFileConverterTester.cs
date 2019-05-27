/**************************************************************************
 *                                                                        *
 *  File:        AudioFileConverterTester.cs                              *
 *  Copyright:   (c) 2019, Octavian Oprișan                               *
 *  Description: Tests various functionalities of AudioFileConverter:     *
 *               ConvertToFormat, Rate, StereoToMono, also using          *
 *               wrong input parameters to check the output               *
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Speech_Transcriber;
using System.IO;

namespace SpeechTranscriberTester
{
    [TestClass]
    public class AudioFileConverterTester
    {
        private string testFile = System.IO.Path.GetFullPath(@"../../../Dataset/recognizer.mp3");
        private AudioFileConverter afc;

        /// <summary>
        /// Initializes the AudioFileConverter
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            afc = new AudioFileConverter();
        }

        /// <summary>
        /// Tests if the conversion created the wav file
        /// </summary>
        [TestMethod]
        public void TestFileExistsAfterConversion()
        {
            string outputFile = testFile.Replace(".mp3", ".wav");
            afc.ConvertToFormat(testFile, outputFile);
            Assert.AreEqual(true, File.Exists(outputFile));
        }

        /// <summary>
        /// Tests if the conversion failed by passing an invalid path
        /// </summary>
        [TestMethod]
        public void TestInexistentInputFile()
        {
            string outputFile = testFile.Replace(".mp3", ".wav");
            int result = afc.ConvertToFormat("inexistent-path", outputFile);
            Assert.AreEqual(-1, result);
        }

        /// <summary>
        /// Tests if the conversion failed by passing an invalid input format (mkv)
        /// </summary>
        [TestMethod]
        public void TestWrongInputFormat()
        {
            string wrongInput = testFile.Replace(".mp3", ".mkv");
            string outputFile = testFile.Replace(".mp3", ".wav");
            int result = afc.ConvertToFormat(wrongInput, outputFile);
            Assert.AreEqual(-1, result);
        }

        /// <summary>
        /// Tests if the conversion failed by passing an invalid output format (mkv)
        /// </summary>
        [TestMethod]
        public void TestWrongOutputFormat()
        {
            string wrongOutput = testFile.Replace(".mp3", ".mkv");
            int result = afc.ConvertToFormat(testFile, wrongOutput);
            Assert.AreEqual(-1, result);
        }

        /// <summary>
        /// Tests if the conversion succeded, by passing valid arguments
        /// </summary>
        [TestMethod]
        public void TestSuccessfulConversion()
        {
            string outputFile = testFile.Replace(".mp3", ".wav");
            int result = afc.ConvertToFormat(testFile, outputFile);
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Tests if the rate is equal to the expected rate
        /// </summary>
        [TestMethod]
        public void TestRate()
        {
            string file = testFile.Replace(".mp3", ".wav");
            int rate = afc.Rate(file);
            Assert.AreEqual(48000, rate);
        }

        /// <summary>
        /// Tests if tha rate is -1 when passing an inexistent file
        /// </summary>
        [TestMethod]
        public void TestRateInexistentFile()
        {
            string file = testFile.Replace(".mp3", ".wavx");
            int rate = afc.Rate(file);
            Assert.AreEqual(-1, rate);
        }

        /// <summary>
        /// Tests if there is only one channel (output 0)
        /// </summary>
        [TestMethod]
        public void TestStereoToMonoWrongNoChannels()
        {
            string inputFile = testFile.Replace(".mp3", ".wav");
            string outputFile = testFile.Replace("recognizer.mp3", "recognizer2.wav");
            int result = afc.StereoToMono(inputFile, outputFile);
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Tests if StereoToMono fails when an invalid path is passed
        /// </summary>
        [TestMethod]
        public void TestInexistentPathStereoToMono()
        {
            string inputFile = testFile.Replace(".mp3", ".wav2");
            string outputFile = testFile.Replace("recognizer.mp3", "recognizer2.wav");
            int result = afc.StereoToMono(inputFile, outputFile);
            Assert.AreEqual(-1, result);
        }
    }
}
