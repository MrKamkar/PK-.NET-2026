using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSimulation;

public class Library : IBookOperations
{
    private List<Book> _books;
    private List<Reader> _readers;

    public Library()
    {
        _books = new List<Book>();
        _readers = new List<Reader>();
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public void RegisterReader(Reader reader)
    {
        _readers.Add(reader);
    }

    public void ListAvailableBooks()
    {
        foreach (var book in _books.Where(b => b.IsAvailable))
        {
            book.DisplayInfo();
        }
    }

    public void BorrowBook(int bookId, string borrowerName)
    {
        var book = _books.FirstOrDefault(b => b.Id == bookId.ToString());
        if (book == null)
            throw new BookNotFoundException($"Book with ID {bookId} not found.");

        if (!book.IsAvailable)
            throw new BookAlreadyBorrowedException($"Book with ID {bookId} is already borrowed.");

        book.IsAvailable = false;
    }

    public void ReturnBook(int bookId)
    {
        var book = _books.FirstOrDefault(b => b.Id == bookId.ToString());
        if (book == null)
            throw new BookNotFoundException($"Book with ID {bookId} not found.");

        if (book.IsAvailable)
            throw new BookNotBorrowedException($"Book with ID {bookId} was not borrowed.");

        book.IsAvailable = true;
    }

    public bool BorrowBook(string bookId, Reader reader)
    {
        try
        {
            BorrowBook(int.Parse(bookId), reader.Name);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool ReturnBook(string bookId, Reader reader)
    {
        try
        {
            ReturnBook(int.Parse(bookId));
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
