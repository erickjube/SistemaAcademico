namespace SistemaAcademico.Patterns.Strategy;

public class MediaPonderada : ICalculadoraMedia
{
    public decimal Calcular(decimal p1, decimal p2, decimal trabalho)
       => (p1 * 0.3m) + (p2 * 0.3m) + (trabalho * 0.4m);
}
