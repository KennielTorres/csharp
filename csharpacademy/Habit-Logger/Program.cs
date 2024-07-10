
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;

namespace HabitLogger{

    public class Program
    {   
        static string connectionString = @"Data Source=habit-logger.db";
        public static void Main(string[] args){

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
            GetUserInput();
        }

        static void GetUserInput(){
            bool closeApp = false;
            while(!closeApp){
                Console.WriteLine("\n\n>>MAIN MENU<<");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to View All Records.");
                Console.WriteLine("Type 2 to Insert Record");
                Console.WriteLine("Type 3 to Delete Record");
                Console.WriteLine("Type 4 to Update Record");
                Console.WriteLine("-----------------------------------------\n");

                string? commandInput = Console.ReadLine();

                switch(commandInput){
                    case "0":
                        closeApp = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    case "1":
                        ViewRecords();
                        break;
                    case "2":
                        InsertRecord();
                        break;
                    case "3":
                        DeleteRecord();
                        break;
                    case "4":
                        UpdateRecord();
                        break;
                    default:
                        Console.WriteLine("Invalid Input. Please type a number from 0 to 4.");
                        break;
                }
            }
        }

        private static void InsertRecord(){
            string? date = GetDateInput();
            string? habit = GetHabitInput();
            int quantity = GetNumberInput("\n\nPlease insert a measure of your habit. (No decimal allowed.)\n");
            using (var connection = new SqliteConnection(connectionString)){
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"INSERT INTO habits(date, habit, quantity) VALUES('{date}', '{habit}', {quantity})";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        internal static string? GetDateInput(){
            string? dateInput = null;
            bool isValid = false;
            while (!isValid){
                Console.WriteLine("\n\nPlease insert the date: (Format: mm-dd-yyyy).");
                dateInput = Console.ReadLine();

                if (DateTime.TryParseExact(dateInput, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid date format. Please try again.");
                }
            }
            return dateInput;
        }

        internal static string? GetHabitInput(){
            Console.WriteLine("\n\nPlease insert the name of your habit.");
            string? habitInput = Console.ReadLine();
            return habitInput;
        }

        internal static int GetNumberInput(string message){
            Console.WriteLine(message);
            int parsedValue;
            string? userInput;

            while (true){
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out parsedValue))
                {
                    return parsedValue;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please insert an integer.\n");
                }
            }
        }

        internal static void ViewRecords(){
            using (var connection = new SqliteConnection(connectionString)){
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "SELECT * FROM habits";
                List<Habits> habitsTableData = new List<Habits>();    
                SqliteDataReader reader = tableCmd.ExecuteReader();

                if(reader.HasRows){
                    while (reader.Read()){
                        habitsTableData.Add(
                            new Habits{
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), "MM-dd-yyyy", new CultureInfo("en-US")),
                                Habit = reader.GetString(2),
                                Quantity = reader.GetInt32(3)
                            }
                        );
                    }
                }
                else{
                    Console.WriteLine("No Records Found.");
                }
                connection.Close();

                Console.WriteLine("\n\n----------Habits Table----------\n");
                foreach (var habit in habitsTableData) {
                    Console.WriteLine($"{habit.Id} - {habit.Date.ToString("MM-dd-yyyy")} - Habit: {habit.Habit} - Quantity: {habit.Quantity}");
                }
                Console.WriteLine("--------------------------------");
            }
        }

        internal static void DeleteRecord(){
            ViewRecords();
            int id = GetNumberInput("Please insert the ID of the habit you would like to update or type 0 to return to main menu.");
            if (id == 0){
                return;
            }
            using (var connection = new SqliteConnection(connectionString)){
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE FROM habits WHERE id = {id}";
                int rowCount = tableCmd.ExecuteNonQuery();
                if (rowCount == 0){
                    Console.WriteLine($"\nRecord with ID {id} doesn't exist.\n");
                    DeleteRecord(); 
                }
                connection.Close();
            }
            Console.WriteLine($"Record with id {id} has been deleted.\n");
        }

        internal static void UpdateRecord(){
            ViewRecords();
            int id = GetNumberInput("Please insert the ID of the habit you would like to update or type 0 to return to main menu.");
            if (id == 0){
                return;
            }
            using (var connection = new SqliteConnection(connectionString)){
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM habits WHERE id = {id})";
                int checkQuery = Convert.ToInt32(tableCmd.ExecuteScalar());

                if (checkQuery == 0){
                    Console.WriteLine($"\nRecord with ID {id} doesn't exist.\n");
                    connection.Close();
                    UpdateRecord();
                }
                string? date = GetDateInput();
                string? habit = GetHabitInput();
                int quantity = GetNumberInput("\n\nPlease insert a measure of your habit. (No decimal allowed.)\n\n");

                tableCmd.CommandText = $"UPDATE habits SET date = '{date}', habit = '{habit}', quantity = {quantity} WHERE id = {id}";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }


    }
}

public class Habits(){
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Habit { get; set; }
    public int Quantity { get; set; }
}