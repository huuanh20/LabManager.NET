using System;
using System.Collections.Generic;
using LabManager.Domain.Common;
using LabManager.Domain.Enums;

namespace LabManager.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string IdentifyNumber { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Address { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsLocked { get; set; } = false;

    // Navigation properties
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
