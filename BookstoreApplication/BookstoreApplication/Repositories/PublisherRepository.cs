using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories;

public class PublisherRepository : IPublisherRepository
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

    public async Task<PaginatedList<Publisher>> GetAllSorted(int page, PublisherSortType sortType)
    {
        IQueryable<Publisher> publishers = _context.Publishers;

        publishers = sortType switch
        {
            PublisherSortType.NameDesc => publishers.OrderByDescending(p => p.Name),
            PublisherSortType.AddressAsc => publishers.OrderBy(p => p.Address),
            PublisherSortType.AddressDesc => publishers.OrderByDescending(p => p.Address),
            _ => publishers.OrderBy(p => p.Name)
        };

        int PageSize = 5;
        int pageIndex = page - 1;
        var count = await publishers.CountAsync();
        var items = await publishers.Skip(pageIndex * PageSize).Take(PageSize).ToListAsync();
        PaginatedList<Publisher> result = new PaginatedList<Publisher>(items, count, pageIndex, PageSize);
        return result;

    }
}