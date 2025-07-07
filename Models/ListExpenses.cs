using ExpensesPlannerAPI.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpensesPlanner.Client.Models
{
    public class ListExpenses
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public List<Expense> Expenses { get; set; } = new List<Expense>();

        [BsonRequired]
        public string UserId { get; set; } = string.Empty;
    }
}
