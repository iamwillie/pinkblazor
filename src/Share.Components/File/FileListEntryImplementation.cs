using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PinkBlazor
{
    /// <summary>
    /// This is public only because it's used in a JSInterop method signature,
    /// but otherwise is intended as internal
    /// </summary>
    public class FileListEntryImplementation : IFileListEntry
    {
        #region Members
        private Stream _stream;
        #endregion

        #region Constructor

        #endregion

        #region Public
        /// <summary>
        /// On data read event handler
        /// </summary>
        public event EventHandler OnDataRead;

        /// <summary>
        /// File Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// File last modified date
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File size
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// File type
        /// </summary>
        public string Type { get; set; }

        public Stream Data
        {
            get
            {
                _stream ??= Owner.OpenFileStream(this);
                return _stream;
            }
        }

        internal void RaiseOnDataRead()
        {
            OnDataRead?.Invoke(this, null);
        }
        #endregion

        #region Internal
        /// <summary>
        /// File - owner
        /// </summary>
        internal File Owner { get; set; }
        #endregion
    }
}
