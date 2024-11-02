using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Npgsql;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options =>
{
   options.SuppressAsyncSuffixInActionNames = false;
});
// Registers UserService for DI for /GetUsers endpoint
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<FridgeItemService>();
builder.Services.AddCors();


builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("FIWAdb")));

var app = builder.Build();


// UserService is injected here.
app.MapPost("/PostUser", async (User user, UserService userService) => {
    await userService.PostUserAsync(user);
    return Results.Ok("/PostUser");
});

app.MapGet("/GetUsers", async (UserService userService) => {
    return Results.Ok(await userService.GetUsersAsync());
});

app.MapPut("/UpdateUser", async (User user, UserService userService) => {
    return await userService.UpdateUserProfileAsync(user);
});

app.MapDelete("/DeleteUser", async (int userID, UserService userService) => {
    return await userService.DeleteUserAsync(userID);
    
});


app.MapPost("/PostFridgeItem", async (FridgeItem fridgeItem, FridgeItemService fridgeItemService) => {
    await fridgeItemService.PostFridgeItemAsync(fridgeItem);
    return Results.Ok("/PostFridgeItem");
});

app.MapGet("/GetAllFridgeItems", (FridgeItemService fridgeItemService) =>
{
    fridgeItemService.GetAllFridgeItems();
    return Results.Ok();
});

app.MapGet("/GetAllFridgeItemsByUserID/{userID}", (HttpRequest request, FridgeItemService fridgeItemService, int userID) => {
    fridgeItemService.GetAllFridgeItems(userID);
    return Results.Ok();
});

app.MapPut("/UpdateFridgeItem", async (FridgeItem fridgeItem, FridgeItemService fridgeItemService) => {
    return await fridgeItemService.UpdateFridgeItemAsync(fridgeItem);
});

app.MapDelete("/TossOutFridgeItem{itemID}", async (int itemID, FridgeItemService fridgeItemService) => {
    return await fridgeItemService.TossOutFridgeItemAsync(itemID);
    
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

