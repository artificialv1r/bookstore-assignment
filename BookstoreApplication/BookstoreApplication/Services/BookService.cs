using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Services.Interfaces;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<BookService> _logger;

    public BookService(IBookRepository repository, IMapper mapper, ILogger<BookService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<BookDto>> GetAll()
    {
        _logger.LogInformation("Getting all books");
        var books = await _repository.GetAll();
        _logger.LogInformation($"Returned {books.Count} books");
        return books
            .Select(_mapper.Map<BookDto>)
            .ToList();
    }

    public async Task<BookDetailsDto> GetById(int id)
    {
        _logger.LogInformation($"Getting book with id:{id}");
        var book = await _repository.GetOne(id);

        if (book == null)
        {
            _logger.LogError($"Book with id:{id} not found");
            throw new NotFoundException(id);
        }
        _logger.LogInformation($"Returning book with id:{book.Id}");
        return _mapper.Map<BookDetailsDto>(book);
    }

    public async Task<Book> Create(Book book)
    {
        _logger.LogInformation("Creating new book");
        if (book == null)
        {
            _logger.LogError($"Failed to create new book");
            throw new ArgumentNullException(nameof(book));
        }
        
        await _repository.Add(book);
        _logger.LogInformation($"Book with id:{book.Id}, title:{book.Title} created");
        return book;
    }

    public async Task<Book> Update(Book book)
    {
        _logger.LogInformation($"Updating book with id:{book.Id}");
        if (book == null)
        {
            _logger.LogError($"Failed to update book with id:{book.Id}");
            throw new ArgumentNullException(nameof(book));
        }
        
        await  _repository.Update(book);
        _logger.LogInformation($"Book with id:{book.Id}, title:{book.Title} updated successfully");
        return book;
    }

    public async Task<bool> Delete(int id)
    {
        _logger.LogInformation($"Deleting book with id:{id}");
        var book = await _repository.GetOne(id);
        if (book == null)
        {
            _logger.LogError($"Book with id:{id} not found");
            return false;
        }
        await _repository.Delete(book);
        _logger.LogInformation($"Book with id:{book.Id}, deleted successfully");
        return true;
    }

    public async Task<PaginatedList<BookDetailsDto>> GetAllSorted(int page, BookSortType sortType)
    {
        int PageSize = 5;
        var books = await _repository.GetAllSorted(page, sortType);
        var dtos = books.Items
            .Select(_mapper.Map<BookDetailsDto>).ToList();
        return new PaginatedList<BookDetailsDto>(dtos, books.Count, books.PageIndex, PageSize);
    }
}