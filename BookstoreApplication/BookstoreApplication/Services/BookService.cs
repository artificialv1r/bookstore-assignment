using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Services.Interfaces;

namespace BookstoreApplication.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<BookDto>> GetAll()
    {
        var books = await _repository.GetAll();
        return books
            .Select(_mapper.Map<BookDto>)
            .ToList();
    }

    public async Task<BookDetailsDto> GetById(int id)
    {
        var book = await _repository.GetOne(id);

        if (book == null)
        {
            throw new  KeyNotFoundException("Book not found");
        }
        
        return _mapper.Map<BookDetailsDto>(book);
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