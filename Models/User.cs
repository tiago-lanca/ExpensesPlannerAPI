using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpensesPlanner.Shared.Models
{
    public class User
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public required string Name { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }

        [BsonElement("phoneNumber"), BsonRepresentation(BsonType.String)]
        public string? PhoneNumber { get; set; }

        [BsonElement("address"), BsonRepresentation(BsonType.String)]
        public string? Address { get; set; }

        [BsonElement("dateBirth"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? DateOfBirth { get; set; }
        
        [BsonElement("profilePictureUrl"), BsonRepresentation(BsonType.String)]
        public string? ProfilePictureUrl { get; set; }

        public List<string>? ListExpensesId { get; set; } = new List<string>();
    }
}
