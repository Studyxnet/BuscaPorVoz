using System;
using SQLite.Net;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(BuscaPorVoz.iOS.SQLiteAccess_iOS))]
namespace BuscaPorVoz.iOS
{
    public class SQLiteAccess_iOS : ISQLite
    {
        public SQLiteAccess_iOS()
        {
        }

        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "BuscaPorVoz.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var conn = new SQLiteConnection(plat, path);
            return conn;
        }
    }
}

