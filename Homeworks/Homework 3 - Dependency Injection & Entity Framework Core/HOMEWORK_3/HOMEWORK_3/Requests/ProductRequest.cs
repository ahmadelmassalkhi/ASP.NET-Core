using System.ComponentModel.DataAnnotations;

namespace HOMEWORK_3.Requests
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; } // optional

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
