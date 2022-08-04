using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
         Task AtualizarEmail(Guid id, string emailAtualizado);
         Task AtualizarUrlImagemCadastro(Guid id, string urlAtualizada);
         Task<Usuario> BuscarPorEmail(string email);
    }
}