using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    interface IFileManager
    {
        bool CheckExists(string filePath);
        int DeleteFile(string filePath);
        string[] ListFilesFromPath(string filePath);
        int StoreFile(string inputData, string outputPath);
    }
}
