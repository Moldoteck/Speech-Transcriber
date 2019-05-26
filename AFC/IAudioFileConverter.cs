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
        /// <param name="filePath">File name and path to input file</param>
        /// <param name="outputFilePath">File name and path to output file</param>
        /// <returns>Returns 1 in case of success or -1 in case of error</returns>
        int ConvertToFormat(string filePath, string outputFilePath);

        /// <summary>
        /// Converts stereo audio to mono audio file
        /// </summary>
        /// <param name="filePath">File name and path to input file</param>
        /// <param name="outputFilePath">File name and path to output file</param>
        /// <returns>Returns 1 in case of success or -1 in case of error</returns>
        int StereoToMono(string filePath, string outputFilePath);

        /// <summary>
        /// Converts stereo audio to mono audio file
        /// </summary>
        /// <param name="filePath">File name and path to audio file</param>
        /// <returns>Returns frame rate of audio file or -1 in case of error</returns>
        int Rate(string filePath);
        #endregion
    }
}
