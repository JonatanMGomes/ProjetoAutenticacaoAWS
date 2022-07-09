using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces
{
    public interface IRepositorioBase<T> where T : ModelBase
    {
         void Adicionar(T item);
         T BuscarPorId(int id);
         List<T> BuscarTodos();
         void DeletarItemDesejado(int id);
    }
}