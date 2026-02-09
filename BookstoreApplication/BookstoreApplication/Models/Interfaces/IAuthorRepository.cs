using BookstoreApplication.Utils;

namespace BookstoreApplication.Models.Interfaces;

public interface IAuthorRepository
{
    Task<List<Author>> GetAll();
    Task<Author?> GetOne(int id);
    Task<Author> Add(Author author);
    Task<Author> Update(Author author);
    Task Delete(int id);
    Task<PaginatedList<Author>> GetAllPaged(int page);
}