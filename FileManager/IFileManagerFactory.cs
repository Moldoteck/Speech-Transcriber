using System;
using System.Collections.Generic;
using System.Text;

namespace Speech_Transcriber
{
    /// <summary>  
    /// The 'Creator' Abstract Class  
    /// </summary> 
    public abstract class IFileManagerFactory
    {
        public abstract IFileManager GetFileManager(string jsonPath=null);
    }
}
