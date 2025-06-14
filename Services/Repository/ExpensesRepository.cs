using ExpensesPlanner.Shared.Models;
using ExpensesPlanner.Shared.Services.Interfaces;
using ExpensesPlannerAPI.Data;
using MongoDB.Driver;

namespace ExpensesPlanner.Services.Repository
{
    public class ExpensesRepository : IExpensesRepository
    {
        private readonly IMongoCollection<Expense> _expenses;

        public ExpensesRepository(MongoDbService mongoDbService)
        {
            _expenses = mongoDbService.Database.GetCollection<Expense>("Expenses");
        }

        public async Task CreateAsync(Expense expense)
        {
            await _expenses.InsertOneAsync(expense);
        }

        public async Task DeleteAsync(string id)
        {
            await _expenses.DeleteOneAsync(expense => expense.Id.ToString() == id);
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await _expenses.Find(FilterDefinition<Expense>.Empty).ToListAsync();
        }

        public async Task<Expense> GetByIdAsync(string id)
        {
            return await _expenses.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Expense>> GetByNameAsync(string description)
        {
            return await _expenses.Find(
                expense => expense.Description.ToLower().Contains(description.ToLower())).ToListAsync();
        }

        public decimal GetTotalAmount()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(string id, Expense expense)
        {
             await _expenses.ReplaceOneAsync(expense => expense.Id.ToString() == id, expense);
        }
    }
}
