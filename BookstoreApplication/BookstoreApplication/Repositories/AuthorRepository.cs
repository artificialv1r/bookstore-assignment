using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories;

public class AuthorRepository
{
    private AppDbContext _context;
    
    public AuthorRepository(AppDbContext context){
        _context = context;
    }

    public async Task <List<Author>> GetAll()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author?> GetOne(int id)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Author> Add(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task <Author> Update(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
        return author;
    }
    
    public async Task Delete(int id)
    {
        Author author = await _context.Authors.FindAsync(id);
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }
}