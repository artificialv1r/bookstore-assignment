namespace BookstoreApplication.Models.Interfaces;

public interface IPublisherRepository
{
    Task<List<Publisher>> GetAll();
    Task<Publisher?> GetOne(int id);
    Task<Publisher> Add(Publisher publisher);
    Task<Publisher> Update(Publisher publisher);
    Task Delete(int id);
}