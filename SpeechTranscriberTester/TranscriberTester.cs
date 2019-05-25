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

        [TestInitialize]
        public void Init()
        {
            transcriber = new Transcriber();
        }
        [TestMethod]
        public void TestTranscribeAudioFile()
        {

            AudioFileConverter conv = new AudioFileConverter();
            string wavFilePath = testFile.Replace(".mp3", ".wav");
            string currentCloudFile = "interviewstorage1/" + wavFilePath;
            string textResult = transcriber.TranscribeAudioFile(currentCloudFile, 999999, false, new string[0], "Romanian (Romania)", conv.Rate(wavFilePath));
            Assert.AreEqual("", textResult);
        }
    }
}
