/**************************************************************************
 *                                                                        *
 *  File:        IAudioFileConverter.cs                                   *
 *  Copyright:   (c) 2019, Octavian Oprișan                               *
 *  Description: IAudioFileConverter interface                            *
 *               It defines possible operations that                      *
 *               can be performed with audio files                        *
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
using System.Text;

namespace Speech_Transcriber
{
    interface IAudioFileConverter
    {
        #region Interface methods
        /// <summary>
        /// Converts audio from one format to another. Format should be determined by extension
        /// </summary>
        /// <param name="size">Dimensiunea pătratului</param>
        int ConvertToFormat(string filePath, string outputFilePath);
        int StereoToMono(string filePath, string outputFilePath);
        int Rate(string filePath);
        #endregion
    }
}
