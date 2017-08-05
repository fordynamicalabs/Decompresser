using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Decompresser
{
    /// <summary>
    /// Service for decompressing archives
    /// </summary>
    public class DecompresService : IDecompresService
    {
        #region Fields

        public ZipArchive _zip { get; set; }
        private List<string> _extracted_files;
        private string _temp_directory;

        List<string> IDecompresService._extracted_files
        {
            get
            {
                return _extracted_files;
            }

            set
            {
                _extracted_files = value;
            }
        }

        string IDecompresService._temp_directory
        {
            get
            {
                return _temp_directory;
            }

            set
            {
                _temp_directory = value;
            }
        }

        private readonly string _path_from;
        private readonly string _search_pattern;
        private readonly bool _auto_play;

        #endregion


        #region Init

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path_from">path to compressed file</param>
        /// <param name="search_pattern">search pattern for specifying wich files we want to extract</param>
        /// <param name="auto_play">if true - creating temp directory and reading archive will start automatically</param>
        public DecompresService(string path_from, string search_pattern, bool auto_play)
        {
            _path_from = path_from;
            _search_pattern = search_pattern;
            _extracted_files = new List<string>();
            _auto_play = auto_play;

            if (_auto_play)
            {
                //Open archive
                _zip = OpenRead();
                //Creating Temp Directory
                _temp_directory = CreateTempDirectory();
            }

        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Opens a zip archive for reading in the specified path
        /// </summary>
        /// <returns>ZipFile</returns>
        public ZipArchive OpenRead()
        {
            return ZipFile.OpenRead(_path_from);
        }
   
        /// <summary>
        /// Loop entries and Extract it to specified directory
        /// </summary>
        public void ExtractAll()
        {
            //Files extracting
            foreach (ZipArchiveEntry entry in _zip.Entries.Where(q => q.Name.EndsWith(_search_pattern, StringComparison.OrdinalIgnoreCase)))
            {
                //1 - Extract every file
                ExtractFile(entry);
            }

            //Empty method for every file
            foreach (var path in _extracted_files)
            {
                //2 - Run Empty method for file
                SomeMethod(path);
            }

            //3 - After all Remove Temp Directory
            if (_auto_play)
            {
                RemoveTempDirectory();
            }
        }

        /// <summary>
        /// Create Temporary directory for files saving
        /// </summary>
        /// <returns></returns>
        public string CreateTempDirectory()
        {
            try
            {
                string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(path);

                Console.WriteLine(string.Format("\nTemporary Directory is '{0}'.", path));
                return path;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("\nCouldn't create Temporary Directory.\n Message: '{0}'.", ex.Message));
                throw new Exception(string.Format("Couldn't create Temporary Directory.\n Message: '{0}'.", ex.Message), ex);
            }
        }

        /// <summary>
        /// Remove Temporary Directory
        /// </summary>
        public void RemoveTempDirectory()
        {
            try
            {
                if (Directory.Exists(_temp_directory))
                {
                    Directory.Delete(_temp_directory, true);
                    Console.WriteLine("Temporary directory has been deleted");
                }
                else
                    Console.WriteLine(string.Format("Directory with name '{0}' is not exist", _temp_directory));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Couldn't delete temporary directory.\n Message: '{0}", ex.Message));
            }
        }

        #endregion
 

        #region Private Methods

        /// <summary>
        /// Save entry as file to specified directory
        /// </summary>
        /// <param name="_entry">entry of ZipAcrhive</param>
        /// <returns></returns>
        private void ExtractFile(ZipArchiveEntry _entry)
        {
            Console.WriteLine("\nTrying to extract file '{0}'...", _entry.FullName.ToString());
            try
            {
                _entry.ExtractToFile(Path.Combine(_temp_directory, _entry.Name));
                //Add file name to saved files list
                _extracted_files.Add(_entry.Name);
                Console.WriteLine("File '{0}' has been extracted successfully!", _entry.FullName.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error while extracting file '{0}'.\n Message: '{1}'.", _entry.Name, ex.Message));
                //throw new Exception(string.Format("Error while extracting file '{0}'", entry.Name), ex);
            }
        }

        /// <summary>
        /// Empty Method
        /// </summary>
        /// <param name="file_path">path to saved file</param>
        private void SomeMethod(string file_path)
        {
            try
            {
                Console.WriteLine(string.Format("\nSome method for file '{0}' has been successfully called", file_path));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("\nCouldn't load Some Method for file '{0}'.", file_path), ex);
            }

        }

        #endregion


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _zip.Dispose();
                    _extracted_files = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DecompresService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
