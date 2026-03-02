using System.Threading.Tasks;
using LabManager.Application.DTOs.Auth;

namespace LabManager.Application.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
}
