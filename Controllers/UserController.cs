using ExpensesPlanner.Shared.Models;
using ExpensesPlannerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ExpensesPlannerAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _users;

        public UserController(MongoDbService mongoDbService)
        {
            _users = mongoDbService.Database.GetCollection<User>("Users");
        }

        // GET: api/User
        [HttpGet]
        public async Task<ICollection<User>> Get()
        {
            // Retrieve all users from the Users collection in MongoDB with no filter
            return await _users.Find(FilterDefinition<User>.Empty).ToListAsync();
        }

        // GET api/User/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
