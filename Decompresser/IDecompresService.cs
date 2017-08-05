using System;
using System.IO.Compression;

namespace Decompresser
{
    /// <summary>
    /// Interface for DecompressService
    /// </summary>
    public interface IDecompresService : IDisposable
    {
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
