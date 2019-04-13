using System;
using System.Collections.Generic;
using System.Text;

namespace AudioFileConverter
{
    interface IAudioFileConverter
    {
        int ConvertToFormat(string filePath, string outputFilePath);
        int StereoToMono(string filePath, string outputFilePath);
    }
}
