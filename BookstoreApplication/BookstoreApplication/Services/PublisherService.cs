using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Services.Interfaces;
using BookstoreApplication.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services;

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepository _repository;
    private readonly ILogger<PublisherService> _logger;

    public PublisherService(IPublisherRepository repository, ILogger<PublisherService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<Publisher>> GetAll()
    {
        _logger.LogInformation($"Retrieving all publishers.");
        return await  _repository.GetAll();
    }

    public async Task<Publisher> GetOne(int id)
    {
        _logger.LogInformation($"Retrieving publisher with id:{id}.");
        var publisher = await _repository.GetOne(id);
            
        if (publisher == null)
        {
            _logger.LogError($"Publisher with id:{id} was not found.");
            throw new NotFoundException(publisher.Id);
        }
        _logger.LogInformation($"Retrieved publisher {publisher.Name} with id:{id}.");
        return publisher;
    }

    public async Task<Publisher> Add(Publisher publisher)
    {
        _logger.LogInformation($"Adding publisher {publisher.Name}.");
        if (publisher == null)
        {
            _logger.LogError($"Failed to add publisher.");
            throw new BadRequestException("Failed to add publisher");
        }
        
        await  _repository.Add(publisher);
        _logger.LogInformation($"Added publisher {publisher.Name}.");
        return publisher;
    }
    
    public async Task<Publisher> Update(Publisher publisher)
    {
        _logger.LogInformation($"Updating publisher {publisher.Name} with id:{publisher.Id}.");
        if (publisher == null)
        {
            _logger.LogError($"Failed to update publisher.");
            throw new BadRequestException("Failed to update publisher");
        }
        
        var existingPublisher = await _repository.GetOne(publisher.Id);
        if (existingPublisher == null)
        {
            _logger.LogError($"Publisher with id:{publisher.Id} was not found.");
            throw new NotFoundException(publisher.Id);
        }
        
        await  _repository.Update(publisher);
        _logger.LogInformation($"Updated publisher {publisher.Name} with id:{publisher.Id}.");
        return publisher;
    }
    
    public async Task<bool> Delete(int id)
    {
        _logger.LogInformation($"Deleting publisher with id:{id}.");
        var publisher = await _repository.GetOne(id);
        if (publisher == null)
        {
            _logger.LogError($"Publisher with id:{id} was not found.");
            return false;
        }
        await  _repository.Delete(id);
        _logger.LogInformation($"Deleted publisher {publisher.Name} with id:{publisher.Id}.");
        return true;
    }

    public async Task<PaginatedList<Publisher>> GetAllSorted(int page, PublisherSortType sortType)
    {
        int PageSize = 5;
        var publishers = await _repository.GetAllSorted(page, sortType);
        var items = publishers.Items;
        return new PaginatedList<Publisher>(items, publishers.Count, publishers.PageIndex, PageSize);
    }
}