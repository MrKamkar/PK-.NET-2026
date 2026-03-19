using System;

namespace LibraryManagementSimulation;

public class EBook : Book
{
    private string _fileFormat;

    public string FileFormat
    {
        get { return _fileFormat; }
        set { _fileFormat = value; }
    }

    public EBook(string title, string author, string id, string fileFormat) : base(title, author, id)
    {
        _fileFormat = fileFormat;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Format: {FileFormat}");
    }
}
