using ExpensesPlanner.Client.Models;
using ExpensesPlannerAPI.Models;
using ExpensesPlannerAPI.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpensesPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListExpensesController : ControllerBase
    {
        private readonly ListExpensesRepository _listExpensesRepository;

        public ListExpensesController(ListExpensesRepository listExpenses)
        {
            _listExpensesRepository = listExpenses;
        }


        // GET: api/<ListExpenses>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetAllLists()
        {
            return Ok(await _listExpensesRepository.GetAllListsAsync());
        }

        // GET api/<ListExpenses>/5
        [HttpGet("{id}")]
        public async Task<ListExpenses> GetListById(string id)
        {
            return await _listExpensesRepository.GetListByIdAsync(id);
        }

        // GET api/<ListExpenses>/user/dfwer23421
        [HttpGet("user/{id}")]
        public async Task<ListExpenses> GetListByUserId(string id)
        {
            var list = await _listExpensesRepository.GetListByUserIdAsync(id);

            if (list is null)
                return new ListExpenses();

            return list;
        }

        // POST api/<ListExpenses>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ListExpenses listExpenses)
        {
            await _listExpensesRepository.CreateListAsync(listExpenses);
            return CreatedAtAction(nameof(GetListById), new { id = listExpenses.Id }, listExpenses);
        }

        // PUT api/<ListExpenses>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(ListExpenses newList)
        {

            newList.Expenses.Last().ListExpensesId = newList.Id; // Update the last expense's ListExpensesId to match the list's ID

            await _listExpensesRepository.UpdateListAsync(newList);

            return Ok(newList);
        }

        // DELETE api/<ListExpenses>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
