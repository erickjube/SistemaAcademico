namespace SistemaAcademico.Models;

public class PeriodoLetivo
{
    public int Id { get; private set; }
    public string Semestre { get; private set; } = string.Empty; // 2025.1, 2025.2, etc.
    public DateTime InicioMatricula { get; private set; }
    public DateTime FimMatricula { get; private set; }
    public DateTime InicioAulas { get; private set; }
    public DateTime FimAulas { get; private set; }

    public PeriodoLetivo() { }
    public PeriodoLetivo(string semestre, DateTime inicioMatricula, DateTime fimMatricula, DateTime inicioAulas, DateTime fimAulas)
    {
        if (string.IsNullOrWhiteSpace(semestre)) throw new ArgumentException("Semestre é obrigatório.", nameof(semestre));
        if (inicioMatricula >= fimMatricula) throw new ArgumentException("Data de início da matrícula deve ser anterior à data de fim.");
        if (inicioAulas >= fimAulas) throw new ArgumentException("Data de início das aulas deve ser anterior à data de fim.");
        Semestre = semestre;
        InicioMatricula = inicioMatricula;
        FimMatricula = fimMatricula;
        InicioAulas = inicioAulas;
        FimAulas = fimAulas;
    }

    public void ValidarPeriodoMatricula()
    {
        var agora = DateTime.UtcNow;

        if (agora < InicioMatricula || agora > FimMatricula)
            throw new InvalidOperationException("O período de matrícula está fechado.");
    }
}
