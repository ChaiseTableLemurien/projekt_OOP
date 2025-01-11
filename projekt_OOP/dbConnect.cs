using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projekt_OOP
{
    public class dbConnect
    {
        private string ConnectionString = "Server=localhost;Database=kurnik_test;User ID=root;Password=''";
        private MySqlConnection connection;

        public dbConnect()
        {
        }

        public MySqlConnection CreateConnection()
        {
            try
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();
                //Console.WriteLine("\nNawiązano połączenie z bazą danych\n");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd połączenia z bazą danych: {ex.Message}");
                throw;
            }
        }

        public void CloseConnection()
        {
            connection.Close();
            //Console.WriteLine("\nZakończono połączenie z bazą danych\n");
        }
        
        public void Select()
        {
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM kury;";
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // Wypisanie wszystkich kolumn w jednym wierszu. 
                // Możesz dostosować to do swoich potrzeb, np. wypisując tylko wybrane kolumny.
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader.GetName(i)}: {reader[i]} | ");
                }
                Console.WriteLine(); // Nowa linia po każdym wierszu
            }
            reader.Close();
        }

        public void InsertKura(Kura kura, int id_kurnik)
        {
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO kury(id_kurnik, plec, zniesione_jaja, waga) VALUES (@id_kurnik, @plec, @zniesione_jaja, @waga)";
                cmd.Parameters.AddWithValue("@id_kurnik", id_kurnik);
                cmd.Parameters.AddWithValue("@plec", kura.gender);
                cmd.Parameters.AddWithValue("@zniesione_jaja", kura.eggs_laid);
                cmd.Parameters.AddWithValue("@waga", kura.weight);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteKura(int id_kura)
        {
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM kury WHERE id_kury = @id_kura";
                cmd.Parameters.AddWithValue("@id_kura", id_kura);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertKurnik(kurnik kurnik)
        {
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO kurniki(lokalizacja, nazwa) VALUES (@lokalizacja, @nazwa)";
                cmd.Parameters.AddWithValue("@lokalizacja", kurnik.Localization);
                cmd.Parameters.AddWithValue("@nazwa", kurnik.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteKurnik(int id_kurnik)
        {
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM kurniki WHERE id_kurnik = @id_kurnik";
                cmd.Parameters.AddWithValue("@id_kurnik", id_kurnik);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
