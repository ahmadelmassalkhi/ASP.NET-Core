using System.ComponentModel.DataAnnotations;

namespace CVInfoApp.Models
{
    public class CVBindModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateOnly BirthDay { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string Gender { get; set; }

        public List<string> Skills { get; set; } = [];

        [Required]
        public string Email { get; set; }

        [Required]
        [Compare("Email", ErrorMessage = "Email and Email Confirmation should be equal")]
        public string EmailConfirmation { get; set; }

        [Required]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$", ErrorMessage = "Password must have digit, letter, and symbol as well as be atleast of 8 characters")]
        public string Password { get; set; }

        [Required]
        public int Verify1 { get; set; }

        [Required]
        public int Verify2 { get; set; }

        [Required]
        public int VerifyTotal { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
