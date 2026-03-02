using LabManager.Domain.Enums;

namespace LabManager.Application.DTOs;

public class TestOrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderedAt { get; set; }
    public TestStatus Status { get; set; }
    public string TestName { get; set; } = string.Empty;
    public string? Result { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Notes { get; set; }
    public int PatientId { get; set; }
    public int LabId { get; set; }
}
