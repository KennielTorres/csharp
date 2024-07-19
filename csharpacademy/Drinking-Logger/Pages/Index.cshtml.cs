using System.Globalization;
using Drinking_Logger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace Drinking_Logger.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;


    public List<DrinkModel> Records { get; set; }

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<DrinkModel> GetAllRecords()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString"))){
            connection.Open();

            var tableCmd = connection.CreateCommand();  
            tableCmd.CommandText = "SELECT * FROM drinks";
            var reader = tableCmd.ExecuteReader();

            var tableData = new List<DrinkModel>();
            while (reader.Read())
            {
                tableData.Add(
                    new DrinkModel
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Date = DateTime.Parse(reader.GetString(2), new CultureInfo("en-US")),
                        Quantity = reader.GetDouble(3)
                    }
                );
            }   
            return tableData;
        }
    }
}
