using ExpensesPlanner.Shared.Models;

namespace ExpensesPlanner.Shared.Services.Interfaces
{
    public interface IExpensesRepository
    {
        Task<IEnumerable<Expense>> GetAllExpenses();
        Task<Expense> GetByIdAsync(string id);
        Task<List<Expense>> GetByNameAsync(string description);
        decimal GetTotalAmount();
        Task CreateAsync(Expense expense);
        Task UpdateAsync(string id, Expense expense);
        Task DeleteAsync(string id);
    }
}
