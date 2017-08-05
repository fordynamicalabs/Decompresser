using System;
using System.Collections.Generic;
using System.IO.Compression;

namespace Decompresser
{
    /// <summary>
    /// Interface for DecompressService
    /// </summary>
    public interface IDecompresService : IDisposable
    {
        /// <summary>
        /// Archive 
        /// </summary>
        ZipArchive _zip { get; set; }
        /// <summary>
        /// List with extracted files form archive
        /// </summary>
        List<string> _extracted_files { get; set; }
        /// <summary>
        /// Temporary directory for extracted files
        /// </summary>
        string _temp_directory { get; set; }
        /// <summary>
        /// Open and read compressed file(acrchive)
        /// </summary>
        /// <returns>Return archive as ZipArchive</returns>
        ZipArchive OpenRead();
        /// <summary>
        /// Extract all specified entries and save it as files
        /// </summary>
        void ExtractAll();
        /// <summary>
        /// Create temporary Directory
        /// </summary>
        /// <returns>Return path to created temporary Direcory</returns>
        string CreateTempDirectory();
        /// <summary>
        /// Delete temporary Directory
        /// </summary>
        void RemoveTempDirectory();

    }
}
