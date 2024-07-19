using System.ComponentModel.DataAnnotations;
namespace Drinking_Logger.Models
{
    public class DrinkModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public double Quantity { get; set; }
    }
}