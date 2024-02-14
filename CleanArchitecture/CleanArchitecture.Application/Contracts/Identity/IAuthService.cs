using CleanArchitecture.Application.Models.Identity;

namespace CleanArchitecture.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<AuthResponse> Register(RegistrationRequest request);
}
