using System.Globalization;
using Drinking_Logger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace Drinking_Logger.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkModel Drink { get; set; }

        public UpdateModel(IConfiguration configuration)
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
                    drinkRecord.Date = DateTime.Parse(reader.GetString(2), new CultureInfo("en-US"));
                    drinkRecord.Quantity = reader.GetDouble(3);
                }

                return drinkRecord;
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                   @$"UPDATE drinks SET name = '{Drink.Name}', 
                                                date ='{Drink.Date}', 
                                                quantity = {Drink.Quantity} 
                   WHERE Id = {Drink.Id}";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
