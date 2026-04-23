using Microsoft.EntityFrameworkCore;
using BookApi.Models;
using BookApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BooksDbContext>(options =>
    options.UseSqlite("Data Source=books.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BooksDbContext>();
    db.Database.EnsureCreated();
}

// GET /api/books – Zwraca wszystkie książki z bazy
app.MapGet("/api/books", async (BooksDbContext db) =>
    Results.Ok(await db.Books.ToListAsync()));

// GET /api/books/{id} – Zwraca szczegóły książki o podanym ID
app.MapGet("/api/books/{id}", async (int id, BooksDbContext db) =>
{
    var book = await db.Books.FindAsync(id);
    return book is not null ? Results.Ok(book) : Results.NotFound();
});

// POST /api/books – Dodaje książkę na podstawie danych z żądania
app.MapPost("/api/books", async (Book book, BooksDbContext db) =>
{
    db.Books.Add(book);
    await db.SaveChangesAsync();
    return Results.Created($"/api/books/{book.Id}", book);
});

// PUT /api/books/{id} – Edytuje książkę na podstawie danych z żądania
app.MapPut("/api/books/{id}", async (int id, Book input, BooksDbContext db) =>
{
    var book = await db.Books.FindAsync(id);
    if (book is null) return Results.NotFound();

    book.Title = input.Title;
    book.Author = input.Author;
    book.PublishedYear = input.PublishedYear;
    book.IsRead = input.IsRead;

    await db.SaveChangesAsync();
    return Results.Ok(book);
});

// DELETE /api/books/{id} – Usuwa książkę o podanym ID z bazy
app.MapDelete("/api/books/{id}", async (int id, BooksDbContext db) =>
{
    var book = await db.Books.FindAsync(id);
    if (book is null) return Results.NotFound();

    db.Books.Remove(book);
    await db.SaveChangesAsync();
    return Results.Ok(book);
});

app.Run();
