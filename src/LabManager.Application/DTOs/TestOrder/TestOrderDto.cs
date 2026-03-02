using System;
using LabManager.Domain.Enums;

namespace LabManager.Application.DTOs.TestOrder;

public class TestOrderDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public TestOrderStatus Status { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? RawResults { get; set; }
    public string? ProcessedResults { get; set; }
}

public class CreateTestOrderDto
{
    public Guid PatientId { get; set; }
}

public class UpdateTestOrderResultDto
{
    public string RawResults { get; set; } = string.Empty;
    public string ProcessedResults { get; set; } = string.Empty;
}
