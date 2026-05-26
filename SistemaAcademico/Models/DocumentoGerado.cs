using SistemaAcademico.ENUMs;

namespace SistemaAcademico.Models;

public class DocumentoGerado
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public TipoDocumento Tipo { get; set; }
    public string HashSha256 { get; set; } = string.Empty;
    public string CaminhoPdf { get; set; } = string.Empty;
    public DateTime GeradoEm { get; set; } = DateTime.UtcNow;
}
