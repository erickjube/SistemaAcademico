using SistemaAcademico.DTOs.AuthDto;

namespace SistemaAcademico.Services.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(LoginDto dto);
}
