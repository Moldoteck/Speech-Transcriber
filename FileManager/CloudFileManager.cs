using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Speech_Transcriber
{
    public class CloudFileManager : IFileManager
    {

        StorageClient _storageClient;
        public CloudFileManager(string jsonPath)
        {
            try
            {
                var credential = GoogleCredential.FromFile(jsonPath);
                _storageClient = StorageClient.Create(credential);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        public bool CheckExists(string fullFilePath)
        {
            var fileName = Path.GetFileName(fullFilePath);
            var directoryPath = Path.GetDirectoryName(fullFilePath);

            var listOfObjects = ListFilesFromPath(directoryPath);
            try
            {
                return Array.IndexOf(listOfObjects, fileName) == -1 ? false : true;
            }
            catch (ArgumentNullException exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }
        }

        public ErrorCode DeleteFile(string filePath)
        {
            if (_storageClient == null)
            {
                return ErrorCode.STATE_INVALID;
            }

            string fileName, filepath;
            try
            {
                fileName = Path.GetFileName(filePath);
                filepath = Path.GetDirectoryName(filePath);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return ErrorCode.EXTERNAL_COMPONENT_ERROR;
            }

            if (CheckExists(filePath))
            {
                try
                {
                    _storageClient.DeleteObject(filepath, fileName);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    return ErrorCode.EXTERNAL_COMPONENT_ERROR;
                }
            }

            return ErrorCode.SUCCESS;
        }
        public string[] ListFilesFromPath(string path)
        {
            try
            {
                var listOfObjects = _storageClient.ListObjects(path);
                List<string> files = new List<string>();
                foreach (var file in listOfObjects)
                {
                    files.Add(file.Name);
                }
                return files.ToArray();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
        }

        public ErrorCode StoreFile(string inputData, string outputFullPath)
        {
            if (_storageClient == null)
            {
                return ErrorCode.STATE_INVALID;
            }

            if (inputData == null || outputFullPath == null)
            {
                Console.WriteLine($"One of provided arguments: output path:{outputFullPath}" +
                    $", input path:{inputData} is null");
                return ErrorCode.NULL_ARGUMENT;
            }
            if (inputData == "" || outputFullPath == "")
            {
                Console.WriteLine($"One of provided arguments: output path:{outputFullPath}" +
                    $", input path:{inputData} is empty string");
                return ErrorCode.INVALID_ARGUMENT;
            }

            string outputFileName, outputfilepath;
            try
            {
                outputFileName = Path.GetFileName(outputFullPath);
                outputfilepath = Path.GetDirectoryName(outputFullPath);
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                return ErrorCode.EXTERNAL_COMPONENT_ERROR;
            }

            if (!CheckBucket(outputfilepath))
            {
                Console.WriteLine($"Bucket {outputfilepath} does not exists");
                return ErrorCode.INVALID_ARGUMENT;
            }
            if (!File.Exists(inputData))
            {
                Console.WriteLine($"Provided source file path:{inputData} does not exist");
                return ErrorCode.INVALID_ARGUMENT;
            }

            if (CheckExists(outputFullPath))
            {
                Console.WriteLine($"File with name {outputFileName} already exists");
                DeleteFile(outputFullPath);
                Console.WriteLine($"Deleted old {outputFileName} file");
            }

            try
            {
                using (var f = File.OpenRead(inputData))
                {
                    Console.WriteLine($"Started uploading {outputFileName}.");
                    _storageClient.UploadObject(outputfilepath, outputFileName, null, f);
                    Console.WriteLine($"Uploaded {outputFileName}.");
                    return ErrorCode.SUCCESS;
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                return ErrorCode.EXTERNAL_COMPONENT_ERROR;
            }
        }

        private bool CheckBucket(string bucketName)
        {
            try
            {
                var bucketInfo = _storageClient.GetBucket(bucketName);
                return bucketInfo != null;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }
        }
    }
}
