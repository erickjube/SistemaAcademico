using SistemaAcademico.ENUMs;

namespace SistemaAcademico.DTOs.UsuarioDto;

public class CriarUsuarioDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    public  string Senha { get; set; }
    public PerfilUsuario Perfil { get; set; }
}
