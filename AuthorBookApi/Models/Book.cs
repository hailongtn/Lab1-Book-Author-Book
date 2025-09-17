namespace AuthorBookApi.Models;


public class Book
{
public int BookId { get; set; }
public string Title { get; set; } = null!;


// Many-to-Many
public ICollection<Author> Authors { get; set; } = new List<Author>();
}