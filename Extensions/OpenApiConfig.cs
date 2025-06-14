using Scalar.AspNetCore;

namespace ExpensesPlannerAPI.Extensions;

public static class OpenApiConfig
{
    public static void UseOpenApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.Title = "Expenses Planner API";
                options.Layout = ScalarLayout.Modern;
                //options.Theme = ScalarTheme.Saturn;
            });
        }
    }
}
