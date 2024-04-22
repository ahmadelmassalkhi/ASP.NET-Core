using System.ComponentModel.DataAnnotations;

namespace HOMEWORK_3.Requests
{
    public class CategoryRequest
    {

        [Required]
        public string Name { get; set; }
    }
}
