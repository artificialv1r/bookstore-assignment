using BookstoreApplication.Models;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services.Interfaces;

public interface IAuthorService
{
    Task<List<Author>> GetAll();
    Task<Author> GetOne(int id);
    Task<Author> Create(Author author);
    Task<Author> Update(Author author);
    Task<bool> Delete(int id);
    Task<PaginatedList<AuthorDto>> GetAuthorsPage(int page);
}