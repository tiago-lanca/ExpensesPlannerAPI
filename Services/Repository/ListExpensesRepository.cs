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

        public async Task UpdateAsync(ListExpenses listExpenses)
        {
            if(listExpenses == null)
                throw new ArgumentNullException(nameof(listExpenses), "List cannot be null");

            await _listExpenses.ReplaceOneAsync(x => x.Id == listExpenses.Id, listExpenses);
        }

    }
}
