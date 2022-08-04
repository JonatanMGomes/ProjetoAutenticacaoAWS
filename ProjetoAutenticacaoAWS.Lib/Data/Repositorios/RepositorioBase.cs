using Microsoft.EntityFrameworkCore;
using ProjetoAutenticacaoAWS.Lib.Data.Repositorios.Interfaces;
using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Lib.Data.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : ModelBase
    {
        protected readonly AutenticacaoContext _context;
        protected readonly DbSet<T> _dbSet;
        public RepositorioBase(AutenticacaoContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task Adicionar(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<T> BuscarPorId(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstAsync(x => x.Id == id);
        }

        public async Task<List<T>> BuscarTodos()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task DeletarItemDesejado(Guid id)
        {
            var itemARemover = await _dbSet.FindAsync(id);
            _dbSet.Remove(itemARemover);
            await _context.SaveChangesAsync();
        }
    }
}