using LabManager.Domain.Entities;

namespace LabManager.Application.Interfaces;

public interface ITestOrderRepository
{
    Task<IEnumerable<TestOrder>> GetAllAsync();
    Task<TestOrder?> GetByIdAsync(int id);
    Task<IEnumerable<TestOrder>> GetByPatientIdAsync(int patientId);
    Task<TestOrder> AddAsync(TestOrder testOrder);
    Task UpdateAsync(TestOrder testOrder);
    Task DeleteAsync(int id);
}
