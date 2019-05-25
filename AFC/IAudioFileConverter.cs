using System;
using System.Collections.Generic;
using System.Text;

namespace Speech_Transcriber
{
    interface IAudioFileConverter
    {
        #region Interface methods
        int ConvertToFormat(string filePath, string outputFilePath);
        int StereoToMono(string filePath, string outputFilePath);
        int Rate(string filePath);
        #endregion
    }
}
