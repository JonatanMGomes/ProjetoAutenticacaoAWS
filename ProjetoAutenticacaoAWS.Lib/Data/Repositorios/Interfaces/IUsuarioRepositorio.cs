using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
         void AtualizarEmail(int id, string emailAtualizado);
    }
}