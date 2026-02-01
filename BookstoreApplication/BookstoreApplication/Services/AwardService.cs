using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;

namespace BookstoreApplication.Services;

public class AwardService
{
    private readonly IAwardRepository _repository;
    public AwardService(IAwardRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Award>> GetAll()
    {
        return await _repository.GetAll();
    }
    
    public async Task<Award> GetOne(int id)
    {
        var award = await _repository.GetOne(id);
        if (award == null)
        {
            throw new KeyNotFoundException($"Award with id {id} not found");
        }
        return award;
    }

    public async Task<Award> Create(Award award)
    {
        if (award == null)
        {
            throw new ArgumentException(nameof(award));
        }
        
        await _repository.Add(award);
        return award;
    }

    public async Task<Award> Update(Award award)
    {
        if (award == null)
        {
            throw new ArgumentNullException(nameof(award));
        }

        var existingAward = await _repository.GetOne(award.Id);
        if (existingAward == null)
        {
            throw new KeyNotFoundException("Award not found");
        }
        
        await _repository.Update(award);
        return award;
    }

    public async Task<bool> Delete(int id)
    {
        var award = await _repository.GetOne(id);
        if (award == null)
        {
            return false;
        }

        await _repository.Delete(id);
        return true;
    }
}