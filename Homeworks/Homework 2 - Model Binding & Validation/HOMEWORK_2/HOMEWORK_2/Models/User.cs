using System.ComponentModel.DataAnnotations;

namespace homework2.Models
{
    public class User : IValidatableObject
    {
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(1, 100)]
        public int Age { get; set; }


        // runs after all the DataAnnotations requirements are successfully met
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Age % 2 != 0)
            {
                yield return new ValidationResult("Age must be divisible by 2, game is game!", [nameof(Age)]);
            }
            if (!Email.EndsWith("@gmail.com") && !Email.EndsWith("@ul.edu.lb"))
            {
                yield return new ValidationResult("Email must be one of the following domains: `ul.edu.lb` or `gmail.com` !", [nameof(Email)]);
            }
        }
    }
}
