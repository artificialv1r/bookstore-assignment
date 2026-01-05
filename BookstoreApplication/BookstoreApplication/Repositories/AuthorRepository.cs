using BookstoreApplication.Data;
using BookstoreApplication.Models;

namespace BookstoreApplication.Repositories;

public class AuthorRepository
{
    private AppDbContext _context;
    
    public AuthorRepository(AppDbContext context){
        _context = context;
    }

    public List<Author> GetAll()
    {
        return _context.Authors.ToList();
    }

    public Author GetOne(int id)
    {
        return _context.Authors.Find(id);
    }

    public Author Add(Author author)
    {
        _context.Authors.Add(author);
        _context.SaveChanges();
        return author;
    }

    public Author Update(Author author)
    {
        _context.Authors.Update(author);
        _context.SaveChanges();
        return author;
    }
    
    public bool Delete(int id)
    {
        Author author = _context.Authors.Find(id);

        if (author == null)
        {
            return false;
        }
        
        _context.Authors.Remove(author);
        _context.SaveChanges();
        return true;
    }
}