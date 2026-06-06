namespace SistemaAcademico.Services.Interfaces;

public interface IPdfService
{
    byte[] GerarHistorico(string conteudo);
    byte[] GerarDeclaracao(string conteudo);
}
