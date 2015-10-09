using System;
using SQLite.Net;

namespace BuscaPorVoz
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}

