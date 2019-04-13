using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    interface IFileManager
    {
        void CheckExists(string filePath);
        void DeleteFile(string filePath);
        void ListFilesFromPath(string filePath);
        void StoreFile(string inputData, string outputPath);
    }
}
