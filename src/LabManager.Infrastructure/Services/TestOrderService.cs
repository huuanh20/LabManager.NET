using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabManager.Application.DTOs.TestOrder;
using LabManager.Application.Interfaces;
using LabManager.Application.Interfaces.Services;
using LabManager.Domain.Entities;
using LabManager.Domain.Enums;

namespace LabManager.Infrastructure.Services;

public class TestOrderService : ITestOrderService
{
    private readonly IGenericRepository<TestOrder> _testOrderRepository;
    private readonly IGenericRepository<Patient> _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly MediatR.IPublisher _publisher;

    public TestOrderService(
        IGenericRepository<TestOrder> testOrderRepository,
        IGenericRepository<Patient> patientRepository,
        IUnitOfWork unitOfWork,
        MediatR.IPublisher publisher)
    {
        _testOrderRepository = testOrderRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<IReadOnlyList<TestOrderDto>> GetAllAsync()
    {
        var testOrders = await _testOrderRepository.GetAllAsync();
        return testOrders.Select(t => new TestOrderDto
        {
            Id = t.Id,
            PatientId = t.PatientId,
            PatientName = t.Patient?.FullName ?? "Unknown", // Simplified for demo
            Status = t.Status,
            CreatedOn = t.CreatedOn,
            RawResults = t.RawResults,
            ProcessedResults = t.ProcessedResults
        }).OrderByDescending(x => x.CreatedOn).ToList();
    }

    public async Task<TestOrderDto?> GetByIdAsync(Guid id)
    {
        var t = await _testOrderRepository.GetByIdAsync(id);
        if (t == null) return null;

        var patient = await _patientRepository.GetByIdAsync(t.PatientId);

        return new TestOrderDto
        {
            Id = t.Id,
            PatientId = t.PatientId,
            PatientName = patient?.FullName ?? "Unknown",
            Status = t.Status,
            CreatedOn = t.CreatedOn,
            RawResults = t.RawResults,
            ProcessedResults = t.ProcessedResults
        };
    }

    public async Task<TestOrderDto> CreateAsync(CreateTestOrderDto request, string createdBy)
    {
        var entity = new TestOrder
        {
            Id = Guid.NewGuid(),
            PatientId = request.PatientId,
            Status = TestOrderStatus.Pending,
            CreatedBy = createdBy
        };

        var createdEntity = await _testOrderRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        var patient = await _patientRepository.GetByIdAsync(request.PatientId);
        
        // Publish Domain Event for Monitoring
        await _publisher.Publish(new Application.Features.Events.TestOrderCreatedEvent(
            createdEntity.Id, 
            patient?.FullName ?? "Unknown", 
            createdBy));

        return new TestOrderDto
        {
            Id = createdEntity.Id,
            PatientId = createdEntity.PatientId,
            Status = createdEntity.Status,
            CreatedOn = createdEntity.CreatedOn
        };
    }

    public async Task<bool> UpdateResultAsync(Guid id, UpdateTestOrderResultDto request)
    {
        var testOrder = await _testOrderRepository.GetByIdAsync(id);
        if (testOrder == null) return false;

        testOrder.RawResults = request.RawResults;
        testOrder.ProcessedResults = request.ProcessedResults;
        testOrder.Status = TestOrderStatus.Completed;
        testOrder.RunDate = DateTime.UtcNow;

        await _testOrderRepository.UpdateAsync(testOrder);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
