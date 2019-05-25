using HelperStructures;

namespace Speech_Transcriber
{
    interface IFileManager
    {
        #region Interface methods
        bool CheckExists(string filePath);
        ErrorCode DeleteFile(string filePath);
        string[] ListFilesFromPath(string filePath);
        ErrorCode StoreFile(string inputData, string outputPath);
        #endregion
    }
}
