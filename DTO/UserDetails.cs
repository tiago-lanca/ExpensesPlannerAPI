using ExpensesPlannerAPI.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ExpensesPlannerAPI.DTO
{
    public class UserDetails
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public byte[]? ProfilePictureUrl { get; set; }
        public RoleType Role { get; set; } = RoleType.User; // Default role is User

        public string? ListExpensesId { get; set; } = string.Empty;
    }
}
