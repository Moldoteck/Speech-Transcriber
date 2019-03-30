using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Cloud.Storage.V1;


namespace FileManager
{
    class CloudFileManager : IFileManager
    {

        StorageClient _storageClient;
        public CloudFileManager(string jsonPath)
        {
            var credential = GoogleCredential.FromFile(jsonPath);
            _storageClient = StorageClient.Create(credential);
        }
        public void CheckExists(string filePath, string fileName)
        {

            //var name = 'file_i_want_to_check.txt'; 
            //var storage_client = storage.Client();
            //var = 'my_bucket_name';
            
            //var bucketName = _storageClient.GetBucket(filePath);
            //_storageClient.Ex
            //var stats = storage.Blob(bucket = bucketName, name = fileName).exists(storage_client);

        }
    public void DeleteFile(string filePath, string fileName)
    {

    }
    public void ListFilesFromPath(string path)
    {

    }
    public void GetFileContent(string filePath, string fileName)
    {

    }
    public void StoreFile(string inputData, string outputPath, string outputFileName)
    {

    }
}
}
