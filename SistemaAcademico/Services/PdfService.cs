using QuestPDF.Fluent;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Services;

public class PdfService : IPdfService
{
    public byte[] GerarHistorico(string conteudo)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Content().Text(conteudo);
            });
        }).GeneratePdf();
    }

    public byte[] GerarDeclaracao(string conteudo)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Content().Text(conteudo);
            });
        }).GeneratePdf();
    }
}