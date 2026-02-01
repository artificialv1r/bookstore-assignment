namespace BookstoreApplication.Models.Interfaces;

public interface IAuthorRepository
{
    Task<List<Author>> GetAll();
    Task<Author?> GetOne(int id);
    Task<Author> Add(Author author);
    Task<Author> Update(Author author);
    Task Delete(int id);
}