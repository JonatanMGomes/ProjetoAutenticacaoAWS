using Microsoft.AspNetCore.Mvc;
using ProjetoAutenticacaoAWS.Lib.Models;
using ProjetoAutenticacaoAWS.Web.DTOs;

namespace ProjetoAutenticacaoAWS.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly static List<Usuario> Usuarios = new List<Usuario>();

        [HttpPost()]
        public IActionResult CriarUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario(usuarioDTO.Id, usuarioDTO.Email, usuarioDTO.Cpf, usuarioDTO.DataNascimento,
                                      usuarioDTO.Nome, usuarioDTO.Senha, usuarioDTO.DataCriacao);
            Usuarios.Add(usuario);
            return Ok();
        }
        [HttpGet()]
        public IActionResult BuscarUsuarios()
        {
            return Ok(Usuarios);
        }
        [HttpGet("Id")]
        public IActionResult BuscarUsuarioPorID(int id)
        {
            return Ok(Usuarios.Find(x => x.Id == id));
        }
        [HttpPut()]
        public IActionResult AtualizarSenhaUsuarioPorId(int id, string senha)
        {
            var usuario = Usuarios.Find(x => x.Id == id);
            usuario.SetSenha(senha);
            return Ok();
        }
        [HttpDelete()]
        public IActionResult DeletarUsuarioPorID(int id)
        {
            var usuario = Usuarios.Find(x => x.Id == id);
            Usuarios.Remove(usuario);
            return Ok(usuario);
        }
    }
}