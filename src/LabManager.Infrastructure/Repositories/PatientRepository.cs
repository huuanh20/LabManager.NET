using LabManager.Application.Interfaces;
using LabManager.Domain.Entities;
using LabManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LabManager.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly LabManagerDbContext _context;

    public PatientRepository(LabManagerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
        => await _context.Patients.ToListAsync();

    public async Task<Patient?> GetByIdAsync(int id)
        => await _context.Patients.Include(p => p.TestOrders).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Patient> AddAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient is not null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}
