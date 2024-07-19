using System.Globalization;
using Drinking_Logger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace Drinking_Logger.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkModel Drink { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet(int id)
        {
            Drink = GetById(id);
            return Page();
        }

        private DrinkModel GetById(int id)
        {
            var drinkRecord = new DrinkModel();

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM drinks WHERE Id = {id}";

                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    drinkRecord.Id = reader.GetInt32(0);
                    drinkRecord.Name = reader.GetString(1);
                    drinkRecord.Date = DateTime.Parse(reader.GetString(2),new CultureInfo("en-US"));
                    drinkRecord.Quantity = reader.GetDouble(3);
                }
                return drinkRecord;
            }
        }

        public IActionResult OnPost(int id)
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"DELETE from drinks WHERE Id = {id}";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        } 
    }
}
