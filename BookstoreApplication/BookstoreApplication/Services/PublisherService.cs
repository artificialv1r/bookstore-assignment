using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services;

public class PublisherService
{
    private readonly PublisherRepository _publisherRepository;

    public PublisherService(AppDbContext context)
    {
        _publisherRepository = new PublisherRepository(context);
    }

    public async Task<List<Publisher>> GetAll()
    {
        return await  _publisherRepository.GetAll();
    }

    public async Task<Publisher> GetOne(int id)
    {
        var publisher = await _publisherRepository.GetOne(id);
            
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
        
        await  _publisherRepository.Add(publisher);
        return publisher;
    }
    
    public async Task<Publisher> Update(Publisher publisher)
    {
        if (publisher == null)
        {
            throw new ArgumentNullException(nameof(publisher));
        }
        
        var existingPublisher = await _publisherRepository.GetOne(publisher.Id);
        if (existingPublisher == null)
        {
            throw new KeyNotFoundException($"Publisher with id {publisher.Id} was not found.");
        }
        
        await  _publisherRepository.Update(publisher);
        return publisher;
    }
    
    public async Task<bool> Delete(int id)
    {
        var publisher = await _publisherRepository.GetOne(id);
        if (publisher == null)
        {
            return false;
        }
        await  _publisherRepository.Delete(id);
        return true;
    } 
}