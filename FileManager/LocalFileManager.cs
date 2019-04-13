using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManager
{
    class LocalFileManager : IFileManager
    {
        public bool CheckExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        public int DeleteFile(string filePath)
        {
            try
            {
                System.IO.File.Delete(filePath);
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public string[] ListFilesFromPath(string filePath)
        {
            return System.IO.Directory.GetFiles(filePath);
        }

        public int StoreFile(string inputData, string outputPath)
        {
            try
            {
                File.WriteAllText(outputPath, inputData);
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
