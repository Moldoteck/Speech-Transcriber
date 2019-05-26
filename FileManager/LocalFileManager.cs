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
        public override bool CheckExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

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
