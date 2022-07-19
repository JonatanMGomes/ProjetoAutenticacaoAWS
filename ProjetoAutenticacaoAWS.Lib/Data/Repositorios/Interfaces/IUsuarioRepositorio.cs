using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
         Task AtualizarEmail(int id, string emailAtualizado);
         Task AtualizarUrlImagemCadastro(int id, string urlAtualizada);
    }
}