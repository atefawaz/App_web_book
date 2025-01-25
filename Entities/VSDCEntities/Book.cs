using System;
using System.Collections.Generic;

namespace App_web_book.Entities.VSDCEntities;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int NumberOfPages { get; set; }

    public string Author { get; set; } = null!;

    public DateTime PublicationDate { get; set; }

    public string Genre { get; set; } = null!;
}
