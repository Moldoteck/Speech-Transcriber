using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Speech_Transcriber
{
    public class LocalFileManager : IFileManager
    {
        #region Implemented interface methods
        public bool CheckExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        public ErrorCode DeleteFile(string filePath)
        {
            try
            {
                System.IO.File.Delete(filePath);
                return ErrorCode.SUCCESS;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return ErrorCode.EXTERNAL_COMPONENT_ERROR;
            }
        }

        public string[] ListFilesFromPath(string filePath)
        {
            try
            {
                return System.IO.Directory.GetFiles(filePath);
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
        }

        public ErrorCode StoreFile(string inputData, string outputPath)
        {
            try
            {
                File.WriteAllText(outputPath, inputData);
                return ErrorCode.SUCCESS;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return ErrorCode.EXTERNAL_COMPONENT_ERROR;
            }
        }
        #endregion
    }
}
