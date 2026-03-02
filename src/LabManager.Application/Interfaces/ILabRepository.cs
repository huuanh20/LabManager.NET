using LabManager.Domain.Entities;

namespace LabManager.Application.Interfaces;

public interface ILabRepository
{
    Task<IEnumerable<Lab>> GetAllAsync();
    Task<Lab?> GetByIdAsync(int id);
    Task<Lab> AddAsync(Lab lab);
    Task UpdateAsync(Lab lab);
    Task DeleteAsync(int id);
}
