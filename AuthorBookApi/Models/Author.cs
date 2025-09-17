namespace AuthorBookApi.Models;


public class Author
{
public int AuthorId { get; set; }
public string Name { get; set; } = null!;


// Many-to-Many
public ICollection<Book> Books { get; set; } = new List<Book>();
}