using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Speech_Transcriber
{
    public class FileManagerFactory : IFileManagerFactory
    {
        public override IFileManager GetFileManager(string jsonPath=null)
        {
            if(jsonPath == null)
                return new LocalFileManager();
            else
            {
                if (File.Exists(jsonPath))
                    return new CloudFileManager(jsonPath);
                else
                    return null;
            }
        }
    }
}
