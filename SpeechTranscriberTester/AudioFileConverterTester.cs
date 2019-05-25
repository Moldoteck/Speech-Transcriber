﻿using System;
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

        [TestInitialize]
        public void Init()
        {
            afc = new AudioFileConverter();
        }

        [TestMethod]
        public void TestFileExistsAfterConversion()
        {
            string outputFile = testFile.Replace(".mp3", ".wav");
            afc.ConvertToFormat(testFile, outputFile);
            Assert.AreEqual(true, File.Exists(outputFile));
        }

        [TestMethod]
        public void TestInexistentInputFile()
        {
            string outputFile = testFile.Replace(".mp3", ".wav");
            int result = afc.ConvertToFormat("inexistent-path", outputFile);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void TestWrongInputFormat()
        {
            string wrongInput = testFile.Replace(".mp3", ".mkv");
            string outputFile = testFile.Replace(".mp3", ".wav");
            int result = afc.ConvertToFormat(wrongInput, outputFile);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void TestWrongOutputFormat()
        {
            string wrongOutput = testFile.Replace(".mp3", ".mkv");
            int result = afc.ConvertToFormat(testFile, wrongOutput);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void TestSuccessfulConversion()
        {
            string outputFile = testFile.Replace(".mp3", ".wav");
            int result = afc.ConvertToFormat(testFile, outputFile);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestRate()
        {
            string file = testFile.Replace(".mp3", ".wav");
            int rate = afc.Rate(file);
            Assert.AreEqual(48000, rate);
        }

        [TestMethod]
        public void TestRateInexistentFile()
        {
            string file = testFile.Replace(".mp3", ".wavx");
            int rate = afc.Rate(file);
            Assert.AreEqual(-1, rate);
        }

        [TestMethod]
        public void TestStereoToMonoWrongNoChannels()
        {
            string inputFile = testFile.Replace(".mp3", ".wav");
            string outputFile = testFile.Replace("recognizer.mp3", "recognizer2.wav");
            int result = afc.StereoToMono(inputFile, outputFile);
            Assert.AreEqual(0, result);
        }

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