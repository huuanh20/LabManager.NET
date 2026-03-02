using LabManager.Application.Interfaces;
using LabManager.Domain.Entities;
using LabManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LabManager.Infrastructure.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly LabManagerDbContext _context;

    public EquipmentRepository(LabManagerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Equipment>> GetAllAsync()
        => await _context.Equipment.ToListAsync();

    public async Task<Equipment?> GetByIdAsync(int id)
        => await _context.Equipment.FindAsync(id);

    public async Task<IEnumerable<Equipment>> GetByLabIdAsync(int labId)
        => await _context.Equipment.Where(e => e.LabId == labId).ToListAsync();

    public async Task<Equipment> AddAsync(Equipment equipment)
    {
        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();
        return equipment;
    }

    public async Task UpdateAsync(Equipment equipment)
    {
        _context.Equipment.Update(equipment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var equipment = await _context.Equipment.FindAsync(id);
        if (equipment is not null)
        {
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
        }
    }
}
