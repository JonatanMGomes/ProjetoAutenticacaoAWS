using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces;
using ProjetoAutenticacaoAWS.Lib.Models;
using ProjetoAutenticacaoAWS.Lib.MyExceptions;
using ProjetoAutenticacaoAWS.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace ProjetoAutenticacaoAWS.Application.Services
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IAmazonS3 _amazonS3;
        private static readonly List<string> _extensoesImagem =
        new List<string>() { "image/jpeg", "image/png" };
        private readonly AmazonRekognitionClient _rekognitionClient;
        public UsuarioApplication(IUsuarioRepositorio repositorio, IAmazonS3 amazonS3, AmazonRekognitionClient rekognitionClient)
        {
            _repositorio = repositorio;
            _amazonS3 = amazonS3;
            _rekognitionClient = rekognitionClient;
        }

        public async Task<int> CriarUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario(usuarioDTO.Id, usuarioDTO.Email, usuarioDTO.Cpf, usuarioDTO.DataNascimento,
                                  usuarioDTO.Nome, usuarioDTO.Senha, usuarioDTO.DataCriacao);
            await _repositorio.Adicionar(usuario);
            return usuario.Id;
        }
        public async Task CadastrarImagem(int id, IFormFile imagem)
        {
            var nomeArquivo = await SalvarNoS3(imagem);
            var imagemValida = await ValidarImagem(nomeArquivo);
            if (imagemValida)
            {
                await _repositorio.AtualizarUrlImagemCadastro(id, nomeArquivo);
            }
            else
            {
                var response = await _amazonS3.DeleteObjectAsync("imagens-aulas", nomeArquivo);
                throw new DadosInvalidosException("Imagem inválida!");
            }
        }
        private async Task<string> SalvarNoS3(IFormFile imagem)
        {
            if (!_extensoesImagem.Contains(imagem.ContentType))
            {
                throw new TipoArquivoInvalidoException("Tipo de arquivo inválido!");
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
        public async Task<List<Usuario>> BuscarUsuarios()
        {
            return await _repositorio.BuscarTodos();
        }
        public async Task<Usuario> BuscarUsuarioPorID(int id)
        {
            return await _repositorio.BuscarPorId(id);
        }
        public async Task<int> LoginEmail(string email, string senha)
        {
            var usuario = await _repositorio.BuscarPorEmail(email);
            var validacao = await VerificarSenha(usuario, senha);
            if (validacao)
            {
                return usuario.Id;
            }
            throw new DadosInvalidosException("Senha incorreta!");
        }
        private async Task<bool> VerificarSenha(Usuario usuario, string senha)
        {
            return usuario.Senha == senha;
        }
        public async Task LoginImagem(int id, IFormFile imagem)
        {
            var usuario = await _repositorio.BuscarPorId(id);
            var verificacao = await VerificarImagem(usuario.UrlImagemCadastro, imagem);
            if (verificacao == false)
            {
                throw new DadosInvalidosException("Face não compativel!");
            }
        }
        private async Task<bool> VerificarImagem(string nomeArquivoS3, IFormFile fotoLogin)
        {
            using (var memStream = new MemoryStream())
            {
                var request = new CompareFacesRequest();

                var requestSource = new Image()
                {
                    S3Object = new Amazon.Rekognition.Model.S3Object()
                    {
                        Bucket = "imagens-aulas",
                        Name = nomeArquivoS3
                    }
                };

                await fotoLogin.CopyToAsync(memStream);
                var requestTarget = new Image()
                {
                    Bytes = memStream
                };

                request.SourceImage = requestSource;
                request.TargetImage = requestTarget;

                var response = await _rekognitionClient.CompareFacesAsync(request);
                if (response.FaceMatches.Count == 1 && response.FaceMatches.First().Similarity >= 90)
                {
                    return true;
                }
                return false;
            }
        }
        public async Task AtualizarEmailUsuarioPorId(int id, string email)
        {
            await _repositorio.AtualizarEmail(id, email);
        }
        public async Task DeletarUsuarioPorID(int id)
        {
            await _repositorio.DeletarItemDesejado(id);
        }
    }
}