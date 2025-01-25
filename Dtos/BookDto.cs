namespace App_web_book.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Author { get; set; } = null!;
    }

    public class CreateBookDto
    {
        public string Name { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int NumberOfPages { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}