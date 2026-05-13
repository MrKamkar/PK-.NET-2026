using Microsoft.EntityFrameworkCore;
using BookApi.Models;
using BookApi.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=books.db";

// Rejestracja EF Core
builder.Services.AddDbContext<BooksDbContext>(options =>
    options.UseSqlite(connectionString));

// Rejestracja ADO.NET repozytorium jako singleton
builder.Services.AddSingleton(new BooksAdoRepository(connectionString));

var app = builder.Build();

// Inicjalizacja bazy danych – EF Core
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BooksDbContext>();
    db.Database.EnsureCreated();
}

// Inicjalizacja bazy danych – ADO.NET (tworzy tabelę jeśli nie istnieje)
var adoRepo = app.Services.GetRequiredService<BooksAdoRepository>();
adoRepo.EnsureCreated();

// =============================================
//  ENDPOINTY EF CORE – /api/books
// =============================================

// GET /api/books – Zwraca wszystkie książki z bazy (EF Core)
app.MapGet("/api/books", async (BooksDbContext db) =>
    Results.Ok(await db.Books.ToListAsync()));

// GET /api/books/{id} – Zwraca szczegóły książki o podanym ID (EF Core)
app.MapGet("/api/books/{id}", async (int id, BooksDbContext db) =>
{
    var book = await db.Books.FindAsync(id);
    return book is not null ? Results.Ok(book) : Results.NotFound();
});

// POST /api/books – Dodaje książkę na podstawie danych z żądania (EF Core)
app.MapPost("/api/books", async (Book book, BooksDbContext db) =>
{
    db.Books.Add(book);
    await db.SaveChangesAsync();
    return Results.Created($"/api/books/{book.Id}", book);
});

// PUT /api/books/{id} – Edytuje książkę na podstawie danych z żądania (EF Core)
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

// DELETE /api/books/{id} – Usuwa książkę o podanym ID z bazy (EF Core)
app.MapDelete("/api/books/{id}", async (int id, BooksDbContext db) =>
{
    var book = await db.Books.FindAsync(id);
    if (book is null) return Results.NotFound();

    db.Books.Remove(book);
    await db.SaveChangesAsync();
    return Results.Ok(book);
});

// =============================================
//  ENDPOINTY ADO.NET – /api/ado/books
// =============================================

// GET /api/ado/books – Zwraca wszystkie książki z bazy (ADO.NET)
app.MapGet("/api/ado/books", async (BooksAdoRepository repo) =>
    Results.Ok(await repo.GetAllAsync()));

// GET /api/ado/books/{id} – Zwraca szczegóły książki o podanym ID (ADO.NET)
app.MapGet("/api/ado/books/{id}", async (int id, BooksAdoRepository repo) =>
{
    var book = await repo.GetByIdAsync(id);
    return book is not null ? Results.Ok(book) : Results.NotFound();
});

// POST /api/ado/books – Dodaje książkę na podstawie danych z żądania (ADO.NET)
app.MapPost("/api/ado/books", async (Book book, BooksAdoRepository repo) =>
{
    var created = await repo.AddAsync(book);
    return Results.Created($"/api/ado/books/{created.Id}", created);
});

// PUT /api/ado/books/{id} – Edytuje książkę na podstawie danych z żądania (ADO.NET)
app.MapPut("/api/ado/books/{id}", async (int id, Book input, BooksAdoRepository repo) =>
{
    var updated = await repo.UpdateAsync(id, input);
    return updated is not null ? Results.Ok(updated) : Results.NotFound();
});

// DELETE /api/ado/books/{id} – Usuwa książkę o podanym ID z bazy (ADO.NET)
app.MapDelete("/api/ado/books/{id}", async (int id, BooksAdoRepository repo) =>
{
    var deleted = await repo.DeleteAsync(id);
    return deleted is not null ? Results.Ok(deleted) : Results.NotFound();
});

app.Run();
