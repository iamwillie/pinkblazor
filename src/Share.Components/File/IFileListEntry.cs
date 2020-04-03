using System;
using System.IO;

namespace PinkBlazor
{
    /// <summary>
    /// File list entry interface
    /// </summary>
    public interface IFileListEntry
    {
        /// <summary>
        /// File last modified date
        /// </summary>
        DateTime LastModified { get; }

        /// <summary>
        /// File name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// File size
        /// </summary>
        long Size { get; }

        /// <summary>
        /// File type
        /// </summary>
        string Type { get; }

        /// <summary>
        /// File data
        /// </summary>
        Stream Data { get; }

        /// <summary>
        /// On data read event handler
        /// </summary>
        event EventHandler OnDataRead;
    }
}