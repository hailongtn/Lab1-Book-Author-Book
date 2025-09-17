using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthorBookApi.Data;
using AuthorBookApi.Models;


[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
private readonly AppDbContext _db;
public BooksController(AppDbContext db) => _db = db;


[HttpPost]
public async Task<IActionResult> Create(Book book)
{
_db.Books.Add(book);
await _db.SaveChangesAsync();
return CreatedAtAction(nameof(GetById), new { id = book.BookId }, book);
}


[HttpGet]
public async Task<IActionResult> GetAll() => Ok(await _db.Books.Include(b => b.Authors).ToListAsync());


[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
var book = await _db.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.BookId == id);
if (book == null) return NotFound();
return Ok(book);
}
}