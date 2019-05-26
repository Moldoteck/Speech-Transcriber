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

        [TestInitialize]
        public void Init()
        {
            transcriber = new Transcriber("C:/Users/" + username + "/Downloads/TextToSpeech-d9a5f0e6b87b.json");
        }
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
