using System;
using SQLite.Net;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(BuscaPorVoz.Droid.SQLiteAccess_Droid))]
namespace BuscaPorVoz.Droid
{
    public class SQLiteAccess_Droid : ISQLite
    {
        public SQLiteAccess_Droid()
        {
        }

        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "BuscaPorVoz.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var conn = new SQLiteConnection(plat, path);
            return conn;
        }
    }
}

