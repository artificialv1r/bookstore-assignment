using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services;

public class BookService
{
    private readonly BookRepository _bookRepository;

    public BookService(AppDbContext context)
    {
        _bookRepository = new BookRepository(context);
    }

    public async Task<List<Book>> GetAll()
    {
        return await  _bookRepository.GetAll();
    }

    public async Task<Book> GetById(int id)
    {
        var book = await _bookRepository.GetOne(id);

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
        
        await _bookRepository.Add(book);
        return book;
    }

    public async Task<Book> Update(Book book)
    {
        if (book == null)
        {
            throw new ArgumentNullException(nameof(book));
        }
        
        await  _bookRepository.Update(book);
        return book;
    }

    public async Task<bool> Delete(int id)
    {
        var book = await _bookRepository.GetOne(id);
        if (book == null)
        {
            return false;
        }
        await _bookRepository.Delete(book);
        return true;
    }
}