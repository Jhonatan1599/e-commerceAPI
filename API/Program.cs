using API.Extentions;
using API.Middleware;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Exception Handling Middleware
app.UseMiddleware<ExceptionMiddleware>();
// Redirects to "erros/code"  when the endpoint was not found
app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
   app.UseSwaggerDocumentation();
}
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Create the database and run the migration at runtime
/*This code allows to get access to a Scope service inside our program class where we do not have the ability to inject a servce*/
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

// Get services related to identity
var identityContext = services.GetRequiredService<AppIdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();


try
{   
    //Seeding data to store
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);

    //Seeding data to identity
    await identityContext.Database.MigrateAsync();
    await AppIdentityDbContextSeed.SeedUserAsync(userManager);

}
catch(Exception ex)
{
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
