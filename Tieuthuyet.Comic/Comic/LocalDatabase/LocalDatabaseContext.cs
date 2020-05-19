using Comic.Model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comic.LocalDatabase
{
    public class LocalDatabaseContext:DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public static string DBConnectionString = "Data Source=isostore:/ComicDb.sdf";

        // Pass the connection string to the base class.
        public LocalDatabaseContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a single table for the to-do items.
        public Table<UserHistories> UserHistories;
    }
}
