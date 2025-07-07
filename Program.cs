using ExpensesPlanner.Services.Repository;
using ExpensesPlannerAPI.Data;
using ExpensesPlannerAPI.Extensions;
using ExpensesPlannerAPI.Models;
using ExpensesPlannerAPI.Services.Interfaces;
using ExpensesPlannerAPI.Services.Repository;
using Microsoft.Extensions.FileProviders;

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
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<ListExpensesRepository>();

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("https://localhost:7065")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                                  });
            });

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
                    builder.Configuration.GetConnectionString("DbConnection"),
                    "ExpensesPlanner");

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            builder.Services.AddOpenApi();

            var app = builder.Build();


            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsPath),
                RequestPath = "/uploads"
            });

            // Configure the HTTP request pipeline.
            app.UseOpenApi();
            app.UseCors(MyAllowSpecificOrigins);
            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
