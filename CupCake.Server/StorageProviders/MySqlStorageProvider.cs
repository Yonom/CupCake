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
            try
            {
                using (var conn = new MySqlConnection(this._connectionString))
                {
                    const string query =
                        "INSERT INTO `cupcake` VALUES (@id, @key, @value) ON DUPLICATE KEY UPDATE `id` = @id";
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
            catch (MySqlException ex)
            {
                throw new StorageException(ex.Message, ex);
            }
        }

        public string Get(string id, string key)
        {
            try
            {
                using (var conn = new MySqlConnection(this._connectionString))
                {
                    const string query = "SELECT `value`FROM `cupcake` WHERE `id` = @id AND `key` = @key";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@key", key);

                        conn.Open();
                        return (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new StorageException(ex.Message, ex);
            }
        }

        public void CreateTable()
        {
            try
            {
                using (var conn = new MySqlConnection(this._connectionString))
                {
                    const string query =
                        "CREATE TABLE IF NOT EXISTS `cupcake` (`id` varchar(45) NOT NULL,`key` varchar(45) NOT NULL,`value` text,PRIMARY KEY (`id`,`key`));";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new StorageException(ex.Message, ex);
            }
        }
    }
}