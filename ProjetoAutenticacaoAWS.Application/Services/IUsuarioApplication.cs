using Microsoft.AspNetCore.Http;
using ProjetoAutenticacaoAWS.Application.DTOs;
using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Application.Services
{
    public interface IUsuarioApplication
    {
        Task<Guid> CriarUsuario(UsuarioDTO usuarioDTO);
        Task CadastrarImagem(Guid id, IFormFile imagem);
        Task<List<Usuario>> BuscarUsuarios();
        Task<Usuario> BuscarUsuarioPorID(Guid id);
        Task<Guid> LoginEmail(string email, string senha);
        Task LoginImagem(Guid id, IFormFile imagem);
        Task AtualizarEmailUsuarioPorId(Guid id, string email);
        Task DeletarUsuarioPorID(Guid id);
    }
}