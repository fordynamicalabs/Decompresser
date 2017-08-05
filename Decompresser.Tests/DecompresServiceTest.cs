using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Decompresser.Tests
{
    [TestClass]
    public class DecompresServiceTest
    {
        #region Fields
        private readonly IDecompresService _dService;

        private static readonly string path = @"C:\Users\john_\Desktop\zipfile.zip";
        private static readonly string search_pattern = ".csv";
        #endregion

        #region Init
        public DecompresServiceTest()
        {
            _dService = new DecompresService(path, search_pattern, false);
        }
        #endregion


        #region Test Methods
        [TestMethod]
        public void TestDecompresService()
        {
            Assert.IsInstanceOfType(_dService, typeof(IDecompresService));
            Assert.IsNotNull(_dService);

            using (var archive = _dService.OpenRead())
            {
                Assert.IsNotNull(archive);
                _dService._zip = archive;
                Assert.AreEqual(archive, _dService._zip);

                var temp_directory = _dService.CreateTempDirectory();
                Assert.IsNotNull(temp_directory);

                _dService._temp_directory = temp_directory;
                Assert.AreEqual(temp_directory, _dService._temp_directory);

                try
                {
                    _dService.ExtractAll();
                    Assert.IsTrue(true);
                }
                catch (UnitTestAssertException ex)
                {
                    Assert.Fail(ex.Message);
                }

                try
                {
                    _dService.RemoveTempDirectory();
                    var is_temp_exist = Directory.Exists(temp_directory);
                    Assert.IsFalse(is_temp_exist);
                }
                catch (AssertFailedException ex)
                {
                    Assert.Fail(ex.Message);
                }

                _dService.Dispose();
            }
        }
        #endregion
    }
}
