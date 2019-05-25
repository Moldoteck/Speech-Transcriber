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
        /// Converts audio from one format to another. Format should be determined by extension.
        /// </summary>
        /// <param name="filePath">Calea către și numele fișierului de intrare</param>
        /// <param name="outputFilePath">Calea către și numele fișierului de ieșire</param>
        int ConvertToFormat(string filePath, string outputFilePath);
        /// <summary>
        /// Converts stereo audio to mono audio file
        /// </summary>
        /// <param name="filePath">Calea către și numele fișierului de intrare</param>
        /// <param name="outputFilePath">Calea către și numele fișierului de ieșire</param>
        int StereoToMono(string filePath, string outputFilePath);
        int Rate(string filePath);
        #endregion
    }
}
