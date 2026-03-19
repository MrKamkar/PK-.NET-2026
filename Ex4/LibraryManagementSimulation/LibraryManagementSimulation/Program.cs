using System;

namespace LibraryManagementSimulation;

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        // Dodanie książek do biblioteki
        Book book1 = new Book("C# Programming", "John Doe", "12345");
        Book book2 = new Book("Design Patterns", "Gamma et al.", "67890");
        library.AddBook(book1);
        library.AddBook(book2);

        // Rejestracja czytelnika
        Reader reader = new Reader(1, "Alice", "alice@example.com");
        library.RegisterReader(reader);

        // Wypożyczenie książki
        if (library.BorrowBook("12345", reader))
        {
            Console.WriteLine("Book borrowed successfully.");
        }
        else
        {
            Console.WriteLine("Book is not available.");
        }

        // Zwrot książki
        if (library.ReturnBook("12345", reader))
        {
            Console.WriteLine("Book returned successfully.");
        }
        else
        {
            Console.WriteLine("Failed to return book.");
        }
    }
}
