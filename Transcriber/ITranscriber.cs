/**************************************************************************
 *                                                                        *
 *  File:        ITranscriber.cs                                          *
 *  Copyright:   (c) 2019, Cristian Pădureac                              *
 *  Description: ITranscriber interface                                   *
 *               It defines possible ways of performing transcribing      *
 *               for audio file.                                          *
 *               Google / Amazon / Watson API can be used                 *
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
    public interface ITranscriber
    {
        #region Interface methods
        /// <summary>
        /// Recognizes the text from audio file
        /// </summary>
        /// <param name="cloudFilePath">Path to audio file that should be recognized</param>
        /// <param name="timeout">Timeout for waiting the response</param>
        /// <param name="deleteAfter">Bool that indicate if file should be deleted from cloud after recognizing</param>
        /// <param name="hints">Array of strings that contain special words that are not in language's dictionary</param>
        /// <param name="language">Language of spoken words in audio file</param>
        /// <param name="frameRate">Frame rate of audio file</param>
        /// <param name="numberOfSpeakers">Number of speakers from recorded audio file</param>
        /// <returns>
        /// Recognized text from audio file
        /// </returns>
        string TranscribeAudioFile(
            string cloudFilePath, 
            int timeout, bool deleteAfter, 
            string[] hints, 
            string language, 
            int frameRate, 
            int numberOfSpeakers = 1);
        #endregion
    }
}
