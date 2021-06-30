using System;
using NewsChronicle.Data.Constants;
using NewsChronicle.Data.Interfaces;

namespace NewsChronicle.Services
{
    public class DBFileAccessHelper : IDBFileAccessHelper
    {
        public DBFileAccessHelper()
        {
        }

        public string GetLocalFilePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = System.IO.Path.Combine(docFolder, "..", "Library");

            System.IO.Directory.CreateDirectory(libFolder);

            return System.IO.Path.Combine(libFolder, Constants.LocalDatabaseFileName);
        }
    }
}
