using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentoController : ControllerBase
{
    private readonly IDocumentoService _documentoService;

    public DocumentoController(IDocumentoService documentoService)
    {
        _documentoService = documentoService;
    }

    [HttpGet("historico")]
    public async Task<ActionResult> GerarHistorico(int usuarioId)
    {
        var historico = await _documentoService.GerarHistoricoAsync(usuarioId);
        return File(historico, "application/pdf", $"historico_{usuarioId}.pdf");
    }

    [HttpGet("declaracao")]
    public async Task<ActionResult> GerarDeclaracao(int usuarioId)
    {
        var declaracao = await _documentoService.GerarDeclaracaoAsync(usuarioId);
        return File(declaracao, "application/pdf", $"declaracao_{usuarioId}.pdf");
    }

    [HttpGet("validar/{hash}")]
    public async Task<ActionResult> Validar(string hash)
    {
        var valido = await _documentoService.ValidarDocumentoAsync(hash);

        if (!valido)
            return NotFound(new
            {
                Mensagem = "Documento não encontrado."
            });

        return Ok(new
        {
            Mensagem = "Documento válido."
        });
    }
}
