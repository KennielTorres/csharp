
using Microsoft.Data.Sqlite;

namespace HabitLogger{

    public class Program
    {   
        public static void Main(string[] args){
            string connectionString = @"Data Source=habit-logger.db";

            using (var connection = new SqliteConnection(connectionString)){
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS habits (
                                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            date TEXT, 
                                            habit TEXT, 
                                            quantity INTEGER)";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}

