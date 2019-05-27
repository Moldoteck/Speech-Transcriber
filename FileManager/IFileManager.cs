/**************************************************************************
 *                                                                        *
 *  File:        IFileManager.cs                                          *
 *  Copyright:   (c) 2019, Octavian Oprișan                               *
 *  Description: IFileManager interface                                   *
 *               It defines possible operations that                      *
 *               can be performed with files from                         *
 *               local file system                                        *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

namespace Speech_Transcriber
{
    public abstract class IFileManager
    {
        #region Interface methods
        /// <summary>
        /// Checks if a file with the provided path exists (the location depends on the implementation)
        /// </summary>
        /// <param name="fullFilePath">Full path to the file to be tested</param>
        /// <returns>Returns true if the file is found or false otherwise</returns>
        public abstract bool CheckExists(string filePath);

        /// <summary>
        /// Deletes a file with the specified path (the location depends on the implementation)
        /// </summary>
        /// <param name="filePath">Full path to the file to be deleted</param>
        /// <returns>Returns ErrorCode.STATE_INVALID if the StorageClient is not initialized,
        /// ErrorCode.EXTERNAL_COMPONENT_ERROR if an exception is thrown during the process, 
        /// or ErrorCode.SUCCESS if the file has been deleted</returns>
        public abstract ErrorCode DeleteFile(string filePath);

        /// <summary>
        /// Returns a list with the files from the specified path (the location depends on the implementation)
        /// </summary>
        /// <param name="path">Path of dir in cloud</param>
        /// <returns>Returns a list of strings containing all the file names in the provided directory</returns>
        public abstract string[] ListFilesFromPath(string filePath);

        /// <summary>
        /// Stores a file (the location depends on the implementation)
        /// </summary>
        /// <param name="inputData">text to be written into the file</param>
        /// /// <param name="outputFullPath">the full path for the file to be written</param>
        /// <returns>Returns ErrorCode.STATE_INVALID if the client is null, ErrorCode.NULL_ARGUMENT if at 
        /// least one of the arguments is null, ErrorCode.INVALID_ARGUMENT is the arguments are empty strings,
        /// ErrorCode.EXTERNAL_COMPONENT_ERROR if other errors occured, or ErrorCode.SUCCESS if the file 
        /// has been stored</returns>
        public abstract ErrorCode StoreFile(string inputData, string outputPath);
        #endregion
    }
}
