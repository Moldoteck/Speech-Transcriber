/**************************************************************************
 *                                                                        *
 *  File:        FileManager.cs                                          *
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
        public abstract bool CheckExists(string filePath);
        public abstract ErrorCode DeleteFile(string filePath);
        public abstract string[] ListFilesFromPath(string filePath);
        public abstract ErrorCode StoreFile(string inputData, string outputPath);
        #endregion
    }
}
