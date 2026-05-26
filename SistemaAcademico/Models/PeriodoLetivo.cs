namespace SistemaAcademico.Models;

public class PeriodoLetivo
{
    public int Id { get; set; }
    public string Semestre { get; set; } = string.Empty; // 2025.1, 2025.2, etc.
    public DateTime InicioMatricula { get; set; }
    public DateTime FimMatricula { get; set; }
    public DateTime InicioAulas { get; set; }
}
