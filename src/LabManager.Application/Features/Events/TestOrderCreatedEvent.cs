using System;
using MediatR;

namespace LabManager.Application.Features.Events;

public class TestOrderCreatedEvent : INotification
{
    public Guid TestOrderId { get; }
    public string PatientName { get; }
    public string CreatedBy { get; }

    public TestOrderCreatedEvent(Guid testOrderId, string patientName, string createdBy)
    {
        TestOrderId = testOrderId;
        PatientName = patientName;
        CreatedBy = createdBy;
    }
}
