using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories;

public class BookRepository: IBookRepository
{
    private AppDbContext _context;
    
    public BookRepository(AppDbContext context){
        _context = context;
    }

    public async Task<List<Book>> GetAll()
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .ToListAsync();
    }

    public async Task<Book> GetOne(int id)
    {
        return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> Add(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> Update(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }
    
    public async Task Delete(Book book)
    { 
        _context.Books.Remove(book);
       await _context.SaveChangesAsync();
    }

    public async Task<PaginatedList<Book>> GetAllSorted(int page, BookSortType sortType)
    {
        IQueryable<Book> books = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher);

        books = sortType switch
        {
            BookSortType.TitleDesc => books.OrderByDescending(b => b.Title),
            BookSortType.DateAsc => books.OrderBy(b => b.PublishedDate),
            BookSortType.DateDesc => books.OrderByDescending(b => b.PublishedDate),
            BookSortType.AuthorNameAsc => books.OrderBy(b => b.Author.FullName),
            BookSortType.AuthorNameDesc => books.OrderByDescending(b => b.Author.FullName),
            _ => books.OrderBy(b => b.Title)
        };

        int PageSize = 5;
        int pageIndex = page - 1;
        var count = await books.CountAsync();
        var items = await books.Skip(pageIndex * PageSize).Take(PageSize).ToListAsync();
        PaginatedList<Book> result = new PaginatedList<Book>(items, count, pageIndex, PageSize);
        return result;
    }

    public async Task<PaginatedList<Book>> GetAllFilteredAndSorted(int page, BookSearchQuery bookSearchQuery, BookSortType sortType)
    {
        IQueryable<Book> books = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher);

        books = FilterBooks(books, bookSearchQuery);
        
        books = sortType switch
        {
            BookSortType.TitleDesc => books.OrderByDescending(b => b.Title),
            BookSortType.DateAsc => books.OrderBy(b => b.PublishedDate),
            BookSortType.DateDesc => books.OrderByDescending(b => b.PublishedDate),
            BookSortType.AuthorNameAsc => books.OrderBy(b => b.Author.FullName),
            BookSortType.AuthorNameDesc => books.OrderByDescending(b => b.Author.FullName),
            _ => books.OrderBy(b => b.Title)
        };
        
        int PageSize = 5;
        int pageIndex = page - 1;
        var count = await books.CountAsync();
        var items = await books.Skip(pageIndex * PageSize).Take(PageSize).ToListAsync();
        PaginatedList<Book> result = new PaginatedList<Book>(items, count, pageIndex, PageSize);
        return result;
    }

    private static IQueryable<Book> FilterBooks(IQueryable<Book> books, BookSearchQuery bookSearchQuery)
    {
        if (!string.IsNullOrEmpty(bookSearchQuery.Title))
        {
            books = books.Where(b=>b.Title.ToLower().Contains(bookSearchQuery.Title.ToLower()));
        }
        
        if (bookSearchQuery.PublishedFrom != null)
        {
            books = books.Where(b => b.PublishedDate >= bookSearchQuery.PublishedFrom);
        }
        
        if (bookSearchQuery.PublishedTo != null)
        {
            books = books.Where(b => b.PublishedDate <= bookSearchQuery.PublishedTo);
        }
        
        if (bookSearchQuery.AuthorId != null)
        {
            books = books.Where(b => b.AuthorId == bookSearchQuery.AuthorId);
        }
        
        if (!string.IsNullOrEmpty(bookSearchQuery.Author))
        {
            books = books.Where(b=>b.Author.FullName.ToLower().Contains(bookSearchQuery.Author.ToLower()));
        }
        
        if (bookSearchQuery.AuthorBirthFrom != null)
        {
            books = books.Where(b => b.Author.DateOfBirth >= bookSearchQuery.AuthorBirthFrom);
        }
        
        if (bookSearchQuery.AuthorBirthTo != null)
        {
            books = books.Where(b => b.Author.DateOfBirth <= bookSearchQuery.AuthorBirthTo);
        }
        
        return books;
    }
}