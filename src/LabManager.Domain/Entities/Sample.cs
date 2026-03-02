using LabManager.Domain.Enums;

namespace LabManager.Domain.Entities;

public class Sample
{
    public int Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public SampleType Type { get; set; }
    public DateTime CollectedAt { get; set; }
    public string? Notes { get; set; }
    public int TestOrderId { get; set; }

    public TestOrder? TestOrder { get; set; }
}
