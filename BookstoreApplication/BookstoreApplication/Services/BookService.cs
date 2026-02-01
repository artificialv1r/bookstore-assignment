using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services;

public class BookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Book>> GetAll()
    {
        return await  _repository.GetAll();
    }

    public async Task<Book> GetById(int id)
    {
        var book = await _repository.GetOne(id);

        if (book == null)
        {
            throw new  KeyNotFoundException("Book not found");
        }
        
        return book;
    }

    public async Task<Book> Create(Book book)
    {
        if (book == null)
        {
            throw new ArgumentNullException(nameof(book));
        }
        
        await _repository.Add(book);
        return book;
    }

    public async Task<Book> Update(Book book)
    {
        if (book == null)
        {
            throw new ArgumentNullException(nameof(book));
        }
        
        await  _repository.Update(book);
        return book;
    }

    public async Task<bool> Delete(int id)
    {
        var book = await _repository.GetOne(id);
        if (book == null)
        {
            return false;
        }
        await _repository.Delete(book);
        return true;
    }
}