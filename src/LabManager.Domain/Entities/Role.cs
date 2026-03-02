using System;
using System.Collections.Generic;
using LabManager.Domain.Common;

namespace LabManager.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Privileges { get; set; } = string.Empty;

    // Navigation property
    public ICollection<User> Users { get; set; } = new List<User>();
}
