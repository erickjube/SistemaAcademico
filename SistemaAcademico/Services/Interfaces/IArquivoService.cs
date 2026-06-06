namespace SistemaAcademico.Services.Interfaces;

public interface IArquivoService
{
    Task<string> SalvarAsync(byte[] arquivo, string nomeArquivo);
}
