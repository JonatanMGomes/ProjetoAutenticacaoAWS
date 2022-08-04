using Microsoft.EntityFrameworkCore;
using ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces;
using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Lib.Data.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(AutenticacaoContext context) : base(context, context.Usuarios)
        {

        }
        public async Task AtualizarEmail(Guid id, string emailAtualizado)
        {
            _dbSet.Find(id).SetEmail(emailAtualizado);
            await _context.SaveChangesAsync();
        }
        public async Task AtualizarUrlImagemCadastro(Guid id, string urlAtualizada)
        {
            _dbSet.Find(id).SetUrlImagemCadastro(urlAtualizada);
            await _context.SaveChangesAsync();
        }
        public async Task<Usuario> BuscarPorEmail(string email)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}