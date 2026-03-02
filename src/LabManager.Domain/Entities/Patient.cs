using System;
using System.Collections.Generic;
using LabManager.Domain.Common;

namespace LabManager.Domain.Entities;

public class Patient : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<TestOrder> TestOrders { get; set; } = new List<TestOrder>();
}
