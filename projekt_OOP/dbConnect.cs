using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using projekt_OOP.Interfaces;

namespace projekt_OOP
{
    public class dbConnect
    {
        private string ConnectionString = "Server=localhost;Database=kurnik_test2;User ID=root;Password=''";
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
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd, nie udało się rozłączyć z bazą danych!");
            }
           
        }

        public void Select(int? id_kurnik = null)
        {
            try
            {

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT 
                    kurniki.id_kurnik, 
                    kurniki.nazwa, 
                    kurniki.lokalizacja,
                    kury.id_kury, 
                    kury.plec, 
                    kury.zniesione_jaja, 
                    kury.waga
                FROM 
                    kurniki
                LEFT JOIN 
                    kury 
                ON 
                    kurniki.id_kurnik = kury.id_kurnik";

                    if (id_kurnik.HasValue)
                    {
                        cmd.CommandText += " WHERE kurniki.id_kurnik = @id_kurnik";
                        cmd.Parameters.AddWithValue("@id_kurnik", id_kurnik);
                    }

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int? currentKurnikId = null;
                        bool hasRows = false;

                        while (reader.Read())
                        {
                            hasRows = true;

                            int kurnikId = reader.GetInt32(reader.GetOrdinal("id_kurnik"));
                            if (currentKurnikId != kurnikId)
                            {
                                if (currentKurnikId != null)
                                {
                                    Console.WriteLine("-------------------------------------------------");
                                }

                                currentKurnikId = kurnikId;

                                Console.WriteLine($"ID Kurnika: {reader["id_kurnik"]}");
                                Console.WriteLine($"Nazwa: {reader["nazwa"]}");
                                Console.WriteLine($"Lokalizacja: {reader["lokalizacja"]}");
                                Console.WriteLine("-------------------------------------------------");
                                Console.WriteLine("Rekordy przypisane do kurnika:");
                                Console.WriteLine("ID Kury | Płeć    | Zniesione jaja | Waga");
                                Console.WriteLine("-------------------------------------------------");
                            }

                            if (reader["id_kury"] != DBNull.Value)
                            {
                                Console.WriteLine(
                                    $"{reader["id_kury"],7} | " +
                                    $"{reader["plec"],-6} | " +
                                    $"{reader["zniesione_jaja"],14} | " +
                                    $"{reader["waga"],4}");
                            }
                        }

                        if (!hasRows)
                        {
                            Console.WriteLine("Brak danych w bazie.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }


        public void SelectByID(int id)
        {
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM kury WHERE kury.id_kury LIKE {id};";
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd przy dodawaniu kury");
            }
        }

        public void DeleteKura(int id_kura)
        {
            try
            {
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM kury WHERE id_kury = @id_kura";
                    cmd.Parameters.AddWithValue("@id_kura", id_kura);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd przy usuwaniu kury");
            }
                
        }

        public void InsertKurnik(Kurnik kurnik)
        {
            try
            { 
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO kurniki(lokalizacja, nazwa) VALUES (@lokalizacja, @nazwa)";
                    cmd.Parameters.AddWithValue("@lokalizacja", kurnik.Localization);
                    cmd.Parameters.AddWithValue("@nazwa", kurnik.Name);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas dodawania kurnika");
            }
        }

        public void DeleteKurnik(int id_kurnik)
        {
            try
            {
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM kurniki WHERE id_kurnik = @id_kurnik";
                    cmd.Parameters.AddWithValue("@id_kurnik", id_kurnik);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas usuwania kurnika");
            }
        }

        public void UpdateKura(int id_kura, int columnId, object value)
        {
            try
            {
                string columnName = columnId switch
                {
                    1 => "id_kurnik",
                    2 => "plec",
                    3 => "zniesione_jaja",
                    4 => "waga",
                };

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE kury SET {columnName} = @value WHERE id_kury = @id_kury";
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("@id_kury", id_kura);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("wystąpił błąd podczas aktualizacji");
            }
            
        }





    }
}
