using ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces;
using ProjetoAutenticacaoAWS.Lib.Models;
using ProjetoAutenticacaoAWS.Lib.MyExceptions;
using ProjetoAutenticacaoAWS.Application.DTOs;
using Microsoft.AspNetCore.Http;
using ProjetoAutenticacaoAWS.Services;

namespace ProjetoAutenticacaoAWS.Application.Services
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly ServicesAWS _servicesAWS;
        public UsuarioApplication(IUsuarioRepositorio repositorio, ServicesAWS servicesAWS)
        {
            _repositorio = repositorio;
            _servicesAWS = servicesAWS;
        }

        public async Task<Guid> CriarUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario(usuarioDTO.Email, usuarioDTO.Cpf, usuarioDTO.DataNascimento,
                                  usuarioDTO.Nome, usuarioDTO.Senha, usuarioDTO.DataCriacao);
            await _repositorio.Adicionar(usuario);
            return usuario.Id;
        }
        public async Task CadastrarImagem(Guid id, IFormFile imagem)
        {
            var nomeArquivo = await _servicesAWS.SalvarNoS3(imagem);
            var imagemValida = await _servicesAWS.ValidarImagem(nomeArquivo);
            if (imagemValida)
            {
                await _repositorio.AtualizarUrlImagemCadastro(id, nomeArquivo);
            }
            else
            {
                await _servicesAWS.DeletarNoS3("imagens-aulas", nomeArquivo);
                throw new DadosInvalidosException("Imagem inválida!");
            }
        }
        public async Task<List<Usuario>> BuscarUsuarios()
        {
            return await _repositorio.BuscarTodos();
        }
        public async Task<Usuario> BuscarUsuarioPorID(Guid id)
        {
            return await _repositorio.BuscarPorId(id);
        }
        public async Task<Guid> LoginEmail(string email, string senha)
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
        public async Task LoginImagem(Guid id, IFormFile imagem)
        {
            var usuario = await _repositorio.BuscarPorId(id);
            var verificacao = await _servicesAWS.VerificarImagem(usuario.UrlImagemCadastro, imagem);
            if (verificacao == false)
            {
                throw new DadosInvalidosException("Face não compativel!");
            }
        }
        public async Task AtualizarEmailUsuarioPorId(Guid id, string email)
        {
            await _repositorio.AtualizarEmail(id, email);
        }
        public async Task DeletarUsuarioPorID(Guid id)
        {
            await _repositorio.DeletarItemDesejado(id);
        }
    }
}