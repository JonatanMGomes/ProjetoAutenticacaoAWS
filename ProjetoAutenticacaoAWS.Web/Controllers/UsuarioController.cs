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
        public ILogger<UsuarioController> _log { get; set; }
        public UsuarioController(IUsuarioApplication application, ILogger<UsuarioController> log)
        {
            _application = application;
            _log = log;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var resposta = await _application.CriarUsuario(usuarioDTO);
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("imagem")]
        public async Task<IActionResult> CadastrarImagem(int id, IFormFile imagem)
        {
            try
            {
                await _application.CadastrarImagem(id, imagem);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet()]
        public async Task<IActionResult> BuscarUsuarios()
        {
            try
            {
                var resposta = await _application.BuscarUsuarios();
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Id")]
        public async Task<IActionResult> BuscarUsuarioPorID(int id)
        {
            try
            {
                var resposta = await _application.BuscarUsuarioPorID(id);
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("LoginEmail")]
        public async Task<IActionResult> LoginEmail(string email, string senha)
        {
            try
            {
                var resposta = await _application.LoginEmail(email, senha);
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("LoginImagem")]
        public async Task<IActionResult> LoginImagem(int id, IFormFile imagem)
        {
            try
            {
                await _application.LoginImagem(id, imagem);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Email")]
        public async Task<IActionResult> AtualizarEmailUsuarioPorId(int id, string email)
        {
            try
            {
                await _application.AtualizarEmailUsuarioPorId(id, email);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete()]
        public async Task<IActionResult> DeletarUsuarioPorID(int id)
        {
            try
            {
                await _application.DeletarUsuarioPorID(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}