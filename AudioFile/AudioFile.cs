using System;

namespace AudioFile
{
    public class AudioFile : IAudioFile
    {
        private string _filePath;

        public int GetChannel()
        {
            throw new NotImplementedException();
        }

        public int GetFrameRate()
        {
            throw new NotImplementedException();
        }
    }
}
