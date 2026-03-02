using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabManager.Application.DTOs.TestOrder;

namespace LabManager.Application.Interfaces.Services;

public interface ITestOrderService
{
    Task<IReadOnlyList<TestOrderDto>> GetAllAsync();
    Task<TestOrderDto?> GetByIdAsync(Guid id);
    Task<TestOrderDto> CreateAsync(CreateTestOrderDto request, string createdBy);
    Task<bool> UpdateResultAsync(Guid id, UpdateTestOrderResultDto request);
}
