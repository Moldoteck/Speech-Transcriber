/**************************************************************************
 *                                                                        *
 *  File:        IFileManagerFactory.cs                                   *
 *  Copyright:   (c) 2019, Octavian Oprișan                               *
 *  Description: Is an abstract class which acts as an interface,         *
 *               forcing the subclasses to implement the                  *
 *               GetFileManager method.                                   *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Speech_Transcriber
{
    /// <summary>  
    /// The 'IFileManagerFactory' Abstract Class: specifies how the Factory will look
    /// </summary> 
    public abstract class IFileManagerFactory
    {
        /// <summary>
        /// Get the file manager corresponding to the parameter passed
        /// </summary>
        /// <param name="jsonPath">Path to json file that contains information for OAuth module</param>
        /// <returns>Returns an IFileManager object, that is instance of LocalFileManager if the argument is null,
        /// CloudFileManager if the path exists, or null otherwise</returns>
        public abstract IFileManager GetFileManager(string jsonPath=null);
    }
}
