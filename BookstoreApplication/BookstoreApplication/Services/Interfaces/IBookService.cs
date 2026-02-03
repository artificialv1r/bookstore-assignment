using BookstoreApplication.Models;
using BookstoreApplication.Services.DTOs;

namespace BookstoreApplication.Services.Interfaces;

public interface IBookService
{
    Task<List<BookDto>> GetAll();
    Task<BookDetailsDto> GetById(int id);
    Task<Book> Create(Book book);
    Task<Book> Update(Book book);
    Task<bool> Delete(int id);
}