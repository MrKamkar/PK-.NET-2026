using Microsoft.Data.Sqlite;
using BookApi.Models;

namespace BookApi.Data;

public class BooksAdoRepository
{
    private readonly string _connectionString;

    public BooksAdoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Tworzy tabelę Books jeśli nie istnieje
    public void EnsureCreated()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Books (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL DEFAULT '',
                Author TEXT NOT NULL DEFAULT '',
                PublishedYear INTEGER NOT NULL DEFAULT 0,
                IsRead INTEGER NOT NULL DEFAULT 0
            )";
        cmd.ExecuteNonQuery();
    }

    // Pobiera wszystkie książki z bazy
    public async Task<List<Book>> GetAllAsync()
    {
        var books = new List<Book>();

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT Id, Title, Author, PublishedYear, IsRead FROM Books";

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            books.Add(MapBook(reader));
        }

        return books;
    }

    // Pobiera książkę o podanym ID
    public async Task<Book?> GetByIdAsync(int id)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT Id, Title, Author, PublishedYear, IsRead FROM Books WHERE Id = @Id";
        cmd.Parameters.AddWithValue("@Id", id);

        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapBook(reader) : null;
    }

    // Dodaje nową książkę do bazy
    public async Task<Book> AddAsync(Book book)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO Books (Title, Author, PublishedYear, IsRead)
            VALUES (@Title, @Author, @PublishedYear, @IsRead);
            SELECT last_insert_rowid();";
        cmd.Parameters.AddWithValue("@Title", book.Title);
        cmd.Parameters.AddWithValue("@Author", book.Author);
        cmd.Parameters.AddWithValue("@PublishedYear", book.PublishedYear);
        cmd.Parameters.AddWithValue("@IsRead", book.IsRead ? 1 : 0);

        var newId = (long)(await cmd.ExecuteScalarAsync())!;
        book.Id = (int)newId;

        return book;
    }

    // Aktualizuje istniejącą książkę
    public async Task<Book?> UpdateAsync(int id, Book input)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            UPDATE Books
            SET Title = @Title, Author = @Author, PublishedYear = @PublishedYear, IsRead = @IsRead
            WHERE Id = @Id";
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@Title", input.Title);
        cmd.Parameters.AddWithValue("@Author", input.Author);
        cmd.Parameters.AddWithValue("@PublishedYear", input.PublishedYear);
        cmd.Parameters.AddWithValue("@IsRead", input.IsRead ? 1 : 0);

        var rows = await cmd.ExecuteNonQueryAsync();
        if (rows == 0) return null;

        input.Id = id;
        return input;
    }

    // Usuwa książkę o podanym ID z bazy
    public async Task<Book?> DeleteAsync(int id)
    {
        var book = await GetByIdAsync(id);
        if (book is null) return null;

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = "DELETE FROM Books WHERE Id = @Id";
        cmd.Parameters.AddWithValue("@Id", id);

        await cmd.ExecuteNonQueryAsync();
        return book;
    }

    // Mapuje wiersz z readera na obiekt Book
    private static Book MapBook(SqliteDataReader reader)
    {
        return new Book
        {
            Id = reader.GetInt32(0),
            Title = reader.GetString(1),
            Author = reader.GetString(2),
            PublishedYear = reader.GetInt32(3),
            IsRead = reader.GetInt32(4) != 0
        };
    }
}
