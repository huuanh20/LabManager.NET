using LabManager.Domain.Enums;

namespace LabManager.Domain.Entities;

public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;
    public DateTime? LastMaintenanceDate { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    public int LabId { get; set; }

    public Lab? Lab { get; set; }
}
