using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Storage;
using MySql.Data.MySqlClient;

namespace CupCake.Server.StorageProviders
{
    public class MySqlStorageProvider : IStorageProvider
    {
        private readonly string _connectionString;

        public MySqlStorageProvider(string connectionString)
        {
            this._connectionString = connectionString;

            this.CreateTable();
        }

        public void Set(string id, string key, string value)
        {
            using (var conn = new MySqlConnection(this._connectionString))
            {
                const string query = "INSERT INTO cupcake VALUES (@id, @key, @value) ON DUPLICATE KEY UPDATE id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Parameters.AddWithValue("@value", value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string Get(string id, string key)
        {
            using (var conn = new MySqlConnection(this._connectionString))
            {
                const string query = "SELECT value FROM cupcake WHERE id = @id AND key = @key";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@key", key);

                    try
                    {
                        conn.Open();
                        return (string)cmd.ExecuteScalar();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to connect to MySQL");
                        return null;
                    }
                }
            }
        }

        public void CreateTable()
        {
            using (var conn = new MySqlConnection(this._connectionString))
            {
                const string query = "CREATE TABLE IF NOT EXISTS cupcake (id VARCHAR(45) NOT NULL,key VARCHAR(45) NOT NULL,value TEXT NULL,PRIMARY KEY (id, key));";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
