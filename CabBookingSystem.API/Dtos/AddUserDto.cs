namespace CabBookingSystem.API.Dtos
{
    public class AddUserDto
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }
    }
}
