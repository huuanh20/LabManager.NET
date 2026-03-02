using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabManager.Application.DTOs.Patient;

namespace LabManager.Application.Interfaces.Services;

public interface IPatientService
{
    Task<IReadOnlyList<PatientDto>> GetAllAsync();
    Task<PatientDto?> GetByIdAsync(Guid id);
    Task<PatientDto> CreateAsync(CreatePatientDto request);
    Task<bool> UpdateAsync(Guid id, UpdatePatientDto request);
    Task<bool> DeleteAsync(Guid id);
}
