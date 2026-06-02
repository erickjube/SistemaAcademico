using SistemaAcademico.ENUMs;

namespace SistemaAcademico.Models;

public class DocumentoGerado
{
    public int Id { get; private set; }

    public int UsuarioId { get; private set; }
    public Usuario? Usuario { get; private set; }

    public TipoDocumento Tipo { get; private set; }
    public string HashSha256 { get; private set; } = string.Empty;
    public string CaminhoPdf { get; private set; } = string.Empty;
    public DateTime GeradoEm { get; private set; } = DateTime.UtcNow;

    public DocumentoGerado() { }
    public DocumentoGerado(int usuarioId, TipoDocumento tipo, string hashSha256, string caminhoPdf)
    {
        if (usuarioId <= 0) throw new ArgumentException("UsuarioId deve ser um número positivo.", nameof(UsuarioId));
        if (string.IsNullOrWhiteSpace(hashSha256)) throw new ArgumentException("HashSha256 é obrigatório.", nameof(hashSha256));
        if (string.IsNullOrWhiteSpace(caminhoPdf)) throw new ArgumentException("CaminhoPdf é obrigatório.", nameof(caminhoPdf));
        UsuarioId = usuarioId;
        Tipo = tipo;
        HashSha256 = hashSha256;
        CaminhoPdf = caminhoPdf;
    }
}
