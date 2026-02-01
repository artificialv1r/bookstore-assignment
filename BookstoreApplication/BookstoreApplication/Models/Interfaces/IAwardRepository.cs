namespace BookstoreApplication.Models.Interfaces;

public interface IAwardRepository
{
    Task<List<Award>> GetAll();
    Task<Award> GetOne(int id);
    Task<Award> Add(Award award);
    Task<Award> Update(Award award);
    Task<bool> Delete(int id);
}