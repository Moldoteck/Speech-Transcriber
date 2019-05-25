using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Speech_Transcriber;
using System.IO;

namespace SpeechTranscriberTester
{
    [TestClass]
    public class LocalFileManagerTester
    {
        private string testFile = System.IO.Path.GetFullPath(@"../../../Dataset/recognizer.mp3");
        private LocalFileManager lfm;

        [TestInitialize]
        public void Init()
        {
            lfm = new LocalFileManager();
        }
        [TestMethod]
        public void TestExistingFile()
        {
            bool result = lfm.CheckExists(testFile);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestNonExistingFile()
        {
            bool result = lfm.CheckExists(testFile + "x");
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestDeleteFile()
        {
            string test = testFile.Replace(".mp3", ".txt");
            using (StreamWriter sw = File.CreateText(test))
            {
                sw.WriteLine("File to be deleted");
            }
            ErrorCode status = lfm.DeleteFile(test);
            Assert.AreEqual(ErrorCode.SUCCESS, status);
        }
        [TestMethod]
        public void TestDeleteInexistentFile()
        {
            ErrorCode status = lfm.DeleteFile("");
            Assert.AreEqual(ErrorCode.EXTERNAL_COMPONENT_ERROR, status);
        }

        [TestMethod]
        public void TestDeleteNullFile()
        {
            ErrorCode status = lfm.DeleteFile(null);
            Assert.AreEqual(ErrorCode.EXTERNAL_COMPONENT_ERROR, status);
        }
        [TestMethod]
        public void TestListFilesFromPathLength()
        {
            string dir = testFile.Replace("recognizer.mp3", "");
            int length = lfm.ListFilesFromPath(dir).Length;
            Assert.AreEqual(2, length);
        }
        [TestMethod]
        public void TestListFilesFromPathNonExisting()
        {
            Assert.AreEqual(null, lfm.ListFilesFromPath(""));
        }
        [TestMethod]
        public void TestListFilesFromPathNull()
        {
            Assert.AreEqual(null, lfm.ListFilesFromPath(null));
        }
        [TestMethod]
        public void TestStoreFile()
        {
            string outputFile = testFile.Replace(".mp3", ".txt");
            ErrorCode status = lfm.StoreFile("Write this text to file", outputFile);
            Assert.AreEqual(ErrorCode.SUCCESS, status);
        }
        [TestMethod]
        public void TestStoreFileNull()
        {
            ErrorCode status = lfm.StoreFile(null, null);
            Assert.AreEqual(ErrorCode.EXTERNAL_COMPONENT_ERROR, status);
        }
    }
}
