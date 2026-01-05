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

    public List<Book> GetAll()
    {
        return _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .ToList();
    }

    public Book GetOne(int id)
    {
        return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefault(b => b.Id == id);
    }

    public Book Add(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
        return book;
    }

    public Book Update(Book book)
    {
        _context.Books.Update(book);
        _context.SaveChanges();
        return book;
    }
    
    public bool Delete(int id)
    {
        Book book = _context.Books.Find(id);

        if (book == null)
        {
            return false;
        }
        
        _context.Books.Remove(book);
        _context.SaveChanges();
        return true;
    }
}