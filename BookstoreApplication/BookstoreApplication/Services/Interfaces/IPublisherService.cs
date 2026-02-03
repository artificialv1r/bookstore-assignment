using BookstoreApplication.Models;

namespace BookstoreApplication.Services.Interfaces;

public interface IPublisherService
{
    Task<List<Publisher>> GetAll();
    Task<Publisher> GetOne(int id);
    Task<Publisher> Add(Publisher publisher);
    Task<Publisher> Update(Publisher publisher);
    Task<bool> Delete(int id);
}