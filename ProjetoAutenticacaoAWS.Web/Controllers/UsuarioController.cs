using Microsoft.AspNetCore.Mvc;
using ProjetoAutenticacaoAWS.Application.DTOs;
using ProjetoAutenticacaoAWS.Application.Services;

namespace ProjetoAutenticacaoAWS.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplication _application;
        public UsuarioController(IUsuarioApplication application, ILogger<UsuarioController> log)
        {
            _application = application;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarUsuario(UsuarioDTO usuarioDTO)
        {
            var resposta = await _application.CriarUsuario(usuarioDTO);
            return Ok(resposta);
        }
        [HttpPost("imagem")]
        public async Task<IActionResult> CadastrarImagem(int id, IFormFile imagem)
        {
            await _application.CadastrarImagem(id, imagem);
            return Ok();
        }
        [HttpGet()]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var resposta = await _application.BuscarUsuarios();
            return Ok(resposta);
        }
        [HttpGet("Id")]
        public async Task<IActionResult> BuscarUsuarioPorID(int id)
        {
            var resposta = await _application.BuscarUsuarioPorID(id);
            return Ok(resposta);
        }
        [HttpGet("LoginEmail")]
        public async Task<IActionResult> LoginEmail(string email, string senha)
        {
            var resposta = await _application.LoginEmail(email, senha);
            return Ok(resposta);
        }
        [HttpPost("LoginImagem")]
        public async Task<IActionResult> LoginImagem(int id, IFormFile imagem)
        {
            await _application.LoginImagem(id, imagem);
            return Ok();
        }
        [HttpPut("Email")]
        public async Task<IActionResult> AtualizarEmailUsuarioPorId(int id, string email)
        {
            await _application.AtualizarEmailUsuarioPorId(id, email);
            return Ok();
        }
        [HttpDelete()]
        public async Task<IActionResult> DeletarUsuarioPorID(int id)
        {
            await _application.DeletarUsuarioPorID(id);
            return Ok();
        }
    }
}