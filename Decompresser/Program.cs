using System;

namespace Decompresser
{
    class Program
    {
        #region Fields
        private static IDecompresService _dService;

        static string path = @"C:\Users\john_\Desktop\zipfile.zip";
        static string search_pattern = ".csv";
        #endregion

        #region Start Point
        static void Main(string[] args)
        {
            ProcessNames();

            _dService = new DecompresService(path, search_pattern);
            _dService.ExtractAll();
           
            Console.WriteLine("\nPress enter to finish.");
            Console.ReadLine();
        }
        #endregion

        #region Private Methods
        private static void ProcessNames()
        {
            Console.WriteLine("\nPlease enter path to zip file");
            path = Console.ReadLine();
            Console.WriteLine("Path to zip is: '{0}'", path);

            Console.WriteLine("\nPlease enter search pattern for extracting");
            search_pattern = Console.ReadLine();
            Console.WriteLine("Search pattern is: '{0}'", search_pattern);
        }
        #endregion

    }


}
