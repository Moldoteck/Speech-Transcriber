namespace Speech_Transcriber
{
    interface IFileManager
    {
        bool CheckExists(string filePath);
        ErrorCode DeleteFile(string filePath);
        string[] ListFilesFromPath(string filePath);
        ErrorCode StoreFile(string inputData, string outputPath);
    }
}
