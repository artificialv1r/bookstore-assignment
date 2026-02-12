using BookstoreApplication.Models;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services.Interfaces;

public interface IPublisherService
{
    Task<List<Publisher>> GetAll();
    Task<Publisher> GetOne(int id);
    Task<Publisher> Add(Publisher publisher);
    Task<Publisher> Update(Publisher publisher);
    Task<bool> Delete(int id);
    Task<PaginatedList<Publisher>> GetAllSorted(int page, PublisherSortType sortType);
}