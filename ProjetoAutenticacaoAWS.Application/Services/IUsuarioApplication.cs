using Microsoft.AspNetCore.Http;
using ProjetoAutenticacaoAWS.Application.DTOs;
using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Application.Services
{
    public interface IUsuarioApplication
    {
        Task<int> CriarUsuario(UsuarioDTO usuarioDTO);
        Task CadastrarImagem(int id, IFormFile imagem);
        Task<List<Usuario>> BuscarUsuarios();
        Task<Usuario> BuscarUsuarioPorID(int id);
        Task<int> LoginEmail(string email, string senha);
        Task LoginImagem(int id, IFormFile imagem);
        Task AtualizarEmailUsuarioPorId(int id, string email);
        Task DeletarUsuarioPorID(int id);
    }
}