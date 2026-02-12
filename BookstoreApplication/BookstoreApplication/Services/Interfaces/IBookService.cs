using BookstoreApplication.Models;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services.Interfaces;

public interface IBookService
{
    Task<List<BookDto>> GetAll();
    Task<BookDetailsDto> GetById(int id);
    Task<Book> Create(Book book);
    Task<Book> Update(Book book);
    Task<bool> Delete(int id);
    Task<PaginatedList<BookDetailsDto>> GetAllSorted(int page, BookSortType sortType);
}