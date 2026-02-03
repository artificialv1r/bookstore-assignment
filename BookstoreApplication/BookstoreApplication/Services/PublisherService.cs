using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services;

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepository _repository;

    public PublisherService(IPublisherRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Publisher>> GetAll()
    {
        return await  _repository.GetAll();
    }

    public async Task<Publisher> GetOne(int id)
    {
        var publisher = await _repository.GetOne(id);
            
        if (publisher == null)
        {
            throw new KeyNotFoundException($"Publisher with id {id} was not found.");
        }

        return publisher;
    }

    public async Task<Publisher> Add(Publisher publisher)
    {
        if (publisher == null)
        {
            throw new ArgumentNullException(nameof(publisher));
        }
        
        await  _repository.Add(publisher);
        return publisher;
    }
    
    public async Task<Publisher> Update(Publisher publisher)
    {
        if (publisher == null)
        {
            throw new ArgumentNullException(nameof(publisher));
        }
        
        var existingPublisher = await _repository.GetOne(publisher.Id);
        if (existingPublisher == null)
        {
            throw new KeyNotFoundException($"Publisher with id {publisher.Id} was not found.");
        }
        
        await  _repository.Update(publisher);
        return publisher;
    }
    
    public async Task<bool> Delete(int id)
    {
        var publisher = await _repository.GetOne(id);
        if (publisher == null)
        {
            return false;
        }
        await  _repository.Delete(id);
        return true;
    } 
}