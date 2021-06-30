using System;
using NewsChronicle.Data.Constants;
using NewsChronicle.Data.Interfaces;

namespace NewsChronicle.Droid.Services
{
    public class DBFileAccessHelper : IDBFileAccessHelper
    {
        public DBFileAccessHelper()
        {
        }

        public string GetLocalFilePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, Constants.LocalDatabaseFileName);
        }
    }
}
