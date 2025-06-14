using ExpensesPlannerAPI.Extensions;
using ExpensesPlannerAPI.Data;
using ExpensesPlanner.Shared.Services.Interfaces;
using ExpensesPlanner.Services.Repository;

namespace ExpensesPlannerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSingleton<MongoDbService>();
            builder.Services.AddScoped<IExpensesRepository, ExpensesRepository>();


            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseOpenApi();

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
