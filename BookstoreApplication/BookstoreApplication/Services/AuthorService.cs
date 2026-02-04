
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Services.Interfaces;

namespace BookstoreApplication.Services;

public class AuthorService: IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly ILogger<AuthorService> _logger;

    public AuthorService(IAuthorRepository repository, ILogger<AuthorService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<Author>> GetAll()
    {
        _logger.LogInformation("Getting all authors");
        return await  _repository.GetAll();
    }

    public async Task<Author> GetOne(int id)
    {
        _logger.LogInformation($"Getting author with id:{id}");
        var author = await  _repository.GetOne(id);
        if (author == null)
        {
            _logger.LogError($"No author with id:{id}");
            throw new KeyNotFoundException($"Author with id {id} not found");
        }
        _logger.LogInformation($"Returning author with id:{author.Id}");
        return author;
    }

    public async Task<Author> Create(Author author)
    {
        _logger.LogInformation($"Creating author {author.FullName}.");
        if (author == null)
        {
            _logger.LogError($"Failed to create author with");
            throw new ArgumentNullException(nameof(author));
        }
        
        await  _repository.Add(author);
        _logger.LogInformation($"Created author {author.FullName}.");
        return author;
    }

    public async Task<Author> Update(Author author)
    {
        _logger.LogInformation($"Updating author {author.FullName} with id:{author.Id}.");
        if (author == null)
        {
            _logger.LogError($"Author with id:{author.Id} not found.");
            throw new NotFoundException(author.Id);
        }

        var existingAuthor = await _repository.GetOne(author.Id);
        if (existingAuthor == null)
        {
            _logger.LogError($"Author with id:{existingAuthor.Id} not found");
            throw new NotFoundException(existingAuthor.Id);
        }
        
        await  _repository.Update(author);
        _logger.LogInformation($"Updated author with id:{author.Id}.");
        return author;
    }

    public async Task<bool> Delete(int id)
    {
        _logger.LogInformation($"Deleting author with id:{id}");
        var author = await _repository.GetOne(id);
        if (author == null)
        {
            _logger.LogError($"Author with id:{id} not found");
            return false;
        }
        await  _repository.Delete(id);
        _logger.LogInformation($"Deleted author with id:{id}.");
        return true;
    }
}