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
