using LabManager.Application.DTOs;
using LabManager.Application.Interfaces;
using LabManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LabManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LabsController : ControllerBase
{
    private readonly ILabRepository _labRepository;

    public LabsController(ILabRepository labRepository)
    {
        _labRepository = labRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LabDto>>> GetAll()
    {
        var labs = await _labRepository.GetAllAsync();
        var dtos = labs.Select(l => new LabDto
        {
            Id = l.Id,
            Name = l.Name,
            Location = l.Location,
            Description = l.Description,
            ContactEmail = l.ContactEmail,
            ContactPhone = l.ContactPhone
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LabDto>> GetById(int id)
    {
        var lab = await _labRepository.GetByIdAsync(id);
        if (lab is null)
            return NotFound();

        return Ok(new LabDto
        {
            Id = lab.Id,
            Name = lab.Name,
            Location = lab.Location,
            Description = lab.Description,
            ContactEmail = lab.ContactEmail,
            ContactPhone = lab.ContactPhone
        });
    }

    [HttpPost]
    public async Task<ActionResult<LabDto>> Create(LabDto dto)
    {
        var lab = new Lab
        {
            Name = dto.Name,
            Location = dto.Location,
            Description = dto.Description,
            ContactEmail = dto.ContactEmail,
            ContactPhone = dto.ContactPhone
        };
        var created = await _labRepository.AddAsync(lab);
        dto.Id = created.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, LabDto dto)
    {
        var lab = await _labRepository.GetByIdAsync(id);
        if (lab is null)
            return NotFound();

        lab.Name = dto.Name;
        lab.Location = dto.Location;
        lab.Description = dto.Description;
        lab.ContactEmail = dto.ContactEmail;
        lab.ContactPhone = dto.ContactPhone;

        await _labRepository.UpdateAsync(lab);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var lab = await _labRepository.GetByIdAsync(id);
        if (lab is null)
            return NotFound();

        await _labRepository.DeleteAsync(id);
        return NoContent();
    }
}
