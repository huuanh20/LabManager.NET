using LabManager.Application.DTOs;
using LabManager.Application.Interfaces;
using LabManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LabManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentController(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAll()
    {
        var equipment = await _equipmentRepository.GetAllAsync();
        var dtos = equipment.Select(e => new EquipmentDto
        {
            Id = e.Id,
            Name = e.Name,
            Model = e.Model,
            SerialNumber = e.SerialNumber,
            Status = e.Status,
            LastMaintenanceDate = e.LastMaintenanceDate,
            NextMaintenanceDate = e.NextMaintenanceDate,
            LabId = e.LabId
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EquipmentDto>> GetById(int id)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(id);
        if (equipment is null)
            return NotFound();

        return Ok(new EquipmentDto
        {
            Id = equipment.Id,
            Name = equipment.Name,
            Model = equipment.Model,
            SerialNumber = equipment.SerialNumber,
            Status = equipment.Status,
            LastMaintenanceDate = equipment.LastMaintenanceDate,
            NextMaintenanceDate = equipment.NextMaintenanceDate,
            LabId = equipment.LabId
        });
    }

    [HttpGet("lab/{labId}")]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetByLabId(int labId)
    {
        var equipment = await _equipmentRepository.GetByLabIdAsync(labId);
        var dtos = equipment.Select(e => new EquipmentDto
        {
            Id = e.Id,
            Name = e.Name,
            Model = e.Model,
            SerialNumber = e.SerialNumber,
            Status = e.Status,
            LastMaintenanceDate = e.LastMaintenanceDate,
            NextMaintenanceDate = e.NextMaintenanceDate,
            LabId = e.LabId
        });
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<EquipmentDto>> Create(EquipmentDto dto)
    {
        var equipment = new Equipment
        {
            Name = dto.Name,
            Model = dto.Model,
            SerialNumber = dto.SerialNumber,
            Status = dto.Status,
            LastMaintenanceDate = dto.LastMaintenanceDate,
            NextMaintenanceDate = dto.NextMaintenanceDate,
            LabId = dto.LabId
        };
        var created = await _equipmentRepository.AddAsync(equipment);
        dto.Id = created.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EquipmentDto dto)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(id);
        if (equipment is null)
            return NotFound();

        equipment.Name = dto.Name;
        equipment.Model = dto.Model;
        equipment.SerialNumber = dto.SerialNumber;
        equipment.Status = dto.Status;
        equipment.LastMaintenanceDate = dto.LastMaintenanceDate;
        equipment.NextMaintenanceDate = dto.NextMaintenanceDate;
        equipment.LabId = dto.LabId;

        await _equipmentRepository.UpdateAsync(equipment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(id);
        if (equipment is null)
            return NotFound();

        await _equipmentRepository.DeleteAsync(id);
        return NoContent();
    }
}
