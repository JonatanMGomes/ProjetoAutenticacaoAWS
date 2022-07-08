using Microsoft.EntityFrameworkCore;
using ProjetoAutenticacaoAWS.Lib.Models;

namespace ProjetoAutenticacaoAWS.Lib.Data
{
    public class AutenticacaoContext : DbContext
    {
        public AutenticacaoContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //mapeando tabelas

            //Usuario
            modelBuilder.Entity<Usuario>().ToTable("aws_usuarios");
            modelBuilder.Entity<Usuario>().HasKey(key => key.Id);
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}