using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Services;

public class ArquivoService : IArquivoService
{
    private readonly IWebHostEnvironment _env;

    public ArquivoService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SalvarAsync(byte[] arquivo, string nomeArquivo)
    {
        var pasta = Path.Combine(_env.ContentRootPath, "Documentos");
        if (!Directory.Exists(pasta)) Directory.CreateDirectory(pasta);
        var caminho = Path.Combine(pasta, nomeArquivo);
        await File.WriteAllBytesAsync(caminho, arquivo);
        return caminho;
    }
}