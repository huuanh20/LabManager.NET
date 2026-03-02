using System;
using System.Linq;
using LabManager.Domain.Entities;
using LabManager.Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LabManager.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        // Kiểm tra xem đã có dữ liệu chưa
        if (context.Users.Any())
        {
            return;   // DB đã được seed
        }

        // Tạo Roles
        var adminRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = "Administrator",
            Code = "ADMIN",
            Description = "Quản trị viên hệ thống",
            Privileges = "All"
        };

        var labUserRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = "Lab User",
            Code = "LAB_USER",
            Description = "Nhân viên xét nghiệm",
            Privileges = "Read, Create, Update"
        };

        context.Roles.AddRange(adminRole, labUserRole);

        // Tạo Admin User
        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            FullName = "System Admin",
            Email = "admin@lab.com",
            PhoneNumber = "0123456789",
            IdentifyNumber = "123456789",
            Gender = "Male",
            Age = 30,
            Address = "HCM",
            DateOfBirth = new DateTime(1995, 1, 1).ToUniversalTime(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), // Mật khẩu mặc định
            RoleId = adminRole.Id
        };

        context.Users.Add(adminUser);

        context.SaveChanges();
    }
}
