using System;
using System.Collections.Generic;
using System.Text;

namespace Speech_Transcriber
{
    public interface ITranscriber
    {
        #region Interface methods
        string TranscribeAudioFile(string cloudFilePath, int timeout, bool deleteAfter, string[] hints, string language, int frameRate, int numberOfSpeakers = 1);
        #endregion
    }
}
