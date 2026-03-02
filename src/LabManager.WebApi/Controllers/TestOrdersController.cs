using LabManager.Application.DTOs;
using LabManager.Application.Interfaces;
using LabManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LabManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestOrdersController : ControllerBase
{
    private readonly ITestOrderRepository _testOrderRepository;

    public TestOrdersController(ITestOrderRepository testOrderRepository)
    {
        _testOrderRepository = testOrderRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TestOrderDto>>> GetAll()
    {
        var orders = await _testOrderRepository.GetAllAsync();
        var dtos = orders.Select(o => new TestOrderDto
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            OrderedAt = o.OrderedAt,
            Status = o.Status,
            TestName = o.TestName,
            Result = o.Result,
            CompletedAt = o.CompletedAt,
            Notes = o.Notes,
            PatientId = o.PatientId,
            LabId = o.LabId
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TestOrderDto>> GetById(int id)
    {
        var order = await _testOrderRepository.GetByIdAsync(id);
        if (order is null)
            return NotFound();

        return Ok(new TestOrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            OrderedAt = order.OrderedAt,
            Status = order.Status,
            TestName = order.TestName,
            Result = order.Result,
            CompletedAt = order.CompletedAt,
            Notes = order.Notes,
            PatientId = order.PatientId,
            LabId = order.LabId
        });
    }

    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<TestOrderDto>>> GetByPatient(int patientId)
    {
        var orders = await _testOrderRepository.GetByPatientIdAsync(patientId);
        var dtos = orders.Select(o => new TestOrderDto
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            OrderedAt = o.OrderedAt,
            Status = o.Status,
            TestName = o.TestName,
            Result = o.Result,
            CompletedAt = o.CompletedAt,
            Notes = o.Notes,
            PatientId = o.PatientId,
            LabId = o.LabId
        });
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<TestOrderDto>> Create(TestOrderDto dto)
    {
        var order = new TestOrder
        {
            OrderNumber = dto.OrderNumber,
            OrderedAt = dto.OrderedAt == default ? DateTime.UtcNow : dto.OrderedAt,
            Status = dto.Status,
            TestName = dto.TestName,
            Result = dto.Result,
            CompletedAt = dto.CompletedAt,
            Notes = dto.Notes,
            PatientId = dto.PatientId,
            LabId = dto.LabId
        };
        var created = await _testOrderRepository.AddAsync(order);
        dto.Id = created.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TestOrderDto dto)
    {
        var order = await _testOrderRepository.GetByIdAsync(id);
        if (order is null)
            return NotFound();

        order.OrderNumber = dto.OrderNumber;
        order.Status = dto.Status;
        order.TestName = dto.TestName;
        order.Result = dto.Result;
        order.CompletedAt = dto.CompletedAt;
        order.Notes = dto.Notes;
        order.PatientId = dto.PatientId;
        order.LabId = dto.LabId;

        await _testOrderRepository.UpdateAsync(order);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _testOrderRepository.GetByIdAsync(id);
        if (order is null)
            return NotFound();

        await _testOrderRepository.DeleteAsync(id);
        return NoContent();
    }
}
