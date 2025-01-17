namespace App_web_book.library;

public class Book
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int NumberOfPages { get; set; }
    public required string Author { get; set; }
    public DateTime PublicationDate { get; set; } // Changed to DateTime
    public required string Genre { get; set; }
}