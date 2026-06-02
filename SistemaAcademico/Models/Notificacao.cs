namespace SistemaAcademico.Models;

public class Notificacao
{
    public int Id { get; private set; }
    public int UsuarioId { get; private set; }
    public Usuario? Usuario { get; private set; }

    public string Titulo { get; private set; } = string.Empty;
    public string Mensagem { get; private set; } = string.Empty;
    public bool Lida { get; private set; } = false;
    public DateTime CriadaEm { get; private set; } = DateTime.UtcNow;

    public Notificacao() { }
    public Notificacao(int usuarioId, string titulo, string mensagem)
    {
        if (usuarioId <= 0) throw new ArgumentException("UsuarioId deve ser um número positivo.", nameof(usuarioId));
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(mensagem)) throw new ArgumentException("Mensagem é obrigatória.", nameof(mensagem));
        UsuarioId = usuarioId;
        Titulo = titulo;
        Mensagem = mensagem;
    }
}
