using LabManager.Domain.Entities;

namespace LabManager.Application.Interfaces;

public interface IEquipmentRepository
{
    Task<IEnumerable<Equipment>> GetAllAsync();
    Task<Equipment?> GetByIdAsync(int id);
    Task<IEnumerable<Equipment>> GetByLabIdAsync(int labId);
    Task<Equipment> AddAsync(Equipment equipment);
    Task UpdateAsync(Equipment equipment);
    Task DeleteAsync(int id);
}
