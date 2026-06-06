using SistemaAcademico.ENUMs;
using SistemaAcademico.Helpers;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services.Interfaces;
using System.Text;
using System.Security.Cryptography;

namespace SistemaAcademico.Services;

public class DocumentoService : IDocumentoService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IArquivoService _arquivoService;
    private readonly IPdfService _pdfService;

    public DocumentoService(IUsuarioRepository usuarioRepository, 
                            IDocumentoRepository documentoRepository, 
                            IUnitOfWork unitOfWork,
                            IArquivoService arquivoService,
                            IPdfService pdfService)
    {
        _usuarioRepository = usuarioRepository;
        _documentoRepository = documentoRepository;
        _unitOfWork = unitOfWork;
        _arquivoService = arquivoService;
        _pdfService = pdfService;
    }

    public async Task<byte[]> GerarHistoricoAsync(int usuarioId)
    {
        var aluno = await _usuarioRepository.ObterPorIdAsync(usuarioId);
        if (aluno is null) throw new Exception("Aluno não encontrado.");

        var conteudo = MontarConteudoHistorico(aluno);
        var pdf = _pdfService.GerarHistorico(conteudo);
        var hash = GerarHash(pdf);
        var caminho = await _arquivoService.SalvarAsync(pdf, $"historico_{usuarioId}.pdf");
        var documento = new DocumentoGerado(usuarioId, TipoDocumento.Historico, hash, caminho);

        await _documentoRepository.AddAsync(documento);
        await _unitOfWork.SalvarAsync();
        return pdf;
    }

    public async Task<byte[]> GerarDeclaracaoAsync(int usuarioId)
    {
        var aluno = await _usuarioRepository.ObterPorIdAsync(usuarioId);
        if (aluno is null) throw new Exception("Aluno não encontrado.");

        var conteudo = MontarConteudoDeclaracao(aluno);
        var pdf = _pdfService.GerarDeclaracao(conteudo);
        var hash = GerarHash(pdf);
        var caminho = await _arquivoService.SalvarAsync(pdf, $"declaracao_{usuarioId}.pdf");
        var documento = new DocumentoGerado(usuarioId, TipoDocumento.DeclaracaoMatricula, hash, caminho);

        await _documentoRepository.AddAsync(documento);
        await _unitOfWork.SalvarAsync();
        return pdf;
    }

    public async Task<bool> ValidarDocumentoAsync(string hash)
    {
        var documento = await _documentoRepository.ObterPorHashAsync(hash);
        return documento != null;
    }




    private string MontarConteudoHistorico(Usuario aluno)
    {
        var sb = new StringBuilder();

        sb.AppendLine("HISTÓRICO ESCOLAR");
        sb.AppendLine($"Aluno: {aluno.Nome}");
        sb.AppendLine($"Email: {aluno.Email}");
        sb.AppendLine();

        foreach (var matricula in aluno.MatriculasAluno)
        {
            sb.AppendLine($"Disciplina: {matricula.Turma?.Disciplina?.Nome}");

            if (matricula.Nota != null)
            {
                sb.AppendLine($"Média: {matricula.Nota.Media}");
                sb.AppendLine($"Frequência: {matricula.Nota.Frequencia}");
                sb.AppendLine($"Situação: {matricula.Nota.Situacao}");
            }

            sb.AppendLine("--------------------------------");
        }

        return sb.ToString();
    }

    private string GerarHash(byte[] arquivo)
    {
        using var sha256 = SHA256.Create();

        var hashBytes = sha256.ComputeHash(arquivo);

        return Convert.ToHexString(hashBytes);
    }

    private string MontarConteudoDeclaracao(Usuario aluno)
    {
        return $"""
        DECLARAÇÃO DE MATRÍCULA

        Declaramos para os devidos fins que o aluno
        {aluno.Nome},
        inscrito sob o e-mail {aluno.Email},
        encontra-se regularmente matriculado nesta instituição.

        Emitido em: {DateTime.Now:dd/MM/yyyy}
        """;
    }
}
