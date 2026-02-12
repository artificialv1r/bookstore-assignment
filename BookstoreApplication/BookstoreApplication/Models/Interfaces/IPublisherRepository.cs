using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Models.Interfaces;

public interface IPublisherRepository
{
    Task<List<Publisher>> GetAll();
    Task<Publisher?> GetOne(int id);
    Task<Publisher> Add(Publisher publisher);
    Task<Publisher> Update(Publisher publisher);
    Task Delete(int id);
    Task<PaginatedList<Publisher>> GetAllSorted(int page, PublisherSortType sortType);
}