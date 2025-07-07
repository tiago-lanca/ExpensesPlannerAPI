using ExpensesPlannerAPI.Models;

namespace ExpensesPlannerAPI.Services.Interfaces
{
    public interface IExpensesRepository
    {
        Task<IEnumerable<Expense>> GetAllExpenses();
        Task<Expense> GetByIdAsync(string id);
        Task<List<Expense>> GetByNameAsync(string description);
        decimal GetTotalAmount();
        Task<Expense> CreateAsync(Expense expense);
        Task UpdateAsync(string id, Expense expense);
        Task DeleteAsync(string id);
    }
}
