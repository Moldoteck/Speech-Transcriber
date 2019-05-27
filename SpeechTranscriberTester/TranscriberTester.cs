/**************************************************************************
 *                                                                        *
 *  File:        TranscriberTester.cs                                     *
 *  Copyright:   (c) 2019, Octavian Oprișan                               *
 *  Description: Tests the functionality of method TranscribeAudioFile    *
 *               from the Transcriber, also using wrong input             *
 *               parameters to check the output                           *
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

namespace SpeechTranscriberTester
{
    [TestClass]
    public class TranscriberTester
    {
        private string testFile = System.IO.Path.GetFullPath(@"../../../Dataset/recognizer.mp3");
        private Transcriber transcriber;
        private string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];

        /// <summary>
        /// Initializes the transcriber
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            transcriber = new Transcriber("C:/Users/" + username + "/Downloads/TextToSpeech-d9a5f0e6b87b.json");
        }

        /// <summary>
        /// Test if the transcribed audio file is accurate
        /// </summary>
        [TestMethod]
        public void TestTranscribeAudioFile()
        {

            AudioFileConverter conv = new AudioFileConverter();
            string wavFilePath = testFile.Replace(".mp3", ".wav");
            string currentCloudFile = "interviewstorage1/recognizer.wav";
            string textResult = transcriber.TranscribeAudioFile(currentCloudFile, 999999, false, new string[0], "Romanian (Romania)", conv.Rate(wavFilePath));
            string expectedText = "Bună ziua mă cheamă Cristian și am multe bomboane".ToLower();
            Assert.AreEqual(true, textResult.ToLower().Contains(expectedText));// avoid \r\n or other noise
        }

        /// <summary>
        /// Test if the transcribing fails when passing an invalid language
        /// </summary>
        [TestMethod]
        public void TestTranscribeAudioFileInvalidLanguage()
        {
            AudioFileConverter conv = new AudioFileConverter();
            string wavFilePath = testFile.Replace(".mp3", ".wav");
            string currentCloudFile = "interviewstorage1/recognizer.wav";
            string textResult = transcriber.TranscribeAudioFile(currentCloudFile, 999999, false, new string[0], "invalid language", conv.Rate(wavFilePath));
            Assert.AreEqual("", textResult);
        }
    }
}
