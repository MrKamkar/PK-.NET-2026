using NUnit.Framework;
using LibraryManagementSimulation;

namespace LibraryManagementTest;

[TestFixture]
public class LibraryTests
{
    private Library _library;

    [SetUp]
    public void Setup()
    {
        _library = new Library();
    }

    [Test]
    public void BorrowBook_AvailableBook_ReturnsTrueAndSetsAvailabilityToFalse()
    {
        // Przygotowanie
        var book = new Book("Test Book", "Author", "1");
        _library.AddBook(book);

        // Działanie
        _library.BorrowBook(1, "Test Reader");

        // Sprawdzenie
        Assert.That(book.IsAvailable, Is.False);
    }

    [Test]
    public void BorrowBook_AlreadyBorrowed_ThrowsBookAlreadyBorrowedException()
    {
        // Przygotowanie
        var book = new Book("Test Book", "Author", "2");
        _library.AddBook(book);
        _library.BorrowBook(2, "Test Reader"); // pierwsze wypożyczenie

        // Działanie i Sprawdzenie
        Assert.Throws<BookAlreadyBorrowedException>(() =>
        {
            _library.BorrowBook(2, "Test Reader 2");
        });
    }

    [Test]
    public void ReturnBook_BorrowedBook_ReturnsTrueAndSetsAvailabilityToTrue()
    {
        // Przygotowanie
        var book = new Book("Test Book", "Author", "3");
        _library.AddBook(book);
        _library.BorrowBook(3, "Test Reader");

        // Działanie
        _library.ReturnBook(3);

        // Sprawdzenie
        Assert.That(book.IsAvailable, Is.True);
    }

    [Test]
    public void ReturnBook_AvailableBook_ThrowsBookNotBorrowedException()
    {
        // Przygotowanie
        var book = new Book("Test Book", "Author", "4");
        _library.AddBook(book);

        // Działanie i Sprawdzenie
        Assert.Throws<BookNotBorrowedException>(() =>
        {
            _library.ReturnBook(4);
        });
    }
    
    [Test]
    public void BorrowBook_NonExistentBook_ThrowsBookNotFoundException()
    {
        // Działanie i Sprawdzenie
        Assert.Throws<BookNotFoundException>(() =>
        {
            _library.BorrowBook(999, "Test Reader");
        });
    }
}
