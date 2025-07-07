

using ExpensesPlannerAPI.Enums;

namespace ExpensesPlannerAPI.DTO
{
    public class RegisterUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public byte[]? ProfilePictureUrl { get; set; }

        public RoleType Role { get; set; } = RoleType.User;
    }
}
