using Microsoft.AspNetCore.Mvc;
using ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces;
using ProjetoAutenticacaoAWS.Lib.Models;
using ProjetoAutenticacaoAWS.Web.DTOs;

namespace ProjetoAutenticacaoAWS.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repositorio;
        public UsuarioController(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpPost()]
        public IActionResult CriarUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario(usuarioDTO.Id, usuarioDTO.Email, usuarioDTO.Cpf, usuarioDTO.DataNascimento,
                                      usuarioDTO.Nome, usuarioDTO.Senha, usuarioDTO.DataCriacao);
            _repositorio.Adicionar(usuario);
            return Ok();
        }
        [HttpGet()]
        public IActionResult BuscarUsuarios()
        {
            return Ok(_repositorio.BuscarTodos());
        }
        [HttpGet("Id")]
        public IActionResult BuscarUsuarioPorID(int id)
        {
            return Ok(_repositorio.BuscarPorId(id));
        }
        [HttpPut()]
        public IActionResult AtualizarEmailUsuarioPorId(int id, string email)
        {
            _repositorio.AtualizarEmail(id, email);
            return Ok();
        }
        [HttpDelete()]
        public IActionResult DeletarUsuarioPorID(int id)
        {
            _repositorio.DeletarItemDesejado(id);
            return Ok();
        }
    }
}