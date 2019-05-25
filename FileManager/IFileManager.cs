using HelperStructures;

namespace FileManager
{
    interface IFileManager
    {
        bool CheckExists(string filePath);
        ErrorCode DeleteFile(string filePath);
        string[] ListFilesFromPath(string filePath);
        ErrorCode StoreFile(string inputData, string outputPath);
    }
}
