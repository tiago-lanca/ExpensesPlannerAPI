using ExpensesPlanner.Client.Models;
using ExpensesPlannerAPI.Data;
using ExpensesPlannerAPI.Models;
using MongoDB.Driver;

namespace ExpensesPlannerAPI.Services.Repository
{
    public class ListExpensesRepository
    {
        private readonly IMongoCollection<ListExpenses> _listExpenses;

        public ListExpensesRepository(MongoDbService mongoDbService)
        {
            _listExpenses = mongoDbService.Database.GetCollection<ListExpenses>("ListExpenses");
        }

        public async Task<List<ListExpenses>> GetAllListsAsync()
        {
            return await _listExpenses.Find(FilterDefinition<ListExpenses>.Empty).ToListAsync();
        }

        public async Task CreateListAsync(ListExpenses listExpenses)
        {
            await _listExpenses.InsertOneAsync(listExpenses);
        }

        public async Task<ListExpenses> GetListByIdAsync(string id)
        {
            return await _listExpenses.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ListExpenses> GetListByUserIdAsync(string id)
        {
            return await _listExpenses.Find(x => x.UserId == id).FirstOrDefaultAsync();
        }

        public async Task UpdateListAsync(ListExpenses listExpenses)
        {
            if(listExpenses == null)
                throw new ArgumentNullException(nameof(listExpenses), "List cannot be null");

            await _listExpenses.ReplaceOneAsync(x => x.Id == listExpenses.Id, listExpenses);
        }

        public async Task UpdateExpenseAsync(Expense newExpense)
        {
            var listExpenses = await GetListByIdAsync(newExpense.ListExpensesId);
            if (listExpenses == null)
                throw new ArgumentNullException(nameof(listExpenses), "List cannot be null");

            var expense = listExpenses.Expenses.Find(exp => exp.Id == newExpense.Id);
            expense.Description = newExpense.Description;
            expense.Amount = newExpense.Amount;
            expense.CreationDate = newExpense.CreationDate;
            expense.Category = newExpense.Category;

            await UpdateListAsync(listExpenses);
        }

        public async Task DeleteExpense(string id, string listId)
        {
            var listExpenses = await GetListByIdAsync(listId);

            // Search for a list which id mataches with listExpenses.Id
            var filter = Builders<ListExpenses>.Filter.Eq(list => list.Id, listExpenses.Id);

            // Create an update operation to remove the expense with the specified [string id] PullFilter removes the tem from the array
            var update = Builders<ListExpenses>.Update.PullFilter(list => list.Expenses, exp => exp.Id == id);

            await _listExpenses.UpdateOneAsync(filter, update);
        }

    }
}
