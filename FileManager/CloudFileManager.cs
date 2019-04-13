using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Cloud.Storage.V1;
//using HelperStructures;

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
        public bool CheckExists(string filePath)
        {
            var listPath = filePath.Split('/');
            var fileName = listPath[listPath.Length - 1];
            var found = filePath.IndexOf("/" + fileName);
            var filepath = filePath.Substring(0, found);

            var listOfObjects = _storageClient.ListObjects(filepath);
            foreach (var elem in listOfObjects)
            {
                if (elem.Name.Equals(fileName))
                {
                    return true;
                }
            }
            return false;
        }

        public int DeleteFile(string filePath)
        {
            var listPath = filePath.Split('/');
            var fileName = listPath[listPath.Length - 1];
            var found = filePath.IndexOf("/" + fileName);
            var filepath = filePath.Substring(0, found);

            var listOfObjects = _storageClient.ListObjects(filepath);
            if (CheckExists(filePath))
            {
                _storageClient.DeleteObject(filepath, fileName);
            }
            return 0;
        }
        public string[] ListFilesFromPath(string path)
        {
            var listOfObjects = _storageClient.ListObjects(path);
            List<string> obj = new List<string>();
            foreach (var elem in listOfObjects)
            {
                obj.Add(elem.Name);
            }
            return obj.ToArray();
        }

        public int StoreFile(string inputData, string outputPath)
        {
            if (inputData == null || outputPath == null)
            {
                Console.WriteLine($"One of provided arguments: path:{outputPath}, fileName:{inputData} is null");
                return -1;
                //return ErrorCode.NULL_ARGUMENT;
            }
            if (inputData == "" || outputPath == "")
            {
                Console.WriteLine($"One of provided arguments: path:{outputPath}, fileName:{inputData} is empty string");
                return -1;
                //return ErrorCode.INVALID_ARGUMENT;
            }
            if (!CheckBucket(outputPath))
            {
                Console.WriteLine($"Bucket {inputData} does not exists");
                return -1;
                //return ErrorCode.INVALID_ARGUMENT;
            }
            if (!File.Exists(inputData))
            {
                Console.WriteLine($"Provided source file path:{outputPath} does not exist");
                return -1;
                //return ErrorCode.INVALID_ARGUMENT;
            }
            var fileName = inputData.Split('/')[inputData.Split('/').Length - 1].Split('.')[0];
            if (CheckExists(outputPath + fileName))
            {
                Console.WriteLine($"File with name {fileName} already exists");
                DeleteFile(outputPath+ fileName);
                Console.WriteLine($"Deleted old {fileName} file");
            }
            using (var f = File.OpenRead(inputData))
            {
                Console.WriteLine($"Started uploading {fileName}.");
                _storageClient.UploadObject(outputPath, fileName, null, f);
                Console.WriteLine($"Uploaded {fileName}.");
                return 0;
                //return ErrorCode.SUCCESS;
            }
        }

        private bool CheckBucket(string bucketName)
        {
            var bucketInfo = _storageClient.GetBucket(bucketName);
            return bucketInfo != null;
        }
    }
}
