using System.Reflection;
using DbUp;
using MySql.Data.MySqlClient;

namespace ByCodersChallenge.Cqrs.Dapper.Migrations
{
    public static class Migrations
    {
        public static void CreateDataBase(string server, string databaseName)
        {
            var connection = new MySqlConnection("Server=" + server + ";Uid=root; Pwd=123456;");

            connection.Open();
            using (var cmd = new MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = string.Format("CREATE DATABASE IF NOT EXISTS {0} DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci", databaseName);
                cmd.ExecuteNonQuery();
            }
        }
        public static void Migrate(
            string databaseServerName,
            string databaseName,
            string databaseUser,
            string databasePassword)
        {
            var connectionString =
                "Server=" + databaseServerName +
                ";Database=" + databaseName +
                ";Uid=" + databaseUser +
                ";Pwd=" + databasePassword + ";";

            var upgrader =
                DeployChanges.To
                             .MySqlDatabase(connectionString)
                             .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                             .LogToConsole()
                             .Build();

            upgrader.PerformUpgrade();
        }
    }
}
