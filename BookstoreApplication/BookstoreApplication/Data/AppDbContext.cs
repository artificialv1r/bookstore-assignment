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
        
        // ========== SEED PODACI ==========
        
        // 5 Autora
        modelBuilder.Entity<Author>().HasData(
            new Author 
            { 
                Id = 1, 
                FullName = "Ivo Andrić", 
                Biography = "Jugoslovenski pisac, dobitnik Nobelove nagrade za književnost 1961. godine. Najpoznatiji po romanima o Bosni.", 
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1892, 10, 9), DateTimeKind.Utc)
            },
            new Author 
            { 
                Id = 2, 
                FullName = "Meša Selimović", 
                Biography = "Bosanskohercegovački i jugoslovenski pisac. Najpoznatiji po romanu 'Derviš i smrt'.", 
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1910, 4, 26), DateTimeKind.Utc)
            },
            new Author 
            { 
                Id = 3, 
                FullName = "Dobrica Ćosić", 
                Biography = "Srpski romanopisac i političar, autor velikih romanesknih ciklusa o srpskoj istoriji.", 
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1921, 12, 29), DateTimeKind.Utc)
            },
            new Author 
            { 
                Id = 4, 
                FullName = "Milorad Pavić", 
                Biography = "Srpski pesnik, prozni pisac i književni istoričar. Najpoznatiji po romanu 'Hazarski rečnik'.", 
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1929, 10, 15), DateTimeKind.Utc)
            },
            new Author 
            { 
                Id = 5, 
                FullName = "Danilo Kiš", 
                Biography = "Jugoslovenski pisac jevrejskog porekla, jedan od najznačajnijih autora 20. veka.", 
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1935, 2, 22), DateTimeKind.Utc)
            }
        );

        // 3 Izdavača
        modelBuilder.Entity<Publisher>().HasData(
            new Publisher 
            { 
                Id = 1, 
                Name = "Prosveta", 
                Address = "Terazije 16, Beograd", 
                Website = "https://www.prosveta.rs" 
            },
            new Publisher 
            { 
                Id = 2, 
                Name = "Laguna", 
                Address = "Resavska 33, Beograd", 
                Website = "https://www.laguna.rs" 
            },
            new Publisher 
            { 
                Id = 3, 
                Name = "Vulkan izdavaštvo", 
                Address = "Makedonska 30, Beograd", 
                Website = "https://www.vulkanizdavastvo.rs" 
            }
        );

        // 12 Knjiga
        modelBuilder.Entity<Book>().HasData(
            new Book 
            { 
                Id = 1, 
                Title = "Na Drini ćuprija", 
                PageCount = 352, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1945, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00001-1", 
                AuthorId = 1, 
                PublisherId = 1 
            },
            new Book 
            { 
                Id = 2, 
                Title = "Travnička hronika", 
                PageCount = 288, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1942, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00002-8", 
                AuthorId = 1, 
                PublisherId = 1 
            },
            new Book 
            { 
                Id = 3, 
                Title = "Prokleta avlija", 
                PageCount = 176, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1954, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00003-5", 
                AuthorId = 1, 
                PublisherId = 2 
            },
            new Book 
            { 
                Id = 4, 
                Title = "Derviš i smrt", 
                PageCount = 368, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1966, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00004-2", 
                AuthorId = 2, 
                PublisherId = 1 
            },
            new Book 
            { 
                Id = 5, 
                Title = "Tvrđava", 
                PageCount = 312, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00005-9", 
                AuthorId = 2, 
                PublisherId = 2 
            },
            new Book 
            { 
                Id = 6, 
                Title = "Koreni", 
                PageCount = 624, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1954, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00006-6", 
                AuthorId = 3, 
                PublisherId = 1 
            },
            new Book 
            { 
                Id = 7, 
                Title = "Deobe", 
                PageCount = 544, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1961, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00007-3", 
                AuthorId = 3, 
                PublisherId = 3 
            },
            new Book 
            { 
                Id = 8, 
                Title = "Vreme smrti", 
                PageCount = 1200, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1972, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00008-0", 
                AuthorId = 3, 
                PublisherId = 1 
            },
            new Book 
            { 
                Id = 9, 
                Title = "Hazarski rečnik", 
                PageCount = 336, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1984, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00009-7", 
                AuthorId = 4, 
                PublisherId = 2 
            },
            new Book 
            { 
                Id = 10, 
                Title = "Predeo slikan čajem", 
                PageCount = 192, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1988, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00010-3", 
                AuthorId = 4, 
                PublisherId = 2 
            },
            new Book 
            { 
                Id = 11, 
                Title = "Bašta, pepeo", 
                PageCount = 264, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1965, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00011-0", 
                AuthorId = 5, 
                PublisherId = 3 
            },
            new Book 
            { 
                Id = 12, 
                Title = "Grobnica za Borisa Davidoviča", 
                PageCount = 144, 
                PublishedDate = DateTime.SpecifyKind(new DateTime(1976, 1, 1), DateTimeKind.Utc),
                ISBN = "978-86-01-00012-7", 
                AuthorId = 5, 
                PublisherId = 3 
            }
        );

        // 4 Nagrade
        modelBuilder.Entity<Award>().HasData(
            new Award 
            { 
                Id = 1, 
                Name = "Nobelova nagrada za književnost", 
                Description = "Najpoznatija književna nagrada na svetu, dodeljena za izuzetan doprinos svetskoj književnosti.", 
                YearOfFirstAssignment = 1901 
            },
            new Award 
            { 
                Id = 2, 
                Name = "NIN nagrada", 
                Description = "Prestižna srpska književna nagrada za najbolji roman godine, dodeljena od strane lista NIN.", 
                YearOfFirstAssignment = 1954 
            },
            new Award 
            { 
                Id = 3, 
                Name = "Andrićeva nagrada", 
                Description = "Srpska književna nagrada za najbolju pripovetku ili roman.", 
                YearOfFirstAssignment = 1981 
            },
            new Award 
            { 
                Id = 4, 
                Name = "Evropska književna nagrada", 
                Description = "Međunarodna književna nagrada za izuzetan doprinos evropskoj književnosti.", 
                YearOfFirstAssignment = 1985 
            }
        );

        // 15 podataka u tabeli poveznici (AuthorAwardBridge)
        modelBuilder.Entity<AuthorAward>().HasData(
            new AuthorAward { AuthorId = 1, AwardId = 1, YearAwarded = 1961 },
            new AuthorAward { AuthorId = 1, AwardId = 2, YearAwarded = 1954 },
            new AuthorAward { AuthorId = 1, AwardId = 3, YearAwarded = 1982 },
            
            new AuthorAward { AuthorId = 2, AwardId = 2, YearAwarded = 1966 },
            new AuthorAward { AuthorId = 2, AwardId = 3, YearAwarded = 1985 },
            new AuthorAward { AuthorId = 2, AwardId = 4, YearAwarded = 1990 },
            
            new AuthorAward { AuthorId = 3, AwardId = 2, YearAwarded = 1962 },
            new AuthorAward { AuthorId = 3, AwardId = 3, YearAwarded = 1981 },
            new AuthorAward { AuthorId = 3, AwardId = 4, YearAwarded = 1988 },
            
            new AuthorAward { AuthorId = 4, AwardId = 2, YearAwarded = 1985 },
            new AuthorAward { AuthorId = 4, AwardId = 3, YearAwarded = 1990 },
            new AuthorAward { AuthorId = 4, AwardId = 4, YearAwarded = 1991 },
            
            new AuthorAward { AuthorId = 5, AwardId = 2, YearAwarded = 1973 },
            new AuthorAward { AuthorId = 5, AwardId = 3, YearAwarded = 1987 },
            new AuthorAward { AuthorId = 5, AwardId = 4, YearAwarded = 1989 }
        );
    }
}