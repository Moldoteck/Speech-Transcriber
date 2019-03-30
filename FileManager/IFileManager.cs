using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    interface IFileManager
    {
        void CheckExists(string filePath, string fileName);
        void DeleteFile(string filePath, string fileName);
        void ListFilesFromPath(string path);
        void GetFileContent(string filePath, string fileName);
        void StoreFile(string inputData, string outputPath, string outputFileName);
    }
}
