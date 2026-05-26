using SistemaAcademico.ENUMs;

namespace SistemaAcademico.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;

    public PerfilUsuario Perfil { get; set; }
    
    public bool Ativo { get; set; } = true;
}
