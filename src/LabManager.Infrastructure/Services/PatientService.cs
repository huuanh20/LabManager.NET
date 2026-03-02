using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabManager.Application.DTOs.Patient;
using LabManager.Application.Interfaces;
using LabManager.Application.Interfaces.Services;
using LabManager.Domain.Entities;

namespace LabManager.Infrastructure.Services;

public class PatientService : IPatientService
{
    private readonly IGenericRepository<Patient> _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IGenericRepository<Patient> patientRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<PatientDto>> GetAllAsync()
    {
        var patients = await _patientRepository.GetAllAsync();
        return patients.Select(p => new PatientDto
        {
            Id = p.Id,
            FullName = p.FullName,
            Email = p.Email,
            PhoneNumber = p.PhoneNumber,
            DateOfBirth = p.DateOfBirth,
            Gender = p.Gender,
            Address = p.Address
        }).ToList();
    }

    public async Task<PatientDto?> GetByIdAsync(Guid id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null) return null;

        return new PatientDto
        {
            Id = patient.Id,
            FullName = patient.FullName,
            Email = patient.Email,
            PhoneNumber = patient.PhoneNumber,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            Address = patient.Address
        };
    }

    public async Task<PatientDto> CreateAsync(CreatePatientDto request)
    {
        var entity = new Patient
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Address = request.Address
        };

        var createdEntity = await _patientRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return new PatientDto
        {
            Id = createdEntity.Id,
            FullName = createdEntity.FullName,
            Email = createdEntity.Email,
            PhoneNumber = createdEntity.PhoneNumber,
            DateOfBirth = createdEntity.DateOfBirth,
            Gender = createdEntity.Gender,
            Address = createdEntity.Address
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePatientDto request)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null) return false;

        patient.FullName = request.FullName;
        patient.Email = request.Email;
        patient.PhoneNumber = request.PhoneNumber;
        patient.DateOfBirth = request.DateOfBirth;
        patient.Gender = request.Gender;
        patient.Address = request.Address;

        await _patientRepository.UpdateAsync(patient);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null) return false;

        await _patientRepository.DeleteAsync(patient);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
