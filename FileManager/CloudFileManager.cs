using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Cloud.Storage.V1;
using HelperStructures;

namespace FileManager
{
    public class CloudFileManager : IFileManager
    {

        StorageClient _storageClient;
        public CloudFileManager(string jsonPath)
        {
            var credential = GoogleCredential.FromFile(jsonPath);
            _storageClient = StorageClient.Create(credential);
        }
        public bool CheckExists(string filePath, string fileName)
        {
            var listOfObjects = _storageClient.ListObjects(filePath);
            foreach (var elem in listOfObjects)
            {
                if (elem.Name.Equals(fileName))
                {
                    return true;
                }
            }
            return false;
        }
        public void DeleteFile(string filePath, string fileName)
        {
            var listOfObjects = _storageClient.ListObjects(filePath);
            if (CheckExists(filePath, fileName))
            {
                _storageClient.DeleteObject(filePath, fileName);
            }
        }
        public List<string> ListFilesFromPath(string path)
        {
            List<string> obj = new List<string>();
            var listOfObjects = _storageClient.ListObjects(path);
            foreach (var elem in listOfObjects)
            {
                obj.Add(elem.Name);
            }
            return obj;
        }
        public void GetFileContent(string filePath, string fileName, string outputFilePath = null)
        {
            if (CheckExists(filePath, fileName))
            {
                outputFilePath = outputFilePath ?? Path.GetFileName(fileName);//fileName should be replaced
                using (var outputFile = File.OpenWrite(outputFilePath))
                {
                    _storageClient.DownloadObject(filePath, fileName, outputFile);
                }
                Console.WriteLine($"downloaded {fileName} to {outputFilePath}.");
            }
            else
            {
                Console.WriteLine($"{fileName} does not exist in {filePath}.");
            }
        }
        public ErrorCode StoreFile(string inputData, string outputPath, string outputFileName = null)
        {
            if (outputFileName == null || outputPath == null)
            {
                Console.WriteLine($"One of provided arguments: path:{outputPath}, fileName:{outputFileName} is null");
                return ErrorCode.NULL_ARGUMENT;
            }
            if (outputFileName == "" || outputPath == "")
            {
                Console.WriteLine($"One of provided arguments: path:{outputPath}, fileName:{outputFileName} is empty string");
                return ErrorCode.INVALID_ARGUMENT;
            }
            if (!CheckBucket(outputPath))
            {
                Console.WriteLine($"Bucket {outputFileName} does not exists");
                return ErrorCode.INVALID_ARGUMENT;
            }
            if (!File.Exists(inputData))
            {
                Console.WriteLine($"Provided source file path:{outputPath} does not exist");
                return ErrorCode.INVALID_ARGUMENT;
            }
            if (CheckExists(outputPath, outputFileName))
            {
                Console.WriteLine($"File with name {outputFileName} already exists");
                DeleteFile(outputPath, outputFileName);
                Console.WriteLine($"Deleted old {outputFileName} file");
            }
            using (var f = File.OpenRead(inputData))
            {
                Console.WriteLine($"Started uploading {outputFileName}.");
                _storageClient.UploadObject(outputPath, outputFileName, null, f);
                Console.WriteLine($"Uploaded {outputFileName}.");
                return ErrorCode.SUCCESS;
            }
        }

        private bool CheckBucket(string bucketName)
        {
            var bucketInfo = _storageClient.GetBucket(bucketName);
            return bucketInfo != null;
        }
    }
}
