namespace bilog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; } = "User";
        public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}