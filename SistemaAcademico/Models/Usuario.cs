using SistemaAcademico.ENUMs;

namespace SistemaAcademico.Models;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Telefone { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public string SenhaHash { get; private set; } = string.Empty;
    public PerfilUsuario Perfil { get; private set; }
    
    public bool Ativo { get; private set; } = true;

    public ICollection<Turma> TurmasProfessor { get; private set; } = new List<Turma>();
    public ICollection<Matricula> MatriculasAluno { get; private set; } = new List<Matricula>();

    public Usuario() { }
    public Usuario(string nome, string email, string telefone, string cpf, string senhaHash, PerfilUsuario perfil)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email é obrigatório.", nameof(email));
        if (string.IsNullOrWhiteSpace(telefone)) throw new ArgumentException("Telefone é obrigatório.", nameof(telefone));
        if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentException("CPF é obrigatório.", nameof(cpf));
        if (string.IsNullOrWhiteSpace(senhaHash)) throw new ArgumentException("SenhaHash é obrigatório.", nameof(senhaHash));
        
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Cpf = cpf;
        SenhaHash = senhaHash;
        Perfil = perfil;
        Ativo = true;
    }

    public void AtualizarDados(string nome, string telefone)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(telefone)) throw new ArgumentException("Telefone é obrigatório.", nameof(telefone));
        Nome = nome;
        Telefone = telefone;
    }

    public void AlterarSenha(string novaSenhaHash)
    {
        if (string.IsNullOrWhiteSpace(novaSenhaHash)) throw new ArgumentException("SenhaHash é obrigatório.", nameof(novaSenhaHash));
        SenhaHash = novaSenhaHash;
    }

    public void Desativar()
    {
        if (!Ativo) throw new InvalidOperationException("Usuário já está desativado.");
        Ativo = false;
    }
}
