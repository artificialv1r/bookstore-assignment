using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories;

public class BookRepository
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
}