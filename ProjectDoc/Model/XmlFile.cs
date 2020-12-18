using System;
using System.IO;

using ProjectDoc.Operations;

namespace ProjectDoc.Model
{
    /// <summary>
    /// Represents the XML file in which is saved D-Bus-2 configuration.
    /// </summary>
    internal class XmlFile : CompositeProjectElement, IFile
    {
        #region Private Members

        /// <summary>
        /// The stream of a file.
        /// </summary>
        private FileStream _file;
        /// <summary>
        /// The file watcher.
        /// </summary>
        private FileSystemWatcher _watcher;

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the directory of the file.
        /// </summary>
        public String Directory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the file name.
        /// </summary>
        public override string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the directory of the file.
        /// </summary>
        public String Extension
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the full path of the file.
        /// </summary>
        public String FullPath
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the system file watcher.
        /// </summary>
        public FileSystemWatcher Watcher
        {
            get
            {
                return _watcher;
            }
        }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the XmlFile class.
        /// </summary>
        public XmlFile()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Read the content the file to the stream.
        /// </summary>
        public void Open()
        {
            this.FullPath = this.Directory + this.Name + this.Extension;
            if (File.Exists(this.FullPath))
            {
                FileAttributes attr = File.GetAttributes(this.FullPath);
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    _file = new FileStream(this.FullPath, FileMode.Open, FileAccess.Read);
                }
                else
                {
                    _file = new FileStream(this.FullPath, FileMode.Open, FileAccess.ReadWrite);
                }
            }
            else
            {
                _file = new FileStream(this.FullPath, FileMode.Create, FileAccess.ReadWrite);
            }
            this.StartWatcher();
        }

        /// <summary>
        /// Closes the XmlFile instance and the underlying stream.
        /// </summary>
        public void Close()
        {
            this.StopWatcher();
            this.Dispose();     // dispose image of this element
        }

        /// <summary>
        /// Executes the specific operation over a XmlFile instance.
        /// </summary>
        /// <param name="visitor">The operation to be execute.</param>
        public override void Accept(IProjectOperation visitor)
        {
            visitor.VisitXmlFile(this);
        }

        /// <summary>
        /// Deletes all child ProjectElement instances from the inner list.
        /// </summary>
        public override void Clear()
        {
            this.Close();
            base.Clear();
        }

        #endregion

        #region Protected Methods

        protected override void DisposeUnmanagedResources()
        {
            base.DisposeUnmanagedResources();
            if (_file != null)
            {
                _file.Close();
                _file = null;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Begin monitoring the file.
        /// </summary>
        private void StartWatcher()
        {
            // start the watcher
            if (_watcher == null)
                _watcher = new FileSystemWatcher();
            // default NotifyFilers are NotifyFilters.LastWrite | NotifyFilters.DirectoryName | NotifyFilters.FileName
            _watcher.Path = this.FullPath;
            _watcher.Filter = this.Name + this.Extension;
            _watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Stop monitoring the file.
        /// </summary>
        private void StopWatcher()
        {
            if (_watcher != null)
                _watcher.EnableRaisingEvents = false;
        }

        #endregion
    }
}
