var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

app.MapGet("/items/{id}", (int id) => {
    var item = items.FirstOrDefault(i => i.Id == id);
    return item is not null ? Results.Ok(item) : Results.NotFound();
});

app.MapGet("/items", () => {
    return Results.Ok(items);
});

app.MapPost("/items", (Item newItem) => {
    return $"Item created: {newItem.Name}";
});

app.MapPut("/items/{id}", (int id, Item updatedItem) => {
    return $"Item {id} updated to {updatedItem.Name}";
});

app.MapDelete("/items/{id}", (int id) => {
    return $"Item {id} deleted";
});


app.Run();

