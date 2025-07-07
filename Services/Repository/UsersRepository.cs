using ExpensesPlannerAPI.Data;
using ExpensesPlannerAPI.Models;
using ExpensesPlannerAPI.Services.Interfaces;
using MongoDB.Driver;

namespace ExpensesPlannerAPI.Services.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMongoCollection<ApplicationUser> _users;
        public UsersRepository(MongoDbService mongoDbService)
        {
            _users = mongoDbService.Database.GetCollection<ApplicationUser>("Users");
        }
        public async Task CreateAsync(ApplicationUser user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var existingUser = await _users.Find(u => u.Email == user.Email).FirstOrDefaultAsync();

            if(existingUser is null) { 
                await _users.InsertOneAsync(user);
            }
            else { 
                throw new InvalidOperationException($"User with email {user.Email} already exists.");
            }
        }

        public async Task DeleteAsync(string id)
        {
            await _users.DeleteOneAsync(user => user.Id.ToString() == id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _users.Find(FilterDefinition<ApplicationUser>.Empty).ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _users.Find(user => id == user.Id.ToString()).FirstOrDefaultAsync();
        }

        public async Task<List<ApplicationUser>> GetByNameAsync(string description)
        {
            return await _users.Find(user => user.FirstName.ToLower().Contains(description.ToLower()) ||
                                              user.LastName.ToLower().Contains(description.ToLower())).ToListAsync();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }
    }
}
