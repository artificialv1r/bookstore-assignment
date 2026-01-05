using BookstoreApplication.Data;
using BookstoreApplication.Models;

namespace BookstoreApplication.Repositories;

public class PublisherRepository
{
    private AppDbContext _context;
    
    public PublisherRepository(AppDbContext context){
        _context = context;
    }

    public List<Publisher> GetAll()
    {
        return _context.Publishers.ToList();
    }

    public Publisher GetOne(int id)
    {
        return _context.Publishers.Find(id);
    }

    public Publisher Add(Publisher publisher)
    {
        _context.Publishers.Add(publisher);
        _context.SaveChanges();
        return publisher;
    }

    public Publisher Update(Publisher publisher)
    {
        _context.Publishers.Update(publisher);
        _context.SaveChanges();
        return publisher;
    }
    
    public bool Delete(int id)
    {
        Publisher publisher = _context.Publishers.Find(id);

        if (publisher == null)
        {
            return false;
        }
        
        _context.Publishers.Remove(publisher);
        _context.SaveChanges();
        return true;
    }
}