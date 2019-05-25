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
        string TranscribeAudioFile(string cloudFilePath, int timeout, bool deleteAfter, string[] hints, string language, int frameRate, int numberOfSpeakers = 1);
        #endregion
    }
}
