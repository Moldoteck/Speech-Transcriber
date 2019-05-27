/**************************************************************************
 *                                                                        *
 *  File:        CloudFileManager.cs                                      *
 *  Copyright:   (c) 2019, Cristian Pădureac                              *
 *  Description: Implementation of IFileManager interface                 *
 *               CloudFileManager object can perform:                     *
 *               * Check if file exists on cloud storage                  *
 *               * Delete file from cloud storage                         *
 *               * List all files from given path                         *
 *               * Store file at specific path                            *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Speech_Transcriber
{
    public class CloudFileManager : IFileManager
    {
        #region Private members
        StorageClient _storageClient = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jsonPath">Path to json file that contains information for OAuth module</param>
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
        #endregion

        #region Implemented interface methods
        /// <summary>
        /// Checks if a file with the provided path exists
        /// </summary>
        /// <param name="fullFilePath">Full path to the file to be tested</param>
        /// <returns>Returns true if the file is found or false otherwise</returns>
        public override bool CheckExists(string fullFilePath)
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

        /// <summary>
        /// Deletes a file with the specified path from the cloud (if exists)
        /// </summary>
        /// <param name="filePath">Full path to the file to be deleted</param>
        /// <returns>Returns ErrorCode.STATE_INVALID if the StorageClient is not initialized,
        /// ErrorCode.EXTERNAL_COMPONENT_ERROR if an exception is thrown during the process, 
        /// or ErrorCode.SUCCESS if the file has been deleted</returns>
        public override ErrorCode DeleteFile(string filePath)
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

        /// <summary>
        /// Returns a list with the files from the specified path in cloud
        /// </summary>
        /// <param name="path">Path of dir in cloud</param>
        /// <returns>Returns a list of strings containing all the file names in the provided directory</returns>
        public override string[] ListFilesFromPath(string path)
        {
            if (_storageClient == null)
            {
                Console.WriteLine("Client is null");
                return null;
            }

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

        /// <summary>
        /// Stores a file in the cloud
        /// </summary>
        /// <param name="inputData">text to be written into the file</param>
        /// /// <param name="outputFullPath">the full path for the file to be written in the cloud</param>
        /// <returns>Returns ErrorCode.STATE_INVALID if the client is null, ErrorCode.NULL_ARGUMENT if at 
        /// least one of the arguments is null, ErrorCode.INVALID_ARGUMENT is the arguments are empty strings,
        /// ErrorCode.EXTERNAL_COMPONENT_ERROR if other errors occured, or ErrorCode.SUCCESS if the file 
        /// has been stored</returns>
        public override ErrorCode StoreFile(string inputData, string outputFullPath)
        {
            if (_storageClient == null)
            {
                Console.WriteLine("Client is null");
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
        #endregion

        #region Private methods
        /// <summary>
        /// Checks if bucket exists in the cloud
        /// </summary>
        /// <param name="bucketName">name of the bucket to be tested</param>
        /// <returns>Returns true if the bucket is found or false otherwise</returns>
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
        #endregion
    }
}
