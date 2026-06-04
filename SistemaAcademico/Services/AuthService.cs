namespace SistemaAcademico.Services;

using SistemaAcademico.DTOs.AuthDto;
using SistemaAcademico.Services.Interfaces;
using System.Threading.Tasks;

public class AuthService : IAuthService
{
    public Task<string> LoginAsync(LoginDto dto)
    {
        throw new NotImplementedException();
    }
}
