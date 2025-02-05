//using TodoApi;
//using Pomelo.EntityFrameworkCore.MySql;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//var app = builder.Build();

//builder.Services.AddDbContext<ToDoDbContext>(options =>
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
//);

//app.MapGet("/items/{id}", (int id) => {
//    var item = items.FirstOrDefault(i => i.Id == id);
//    return item is not null ? Results.Ok(item) : Results.NotFound();
//});

//app.MapGet("/items", () => {
//    return Results.Ok(items);
//});

//app.MapPost("/items", (Item newItem) => {
//    return $"Item created: {newItem.Name}";
//});

//app.MapPut("/items/{id}", (int id, Item updatedItem) => {
//    return $"Item {id} updated to {updatedItem.Name}";
//});

//app.MapDelete("/items/{id}", (int id) => {
//    return $"Item {id} deleted";
//});


//app.Run();


using TodoApi;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var connectionString = builder.Configuration.GetConnectionString("ToDoDB");
// Console.WriteLine();
System.Console.WriteLine($"Connection String: {connectionString}");
builder.Services.AddDbContext<ToDoDbContext>(options =>

    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI
(c =>
{

    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1"); ;
    c.RoutePrefix = string.Empty;
});

app.MapGet("/items", async (ToDoDbContext db) => { return await db.Items.ToListAsync(); });
app.MapPost("items/", (Item item, ToDoDbContext db) =>
{
    db.Items.Add(item);
    db.SaveChanges();
    return Results.Created($"/{item.Id}", item);
});
app.MapPut("/items/{id}", (int id, ToDoDbContext db, Item newItem) =>
{
    var it = db.Items.Find(id);
    if (it == null) return Results.NotFound();
    else
    {
        it.Name = newItem.Name;
        it.IsComplete = newItem.IsComplete;
        db.SaveChanges();
        return Results.Ok(it);
    }
});

app.MapDelete("/items/{id}", (int id, ToDoDbContext db) =>
{
    var item = db.Items.Find(id);
    if (item == null) return Results.NotFound();
    db.Items.Remove(item);
    db.SaveChanges();
    return Results.NoContent();
});

app.MapGet("/", () => "ToDoApi server is runing");

private static string connectionString = "\"ToDoDB\": \"server=localhost;user=root;password=215251844;database=tododb\"";

app.Run();
