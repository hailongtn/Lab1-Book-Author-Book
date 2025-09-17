using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthorBookApi.Data;
using AuthorBookApi.Models;


[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
private readonly AppDbContext _db;
public AuthorsController(AppDbContext db) => _db = db;


[HttpPost]
public async Task<IActionResult> Create(Author author)
{
_db.Authors.Add(author);
await _db.SaveChangesAsync();
return CreatedAtAction(nameof(GetById), new { id = author.AuthorId }, author);
}


[HttpGet]
public async Task<IActionResult> GetAll()
{
var authors = await _db.Authors.Include(a => a.Books).ToListAsync();
return Ok(authors);
}


[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
var author = await _db.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.AuthorId == id);
if (author == null) return NotFound();
return Ok(author);
}


// Endpoint gắn 1 book vào author
[HttpPost("{authorId}/books/{bookId}")]
public async Task<IActionResult> AddBookToAuthor(int authorId, int bookId)
{
var author = await _db.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.AuthorId == authorId);
if (author == null) return NotFound($"Author {authorId} not found");


var book = await _db.Books.FindAsync(bookId);
if (book == null) return NotFound($"Book {bookId} not found");


if (!author.Books.Any(b => b.BookId == bookId))
{
author.Books.Add(book);
await _db.SaveChangesAsync();
}


return NoContent();
}
}