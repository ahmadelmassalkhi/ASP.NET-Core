using System.ComponentModel.DataAnnotations;

namespace homework2.Models
{
    public class Product
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
     
        [Required]
        public decimal Price { get; set; }

        public static bool TryParse(string? s, out Product result)
        {
            decimal price = 0; // keep 0 if s==null
            if (s != null && !decimal.TryParse(s, out price))
            {
                result = new Product();
                return false;
            }

            result = new Product
            {
                Title = "",
                Description = "",
                Price = price
            };
            return true;
        }
    }
}