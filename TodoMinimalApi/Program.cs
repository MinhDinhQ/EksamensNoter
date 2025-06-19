using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;


// In-memory liste til at gemme Todo-items
var todos = new List<TodoItem>
{
    new TodoItem(1, "Lær minimal API", false),
    new TodoItem(2, "Skriv noter", false)
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// GET /todos - Hent alle todo-items
app.MapGet("/todos", () => todos);

// GET /todos/{id} - Hent et specifikt todo-item
app.MapGet("/todos/{id:int}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});

// POST /todos - Tilføj et nyt todo-item
app.MapPost("/todos", (TodoItem newTodo) =>
{
    todos.Add(newTodo);
    return Results.Created($"/todos/{newTodo.Id}", newTodo);
});

// PUT /todos/{id} - Opdater et todo-item
app.MapPut("/todos/{id:int}", (int id, TodoItem updatedTodo) =>
{
    var index = todos.FindIndex(t => t.Id == id);
    if (index == -1) return Results.NotFound();

    todos[index] = updatedTodo;
    return Results.Ok(updatedTodo);
});

// DELETE /todos/{id} - Slet et todo-item
app.MapDelete("/todos/{id:int}", (int id) =>
{
    var index = todos.FindIndex(t => t.Id == id);
    if (index == -1) return Results.NotFound();

    todos.RemoveAt(index);
    return Results.NoContent();
});

app.Run();