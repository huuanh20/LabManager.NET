using System;
using System.Collections.Generic;
using LabManager.Domain.Common;
using LabManager.Domain.Enums;

namespace LabManager.Domain.Entities;

public class TestOrder : BaseEntity
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    
    public TestOrderStatus Status { get; set; } = TestOrderStatus.Pending;
    
    public DateTime? RunDate { get; set; }
    public Guid? RunByUserId { get; set; }
    public User? RunByUser { get; set; }

    public string? RawResults { get; set; }
    public string? ProcessedResults { get; set; }
    
    public ICollection<TestOrderComment> Comments { get; set; } = new List<TestOrderComment>();
}

public class TestOrderComment : BaseEntity
{
    public Guid TestOrderId { get; set; }
    public TestOrder TestOrder { get; set; } = null!;
    
    public string Content { get; set; } = string.Empty;
}
