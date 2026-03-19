using System;

namespace LibraryManagementSimulation;

public class Book
{
    private string _title;
    private string _author;
    private string _id;
    private bool _isAvailable;

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public string Author
    {
        get { return _author; }
        set { _author = value; }
    }

    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public bool IsAvailable
    {
        get { return _isAvailable; }
        set { _isAvailable = value; }
    }

    public Book(string title, string author, string id)
    {
        _title = title;
        _author = author;
        _id = id;
        _isAvailable = true;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Book: {Title} by {Author} (ID: {Id}) - Available: {IsAvailable}");
    }
}
