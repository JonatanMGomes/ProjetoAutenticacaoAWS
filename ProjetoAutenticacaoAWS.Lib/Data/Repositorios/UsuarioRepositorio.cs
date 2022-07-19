using ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces;
using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Lib.Data.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(AutenticacaoContext context) : base(context, context.Usuarios)
        {

        }
        public async Task AtualizarEmail(int id, string emailAtualizado)
        {
            _dbSet.Find(id).SetEmail(emailAtualizado);
            await _context.SaveChangesAsync();
        }
        public async Task AtualizarUrlImagemCadastro(int id, string urlAtualizada)
        {
            _dbSet.Find(id).SetUrlImagemCadastro(urlAtualizada);
            await _context.SaveChangesAsync();
        }
    }
}