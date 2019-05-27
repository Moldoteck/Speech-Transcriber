/**************************************************************************
 *                                                                        *
 *  File:        LocalFileManager.cs                                      *
 *  Copyright:   (c) 2019, Octavian Oprișan                               *
 *  Description: Implementation of IFileManager interface                 *
 *               LocalFileManager object can perform:                     *
 *               * Check if file exists on local machine                  *
 *               * Delete file from local machine                         *
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
using System.IO;

namespace Speech_Transcriber
{
    public class LocalFileManager : IFileManager
    {
        #region Implemented interface methods
        /// <summary>
        /// Checks if a file with the provided path exists
        /// </summary>
        /// <param name="fullFilePath">Full path to the file to be tested</param>
        /// <returns>Returns true if the file is found or false otherwise</returns>
        public override bool CheckExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        /// <summary>
        /// Deletes a file with the specified path
        /// </summary>
        /// <param name="filePath">Full path to the file to be deleted</param>
        /// <returns>Returns ErrorCode.SUCCESS if the file has been deleted, or
        /// ErrorCode.EXTERNAL_COMPONENT_ERROR if an exception is thrown during the process</returns>
        public override ErrorCode DeleteFile(string filePath)
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

        /// <summary>
        /// Returns a list with the files from the specified path
        /// </summary>
        /// <param name="filePath">full path to a directory</param>
        /// <returns>Returns a list of strings containing all the file names in the provided
        /// directory or null if an exception occured during the process</returns>
        public override string[] ListFilesFromPath(string filePath)
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

        /// <summary>
        /// Stores a file locally
        /// </summary>
        /// <param name="inputData">text to be written into the file</param>
        /// /// <param name="outputPath">the full path for the file to be written</param>
        /// <returns>Returns ErrorCode.SUCCESS if the file has been stored, or 
        /// ErrorCode.EXTERNAL_COMPONENT_ERROR if an exception occured during the process.</returns>
        public override ErrorCode StoreFile(string inputData, string outputPath)
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
