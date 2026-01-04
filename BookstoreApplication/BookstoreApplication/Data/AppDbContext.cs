using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Data;

public class AppDbContext:DbContext
{
    public  AppDbContext(DbContextOptions options):base(options)
    {}
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Award> Awards { get; set; }
    public DbSet<AuthorAward> AuthorAwards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorAward>(entity =>
        {
            entity.ToTable("AuthorAwardBridge");
            
            entity.HasKey(authorAward => new { authorAward.AuthorId, authorAward.AwardId });
            
            entity.HasOne(authorAward => authorAward.Author)
                .WithMany(author => author.AuthorAwards)
                .HasForeignKey(authorAward => authorAward.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        
            entity.HasOne(authorAward => authorAward.Award)
                .WithMany(author => author.AuthorAwards)
                .HasForeignKey(authorAward => authorAward.AwardId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Author>()
            .Property(author => author.DateOfBirth)
            .HasColumnName("Birthday");
        

        modelBuilder.Entity<Book>()
            .HasOne(book => book.Publisher)
            .WithMany(publisher => publisher.Books)
            .HasForeignKey(book => book.PublisherId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}