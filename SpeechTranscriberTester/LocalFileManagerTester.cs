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
        private IFileManagerFactory fmf = new FileManagerFactory();
        private IFileManager lfm = null;

        /// <summary>
        /// Initializes the IFileManager
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            lfm = fmf.GetFileManager();
        }

        /// <summary>
        /// Test if the file exists by using a valid file path
        /// </summary>
        [TestMethod]
        public void TestExistingFile()
        {
            bool result = lfm.CheckExists(testFile);
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Test if the file exists by using an invalid file path
        /// </summary>
        [TestMethod]
        public void TestNonExistingFile()
        {
            bool result = lfm.CheckExists(testFile + "x");
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test if the file is deleted
        /// </summary>
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

        /// <summary>
        /// Test if an error status code is returned when deleting an empty string path
        /// </summary>
        [TestMethod]
        public void TestDeleteInexistentFile()
        {
            ErrorCode status = lfm.DeleteFile("");
            Assert.AreEqual(ErrorCode.EXTERNAL_COMPONENT_ERROR, status);
        }

        /// <summary>
        /// Test if an error status code is returned when deleting a null path
        /// </summary>
        [TestMethod]
        public void TestDeleteNullFile()
        {
            ErrorCode status = lfm.DeleteFile(null);
            Assert.AreEqual(ErrorCode.EXTERNAL_COMPONENT_ERROR, status);
        }

        /// <summary>
        /// Test if the length of the list returned is equal to the number of items in the directory
        /// </summary>
        [TestMethod]
        public void TestListFilesFromPathLength()
        {
            string dir = testFile.Replace("recognizer.mp3", "");
            int length = lfm.ListFilesFromPath(dir).Length;
            Assert.AreEqual(2, length);
        }

        /// <summary>
        /// Test if an error status code is returned when reading the contents of an empty string directory
        /// </summary>
        [TestMethod]
        public void TestListFilesFromPathNonExisting()
        {
            Assert.AreEqual(null, lfm.ListFilesFromPath(""));
        }

        /// <summary>
        /// Test if an error status code is returned when reading the contents of a null directory
        /// </summary>
        [TestMethod]
        public void TestListFilesFromPathNull()
        {
            Assert.AreEqual(null, lfm.ListFilesFromPath(null));
        }

        /// <summary>
        /// Test if the file was successfully stored
        /// </summary>
        [TestMethod]
        public void TestStoreFile()
        {
            string outputFile = testFile.Replace(".mp3", ".txt");
            ErrorCode status = lfm.StoreFile("Write this text to file", outputFile);
            Assert.AreEqual(ErrorCode.SUCCESS, status);
        }

        /// <summary>
        /// Test if ErrorCode.EXTERNAL_COMPONENT_ERROR is returned when passing null arguments to store
        /// </summary>
        [TestMethod]
        public void TestStoreFileNull()
        {
            ErrorCode status = lfm.StoreFile(null, null);
            Assert.AreEqual(ErrorCode.EXTERNAL_COMPONENT_ERROR, status);
        }
    }
}