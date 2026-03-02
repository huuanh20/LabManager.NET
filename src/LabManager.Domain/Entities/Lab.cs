namespace LabManager.Domain.Entities;

public class Lab
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ContactEmail { get; set; } = string.Empty;
    public string? ContactPhone { get; set; }

    public ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
    public ICollection<TestOrder> TestOrders { get; set; } = new List<TestOrder>();
}
