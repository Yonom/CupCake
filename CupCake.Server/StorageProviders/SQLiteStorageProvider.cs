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
            try
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
            catch (SQLiteException ex)
            {
                throw new StorageException(ex.Message, ex);
            }
        }

        public string Get(string id, string key)
        {
            try
            {
                using (var conn = new SQLiteConnection(this._connectionString))
                {
                    const string query = "SELECT value FROM cupcake WHERE id = @id AND key = @key";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@key", key);

                        conn.Open();
                        return (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new StorageException(ex.Message, ex);
            }
        }

        public void CreateTable()
        {
            try
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
            catch (SQLiteException ex)
            {
                throw new StorageException(ex.Message, ex);
            }
        }
    }
}