using Microsoft.EntityFrameworkCore;
using AuthorBookApi.Models;


namespace AuthorBookApi.Data;


public class AppDbContext : DbContext
{
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


public DbSet<Author> Authors => Set<Author>();
public DbSet<Book> Books => Set<Book>();


protected override void OnModelCreating(ModelBuilder modelBuilder)
{
base.OnModelCreating(modelBuilder);


// (Tùy chọn) cấu hình tên bảng join, key composite... nếu muốn
modelBuilder.Entity<Author>()
.ToTable("Authors");
modelBuilder.Entity<Book>()
.ToTable("Books");


// *Nếu* bạn muốn đặt tên bảng trung gian cụ thể và khóa composite:
modelBuilder.Entity<Author>()
.HasMany(a => a.Books)
.WithMany(b => b.Authors)
.UsingEntity<Dictionary<string, object>>(
"AuthorBooks",
j => j.HasOne<Book>().WithMany().HasForeignKey("BookId").HasConstraintName("FK_AuthorBooks_Books_BookId"),
j => j.HasOne<Author>().WithMany().HasForeignKey("AuthorId").HasConstraintName("FK_AuthorBooks_Authors_AuthorId"),
j => { j.HasKey("AuthorId", "BookId"); j.ToTable("AuthorBooks"); }
);
}
}