using BookstoreApplication.Data;
using BookstoreApplication.Models;

namespace BookstoreApplication.Repositories;

public class AwardRepository
{
    private AppDbContext _context;
    
    public AwardRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Award> GetAll()
    {
        return _context.Awards.ToList();
    }

    public Award GetOne(int id)
    {
        return _context.Awards.Find(id);
    }

    public Award Add(Award award)
    {
        _context.Awards.Add(award);
        _context.SaveChanges();
        return award;
    }

    public Award Update(Award award)
    {
        _context.Awards.Update(award);
        _context.SaveChanges();
        return award;
    }

    public bool Delete(int id)
    {
        Award award = _context.Awards.Find(id);

        if (award == null)
        {
            return false;
        }
        
        _context.Awards.Remove(award);
        _context.SaveChanges();
        return true;
    }
}