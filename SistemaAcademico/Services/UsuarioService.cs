using SistemaAcademico.DTOs.UsuarioDto;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UsuarioResponseDto?> ObterPorEmailAsync(string email)
    {
        var usuario = await _usuarioRepository.ObterPorEmailAsync(email);
        if (usuario == null) throw new Exception("Usuário não encontrado.");
        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone,
            Cpf = usuario.Cpf,
            Perfil = usuario.Perfil.ToString(),
            Ativo = usuario.Ativo ? "Sim" : "Não"
        };
    }

    public async Task<UsuarioResponseDto?> ObterPorIdAsync(int id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuario == null) throw new Exception("Usuário não encontrado.");
        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone,
            Cpf = usuario.Cpf,
            Perfil = usuario.Perfil.ToString(),
            Ativo = usuario.Ativo ? "Sim" : "Não"
        };
    }

    public async Task<IEnumerable<UsuarioResponseDto?>> ObterTodos()
    {
        var usuarios = await _usuarioRepository.ObterTodosAsync();
        return usuarios.Select(u => new UsuarioResponseDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Telefone = u.Telefone,
            Cpf = u.Cpf,
            Perfil = u.Perfil.ToString(),
            Ativo = u.Ativo ? "Sim" : "Não"
        });
    }

    public async Task<IEnumerable<UsuarioResponseDto>> ObterProfessoresAsync()
    {
        var professores = await _usuarioRepository.ObterProfessoresAsync();
        return professores.Select(p => new UsuarioResponseDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Email = p.Email,
            Telefone = p.Telefone,
            Cpf = p.Cpf,
            Perfil = p.Perfil.ToString(),
            Ativo = p.Ativo ? "Sim" : "Não"
        });
    }

    public async Task<IEnumerable<UsuarioResponseDto>> ObterAlunosAsync()
    {
        var alunos = await _usuarioRepository.ObterAlunosAsync();
        return alunos.Select(a => new UsuarioResponseDto
        {
            Id = a.Id,
            Nome = a.Nome,
            Email = a.Email,
            Telefone = a.Telefone,
            Cpf = a.Cpf,
            Perfil = a.Perfil.ToString(),
            Ativo = a.Ativo ? "Sim" : "Não"
        });
    }

    public async Task<UsuarioResponseDto> AdicionarAsync(CriarUsuarioDto dto)
    {
        if (dto == null) throw new Exception("Dados do usuário são obrigatórios.");

        var emailExistente = await _usuarioRepository.ExisteEmailAsync(dto.Email);
        if (emailExistente) throw new Exception("Email já cadastrado.");

        var cpfExistente = await _usuarioRepository.ExisteCpfAsync(dto.Cpf);
        if (cpfExistente) throw new Exception("CPF já cadastrado.");

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        var usuario = new Usuario(dto.Nome, dto.Email, dto.Telefone, dto.Cpf, senhaHash, dto.Perfil);
        await _usuarioRepository.AdicionarAsync(usuario);
        await _unitOfWork.SalvarAsync();
        
        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone,
            Cpf = usuario.Cpf,
            Perfil = usuario.Perfil.ToString()
        };
    }

    public async Task AtualizarAsync(AtualizarUsuarioDto dto)
    {
        if (dto == null) throw new Exception("Dados do usuário são obrigatórios.");
        var usuario = await _usuarioRepository.ObterPorIdAsync(dto.Id);
        if (usuario == null) throw new Exception("Usuário não encontrado.");
        if (!usuario.Ativo) throw new InvalidOperationException("Usuário está desativado.");
        
        usuario.AtualizarDados(dto.Nome, dto.Telefone);

        if (!string.IsNullOrWhiteSpace(dto.SenhaNova))
        {
            var senhaCorreta = BCrypt.Net.BCrypt.Verify(dto.SenhaAntiga, usuario.SenhaHash);
            if (!senhaCorreta) throw new InvalidOperationException("Senha atual incorreta.");

            var novoHash = BCrypt.Net.BCrypt.HashPassword(dto.SenhaNova);
            usuario.AlterarSenha(novoHash);
        }

        await _unitOfWork.SalvarAsync();
    }

    public async Task  DesativarAsync(int id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuario == null) throw new Exception("Usuário não encontrado.");
        usuario.Desativar();
        await _unitOfWork.SalvarAsync();
    }
}
