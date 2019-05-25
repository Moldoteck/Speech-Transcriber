using System;
using System.Collections.Generic;
using System.Text;

namespace AFC
{
    interface IAudioFileConverter
    {
        int ConvertToFormat(string filePath, string outputFilePath);
        int StereoToMono(string filePath, string outputFilePath);
    }
}
