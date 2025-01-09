using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projekt_OOP
{
    public class dbConnect
    {
        private string ConnectionString;

        public dbConnect(string server, string database, string user, string password)
        {
            ConnectionString = $"Server={server};Database={database};User ID={user};Password={password};";
        }

        public MySqlConnection GetConnection()
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                Console.WriteLine("Udało się połączyć z bazą danych :)");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd połączenia z bazą danych: {ex.Message}");
                throw;
            }
        }
    }
}
