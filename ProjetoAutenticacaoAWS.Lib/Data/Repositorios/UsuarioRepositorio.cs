using ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces;
using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Lib.Data.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(AutenticacaoContext context) : base(context, context.Usuarios)
        {

        }
        public void AtualizarEmail(int id, string emailAtualizado)
        {
            _dbSet.Find(id).SetEmail(emailAtualizado);
            _context.SaveChanges();
        }
    }
}