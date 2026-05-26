namespace SistemaAcademico.Models;

public class Notificacao
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public string Titulo { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public bool Lida { get; set; } = false;
    public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
}
