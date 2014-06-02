using System;
using System.Data.SQLite;
using CupCake.Core.Storage;

namespace CupCake.Server.StorageProviders
{
    public class SQLiteStorageProvider : IStorageProvider
    {
        private readonly string _connectionString;

        public SQLiteStorageProvider(string connectionString)
        {
            this._connectionString = connectionString;

            this.CreateTable();
        }

        public void Set(string id, string key, string value)
        {
            using (var conn = new SQLiteConnection(this._connectionString))
            {
                const string query = "INSERT OR REPLACE INTO cupcake VALUES (@id, @key, @value)";
                using (var cmd = new SQLiteCommand(query, conn))
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
            using (var conn = new SQLiteConnection(this._connectionString))
            {
                const string query = "SELECT value FROM cupcake WHERE id = @id AND key = @key";
                using (var cmd = new SQLiteCommand(query, conn))
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
            using (var conn = new SQLiteConnection(this._connectionString))
            {
                const string query =
                    "CREATE TABLE IF NOT EXISTS cupcake (id VARCHAR(45) NOT NULL,key VARCHAR(45) NOT NULL,value TEXT NULL,PRIMARY KEY (id, key));";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}