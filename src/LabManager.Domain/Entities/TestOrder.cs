using LabManager.Domain.Enums;

namespace LabManager.Domain.Entities;

public class TestOrder
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderedAt { get; set; } = DateTime.UtcNow;
    public TestStatus Status { get; set; } = TestStatus.Pending;
    public string TestName { get; set; } = string.Empty;
    public string? Result { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Notes { get; set; }
    public int PatientId { get; set; }
    public int LabId { get; set; }

    public Patient? Patient { get; set; }
    public Lab? Lab { get; set; }
    public ICollection<Sample> Samples { get; set; } = new List<Sample>();
}
