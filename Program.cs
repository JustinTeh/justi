using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Npgsql;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registers UserService for DI for /GetUsers endpoint
builder.Services.AddScoped<UserService>();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("FIWAdb")));

var app = builder.Build();


// UserService is injected here.
app.MapPost("/PostUser", async (User user, UserService userService) => {
    await userService.PostUserAsync(user);
    return Results.CreatedAtRoute("/PostUser");
});

app.MapGet("/GetUsers", async (UserService userService) => {
    return Results.Ok(await userService.GetUsersAsync());
});

app.MapPut("UpdateUser", async (User user, UserService userService) => {
    return await userService.UpdateUserProfileAsync(user);
});

app.MapDelete("/DeleteUser", async (int userID, UserService userService) => {
    return await userService.DeleteUserAsync(userID);
    
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

