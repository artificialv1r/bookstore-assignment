using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Models.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAll();
    Task<Book> GetOne(int id);
    Task<Book> Add(Book book);
    Task<Book> Update(Book book);
    Task Delete(Book book);
    Task<PaginatedList<Book>> GetAllSorted(int page, BookSortType sortType);
}