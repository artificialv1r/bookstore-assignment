using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories;

public class AwardRepository  : IAwardRepository
{
    private AppDbContext _context;
    
    public AwardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Award>> GetAll()
    {
        return await _context.Awards.ToListAsync();
    }

    public async Task<Award> GetOne(int id)
    {
        return await _context.Awards.FindAsync(id);
    }

    public async Task<Award> Add(Award award)
    {
        _context.Awards.Add(award);
        await _context.SaveChangesAsync();
        return award;
    }

    public async Task<Award> Update(Award award)
    {
        var existingAward = await _context.Awards.FirstOrDefaultAsync(a => a.Id == award.Id);
        if (existingAward == null)
        {
            return null;
        }
        
        existingAward.Name = award.Name;
        existingAward.Description = award.Description;
        existingAward.YearOfFirstAssignment = award.YearOfFirstAssignment;
        
        _context.SaveChangesAsync();
        return existingAward;
    }

    public async Task <bool> Delete(int id)
    {
        Award award = await _context.Awards.FindAsync(id);

        if (award == null)
        {
            return false;
        }
        
        _context.Awards.Remove(award);
        await _context.SaveChangesAsync();
        return true;
    }
}