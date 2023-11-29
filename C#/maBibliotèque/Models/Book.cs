namespace Library.Models;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
    public int Year { get; set; }

    public Book(string title, string author, string category, int year)
    {
        Title = title;
        Author = author;
        Category = category;
        Year = year;
    }
}