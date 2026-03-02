using LabManager.Application.Interfaces;
using LabManager.Domain.Entities;
using LabManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LabManager.Infrastructure.Repositories;

public class LabRepository : ILabRepository
{
    private readonly LabManagerDbContext _context;

    public LabRepository(LabManagerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Lab>> GetAllAsync()
        => await _context.Labs.ToListAsync();

    public async Task<Lab?> GetByIdAsync(int id)
        => await _context.Labs.Include(l => l.Equipment).FirstOrDefaultAsync(l => l.Id == id);

    public async Task<Lab> AddAsync(Lab lab)
    {
        _context.Labs.Add(lab);
        await _context.SaveChangesAsync();
        return lab;
    }

    public async Task UpdateAsync(Lab lab)
    {
        _context.Labs.Update(lab);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var lab = await _context.Labs.FindAsync(id);
        if (lab is not null)
        {
            _context.Labs.Remove(lab);
            await _context.SaveChangesAsync();
        }
    }
}
