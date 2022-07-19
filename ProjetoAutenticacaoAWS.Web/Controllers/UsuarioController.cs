using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces;
using ProjetoAutenticacaoAWS.Lib.Models;
using ProjetoAutenticacaoAWS.Lib.MyExceptions;
using ProjetoAutenticacaoAWS.Web.DTOs;

namespace ProjetoAutenticacaoAWS.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IAmazonS3 _amazonS3;
        private static readonly List<string> _extensoesImagem =
        new List<string>() { "image/jpeg", "image/png" };
        private readonly AmazonRekognitionClient _rekognitionClient;
        public UsuarioController(IUsuarioRepositorio repositorio, IAmazonS3 amazonS3, AmazonRekognitionClient rekognitionClient)
        {
            _repositorio = repositorio;
            _amazonS3 = amazonS3;
            _rekognitionClient = rekognitionClient;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuario = new Usuario(usuarioDTO.Id, usuarioDTO.Email, usuarioDTO.Cpf, usuarioDTO.DataNascimento,
                                      usuarioDTO.Nome, usuarioDTO.Senha, usuarioDTO.DataCriacao);
                await _repositorio.Adicionar(usuario);
                return Ok();
            }
            catch (DadosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("imagem")]
        public async Task<IActionResult> CadastrarImagem(int id, IFormFile imagem)
        {
            var nomeArquivo = await SalvarNoS3(imagem);
            var imagemValida = await ValidarImagem(nomeArquivo);
            if (imagemValida)
            {
                await _repositorio.AtualizarUrlImagemCadastro(id, nomeArquivo);
                return Ok();
            }
            else
            {
                var response = await _amazonS3.DeleteObjectAsync("imagens-aulas", nomeArquivo);
                return BadRequest("Imagem inválida!");
            }
        }
        private async Task<string> SalvarNoS3(IFormFile imagem)
        {
            if (!_extensoesImagem.Contains(imagem.ContentType))
            {
                throw new Exception("tipo inválido!");
            }
            using (var streamDaImagem = new MemoryStream())
            {
                await imagem.CopyToAsync(streamDaImagem);
                var request = new PutObjectRequest();
                request.Key = "recFacial " + imagem.FileName;
                request.BucketName = "imagens-aulas";
                request.InputStream = streamDaImagem;

                var resposta = await _amazonS3.PutObjectAsync(request);
                return request.Key;
            }
        }
        private async Task<bool> ValidarImagem(string nomeArquivo)
        {
            var request = new DetectFacesRequest();
            var imagem = new Image();

            var s3Object = new Amazon.Rekognition.Model.S3Object()
            {
                Bucket = "imagens-aulas",
                Name = nomeArquivo

            };

            imagem.S3Object = s3Object;
            request.Image = imagem;
            request.Attributes = new List<string>() { "ALL" };

            var response = await _rekognitionClient.DetectFacesAsync(request);

            if (response.FaceDetails.Count == 1 && response.FaceDetails.First().Eyeglasses.Value == false)
            {
                return true;
            }
            return false;
        }
        [HttpGet()]
        public async Task<IActionResult> BuscarUsuarios()
        {
            try
            {
                return Ok(await _repositorio.BuscarTodos());
            }
            catch (DadosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Id")]
        public async Task<IActionResult> BuscarUsuarioPorID(int id)
        {
            try
            {
                return Ok(await _repositorio.BuscarPorId(id));
            }
            catch (DadosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("LoginEmail")]
        public async Task<IActionResult> LoginEmail(string email, string senha)
        {
            var usuario = await _repositorio.BuscarPorEmail(email);
            var validacao = await VerificarSenha(usuario, senha);
            if (validacao)
            {
                return Ok(usuario.Id);
            }
            return BadRequest("Senha incorreta!");
        }
        private async Task<bool> VerificarSenha(Usuario usuario, string senha)
        {
            if (usuario.Senha == senha)
            {
                return true;
            }
            return false;
        }
        [HttpPut()]
        public async Task<IActionResult> AtualizarEmailUsuarioPorId(int id, string email)
        {
            try
            {
                await _repositorio.AtualizarEmail(id, email);
                return Ok();
            }
            catch (DadosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete()]
        public async Task<IActionResult> DeletarUsuarioPorID(int id)
        {
            try
            {
                await _repositorio.DeletarItemDesejado(id);
                return Ok();
            }
            catch (DadosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}