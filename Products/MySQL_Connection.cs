using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Products
{
    public class MySQL_Connection
    {
        // Connection String for MYSQL database
        private static string myConnectionString = "server=localhost; uid=root; pwd=alvan****; database=school_supplydb";

        public static MySqlConnection GetConnection() 
        {
            return new MySqlConnection(myConnectionString);
        }

        public static void HandleException(MySqlException ex)
        {
            Console.WriteLine("MySQL Error:" + ex.Message);
            Console.WriteLine("MySQL Error:" + ex.ErrorCode);
            Console.WriteLine("MySQL Error:" + ex.SqlState);
        }

    }
}
