using System;

namespace LibraryManagementSimulation;

public class BookAlreadyBorrowedException : Exception
{
    public BookAlreadyBorrowedException(string message) : base(message) { }
}

public class BookNotBorrowedException : Exception
{
    public BookNotBorrowedException(string message) : base(message) { }
}

public class BookNotFoundException : Exception
{
    public BookNotFoundException(string message) : base(message) { }
}
