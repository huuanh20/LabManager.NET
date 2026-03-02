using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LabManager.Application.DTOs.TestOrder;
using LabManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabManager.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TestOrderController : ControllerBase
{
    private readonly ITestOrderService _testOrderService;

    public TestOrderController(ITestOrderService testOrderService)
    {
        _testOrderService = testOrderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var testOrders = await _testOrderService.GetAllAsync();
        return Ok(testOrders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var testOrder = await _testOrderService.GetByIdAsync(id);
        if (testOrder == null) return NotFound();
        return Ok(testOrder);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTestOrderDto request)
    {
        var userName = User.FindFirstValue(ClaimTypes.Name) ?? "System";
        var testOrder = await _testOrderService.CreateAsync(request, userName);
        return CreatedAtAction(nameof(GetById), new { id = testOrder.Id }, testOrder);
    }

    [HttpPut("{id}/result")]
    public async Task<IActionResult> UpdateResult(Guid id, [FromBody] UpdateTestOrderResultDto request)
    {
        var result = await _testOrderService.UpdateResultAsync(id, request);
        if (!result) return NotFound();
        return NoContent();
    }
}
