using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories;

public class PublisherRepository
{
    private AppDbContext _context;
    
    public PublisherRepository(AppDbContext context){
        _context = context;
    }

    public async Task<List<Publisher>> GetAll()
    {
        return await _context.Publishers.ToListAsync();
    }

    public async Task<Publisher?> GetOne(int id)
    {
        return await _context.Publishers.FirstOrDefaultAsync(p =>p.Id==id);
    }

    public async Task<Publisher> Add(Publisher publisher)
    {
        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();
        return publisher;
    }

    public async Task<Publisher> Update(Publisher publisher)
    {
        _context.Publishers.Update(publisher);
        await _context.SaveChangesAsync();
        return publisher;
    }
    
    public async Task Delete(int id)
    {
        Publisher publisher = await _context.Publishers.FindAsync(id);
        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync();
    }
}