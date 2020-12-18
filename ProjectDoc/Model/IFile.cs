using System;
using System.IO;

namespace ProjectDoc.Model
{
    /// <summary>
    /// Exposes the basic members for file represented instance.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// The directory of a file.
        /// </summary>
        String Directory
        {
            get;
            set;
        }

        /// <summary>
        /// The name of a file.
        /// </summary>
        String Name
        {
            get;
            set;
        }

        /// <summary>
        /// The extension of a file.
        /// </summary>
        String Extension
        {
            get;
            set;
        }

        /// <summary>
        /// The file watcher.
        /// </summary>
        FileSystemWatcher Watcher
        {
            get;
        }

        /// <summary>
        /// Read the content of a file to the stream.
        /// </summary>
        void Open();

        /// <summary>
        /// Close and release the open stream.
        /// </summary>
        void Close();
    }
}
