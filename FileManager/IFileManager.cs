using HelperStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    public interface IFileManager
    {
        bool CheckExists(string filePath, string fileName);
        void DeleteFile(string filePath, string fileName);
        List<string> ListFilesFromPath(string path);
        void GetFileContent(string filePath, string fileName, string outputFilePath);
        ErrorCode StoreFile(string inputData, string outputPath, string outputFileName);
    }
}
