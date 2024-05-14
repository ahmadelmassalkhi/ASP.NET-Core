namespace CVInfoApp.Models
{
    public class CV
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDay { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public List<string> Skills { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoURL { get; set; }
    }
}
