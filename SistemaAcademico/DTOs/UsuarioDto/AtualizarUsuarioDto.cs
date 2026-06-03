namespace SistemaAcademico.DTOs.UsuarioDto;

public class AtualizarUsuarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string SenhaAntiga { get; set; }
    public string SenhaNova { get; set; }
}
