using ExpensesPlannerAPI.Models;

namespace ExpensesPlannerAPI.Services.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<List<ApplicationUser>> GetByNameAsync(string description);
        Task CreateAsync(ApplicationUser user);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(string id);
    }
}
