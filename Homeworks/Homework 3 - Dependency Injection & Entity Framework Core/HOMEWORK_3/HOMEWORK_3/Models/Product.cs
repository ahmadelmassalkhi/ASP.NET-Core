using System.ComponentModel.DataAnnotations;

namespace HOMEWORK_3.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        // navigation property
        public Category Category { get; set; }

        public void Update(
            string Name,
            string? Description,
            decimal Price,
            int CategoryId)
        {
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.CategoryId = CategoryId;
        }
    }
}
