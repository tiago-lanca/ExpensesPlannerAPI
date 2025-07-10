using ExpensesPlannerAPI.Models;
using ExpensesPlannerAPI.Services.Interfaces;
using ExpensesPlannerAPI.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesRepository _expenses;
        private readonly ListExpensesRepository _listExpenses;

        public ExpensesController(IExpensesRepository expenses, ListExpensesRepository listExpenses)
        {
            _expenses = expenses;
            _listExpenses = listExpenses;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetAllExpenses()
        {
            return Ok(await _expenses.GetAllExpenses());
        }

        // GET api/Expenses/5
        [HttpGet("id/{id}")]
        public async Task<Expense> GetById(string id)
        {
            var expense = await _expenses.GetByIdAsync(id);
            return expense == null ? null : expense;
        }

        [HttpGet("description/{description}")]
        public async Task<List<Expense>> GetByName(string description)
        {
            var expense = await _expenses.GetByNameAsync(description);
            return expense == null ? null : expense;
        }

        // POST api/Expenses
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Expense expense)
        {
            await _expenses.CreateAsync(expense);
            return CreatedAtAction(nameof(GetById), new { id = expense.Id }, expense);
        }

        // PUT api/Expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Expense expense)
        {
            Expense existingExpense = await _expenses.GetByIdAsync(id);
            if(existingExpense is null) return NotFound($"Expense with ID {id} not found.");

            expense.Id = id;
            await _expenses.UpdateAsync(id.ToString(), expense);
            return NoContent();
        }

        // DELETE api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Expense existingExpense = await _expenses.GetByIdAsync(id);
            if(existingExpense is null) return NotFound($"Expense with ID {id} not found.");
            await _expenses.DeleteAsync(id.ToString());

            await _listExpenses.DeleteExpense(id, existingExpense.ListExpensesId);

            return NoContent();
        }
    }
}
