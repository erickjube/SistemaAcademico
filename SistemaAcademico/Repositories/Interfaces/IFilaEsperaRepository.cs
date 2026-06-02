using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface IFilaEsperaRepository
{
    Task AdicionarFilaEsperaAsync(FilaEspera fila);
}
