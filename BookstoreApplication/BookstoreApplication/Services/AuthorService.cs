
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using BookstoreApplication.Services.Interfaces;

namespace BookstoreApplication.Services;

public class AuthorService: IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Author>> GetAll()
    {
        return await  _repository.GetAll();
    }

    public Task<Author> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Author> GetOne(int id)
    {
        var author = await  _repository.GetOne(id);
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
        
        await  _repository.Add(author);
        return author;
    }

    public async Task<Author> Update(Author author)
    {
        if (author == null)
        {
            throw new ArgumentNullException(nameof(author));
        }

        var existingAuthor = await _repository.GetOne(author.Id);
        if (existingAuthor == null)
        {
            throw new KeyNotFoundException("Author not found");
        }
        
        await  _repository.Update(author);
        return author;
    }

    public async Task<bool> Delete(int id)
    {
        var author = await _repository.GetOne(id);
        if (author == null)
        {
            return false;
        }
        await  _repository.Delete(id);
        return true;
    }
}