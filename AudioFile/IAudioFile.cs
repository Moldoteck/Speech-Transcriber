using System;
using System.Collections.Generic;
using System.Text;

namespace AudioFile
{
    interface IAudioFile
    {
        int GetChannel();
        int GetFrameRate();
    }
}
