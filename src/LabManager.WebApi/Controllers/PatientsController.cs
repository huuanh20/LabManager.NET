using LabManager.Application.DTOs;
using LabManager.Application.Interfaces;
using LabManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LabManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;

    public PatientsController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
    {
        var patients = await _patientRepository.GetAllAsync();
        var dtos = patients.Select(p => new PatientDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            DateOfBirth = p.DateOfBirth,
            Gender = p.Gender,
            PhoneNumber = p.PhoneNumber,
            Email = p.Email,
            Address = p.Address
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientDto>> GetById(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient is null)
            return NotFound();

        return Ok(new PatientDto
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            PhoneNumber = patient.PhoneNumber,
            Email = patient.Email,
            Address = patient.Address
        });
    }

    [HttpPost]
    public async Task<ActionResult<PatientDto>> Create(PatientDto dto)
    {
        var patient = new Patient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Address = dto.Address
        };
        var created = await _patientRepository.AddAsync(patient);
        dto.Id = created.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PatientDto dto)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient is null)
            return NotFound();

        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.DateOfBirth = dto.DateOfBirth;
        patient.Gender = dto.Gender;
        patient.PhoneNumber = dto.PhoneNumber;
        patient.Email = dto.Email;
        patient.Address = dto.Address;

        await _patientRepository.UpdateAsync(patient);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient is null)
            return NotFound();

        await _patientRepository.DeleteAsync(id);
        return NoContent();
    }
}
