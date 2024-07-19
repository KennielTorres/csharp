using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Drinking_Logger.Models;
using Microsoft.Data.Sqlite;

namespace Drinking_Logger.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public DrinkModel Drink { get; set; }

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
                tableCmd.CommandText = @$"INSERT INTO drinks(name, date, quantity) 
                                          VALUES(
                                                 '{Drink.Name}',
                                                 '{Drink.Date}',
                                                  {Drink.Quantity}
                                           )";

                tableCmd.ExecuteNonQuery();
            }
            
            return RedirectToPage("./Index");
        }
    }
}
