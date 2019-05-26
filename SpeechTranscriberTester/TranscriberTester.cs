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

            string jsonPath = "C:/Users/cristian/Downloads/TextToSpeech-d9a5f0e6b87b.json";
            //string jsonPath = "C:/Users/Octavian/Downloads/TextToSpeech-d9a5f0e6b87b.json";
            transcriber = new Transcriber(jsonPath);
        }
        [TestMethod]
        public void TestTranscribeAudioFile()
        {

            AudioFileConverter conv = new AudioFileConverter();
            string wavFilePath = testFile.Replace(".mp3", ".wav");
            string currentCloudFile = "interviewstorage1/recognizer.wav";
            string textResult = transcriber.TranscribeAudioFile(currentCloudFile, 999999, false, new string[0], "Romanian (Romania)", conv.Rate(wavFilePath));
            Assert.AreEqual("", textResult);
        }
    }
}
