using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LabManager.Application.Features.Events;

public class TestOrderCreatedEventHandler : INotificationHandler<TestOrderCreatedEvent>
{
    private readonly ILogger<TestOrderCreatedEventHandler> _logger;

    public TestOrderCreatedEventHandler(ILogger<TestOrderCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TestOrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Trong thực tế sẽ publish vào RabbitMQ/Kafka để gửi cho Monitoring Service.
        // Ở version Intern này, ta sẽ in log ra Console để demo cho dễ giải thích.
        
        _logger.LogInformation(
            "EVENT LOGGED: [Action: Create TestOrder] [TestOrderID: {TestOrderId}] [Patient: {Patient}] [Operator: {Operator}] [Time: {Time}]",
            notification.TestOrderId,
            notification.PatientName,
            notification.CreatedBy,
            DateTime.UtcNow);

        return Task.CompletedTask;
    }
}
