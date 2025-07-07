using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ExpensesPlannerAPI.Models
{
    public class Expense
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required decimal Amount { get; set; }
        public string? Description { get; set; }
        public string ListExpensesId { get; set; } = string.Empty;
    }
}
