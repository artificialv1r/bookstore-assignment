using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services;

public class AuthorService
{
    private readonly AuthorRepository _authorRepository;

    public AuthorService(AppDbContext context)
    {
        _authorRepository = new AuthorRepository(context);
    }

    public async Task<List<Author>> GetAll()
    {
        return await  _authorRepository.GetAll();
    }

    public async Task<Author> GetOne(int id)
    {
        var author = await  _authorRepository.GetOne(id);
        if (author == null)
        {
            throw new KeyNotFoundException($"Author with id {id} not found");
        }
        return author;
    }

    public async Task<Author> Create(Author author)
    {
        if (author == null)
        {
            throw new ArgumentNullException(nameof(author));
        }
        
        await  _authorRepository.Add(author);
        return author;
    }

    public async Task<Author> Update(Author author)
    {
        if (author == null)
        {
            throw new ArgumentNullException(nameof(author));
        }

        var existingAuthor = await _authorRepository.GetOne(author.Id);
        if (existingAuthor == null)
        {
            throw new KeyNotFoundException("Author not found");
        }
        
        await  _authorRepository.Update(author);
        return author;
    }

    public async Task<bool> Delete(int id)
    {
        var author = await _authorRepository.GetOne(id);
        if (author == null)
        {
            return false;
        }
        await  _authorRepository.Delete(id);
        return true;
    }
}