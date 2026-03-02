using System;

namespace LabManager.Desktop.Models;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class PatientModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class CreatePatientModel
{
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    public string Gender { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class TestOrderModel
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int Status { get; set; } // Map từ enum
    public DateTime CreatedOn { get; set; }
    public string? RawResults { get; set; }
    public string? ProcessedResults { get; set; }
}

public class CreateTestOrderModel
{
    public Guid PatientId { get; set; }
}
