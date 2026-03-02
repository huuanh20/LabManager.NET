using LabManager.Application.Interfaces;
using LabManager.Domain.Entities;
using LabManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LabManager.Infrastructure.Repositories;

public class TestOrderRepository : ITestOrderRepository
{
    private readonly LabManagerDbContext _context;

    public TestOrderRepository(LabManagerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TestOrder>> GetAllAsync()
        => await _context.TestOrders.Include(o => o.Patient).Include(o => o.Lab).ToListAsync();

    public async Task<TestOrder?> GetByIdAsync(int id)
        => await _context.TestOrders
            .Include(o => o.Patient)
            .Include(o => o.Lab)
            .Include(o => o.Samples)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<TestOrder>> GetByPatientIdAsync(int patientId)
        => await _context.TestOrders.Where(o => o.PatientId == patientId).ToListAsync();

    public async Task<TestOrder> AddAsync(TestOrder testOrder)
    {
        _context.TestOrders.Add(testOrder);
        await _context.SaveChangesAsync();
        return testOrder;
    }

    public async Task UpdateAsync(TestOrder testOrder)
    {
        _context.TestOrders.Update(testOrder);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _context.TestOrders.FindAsync(id);
        if (order is not null)
        {
            _context.TestOrders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
