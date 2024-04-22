using System.ComponentModel.DataAnnotations;

namespace HOMEWORK_3.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }
}
