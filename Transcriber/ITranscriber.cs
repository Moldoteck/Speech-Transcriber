using HelperStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Speech_Transcriber
{
    public interface ITranscriber
    {
        string TranscribeAudioFile(string cloudFilePath, int timeout, bool deleteAfter, string[] hints, string language, int frameRate, int numberOfSpeakers = 1);
    }
}
