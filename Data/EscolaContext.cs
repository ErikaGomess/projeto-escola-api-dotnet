using Microsoft.EntityFrameworkCore;
using ProjetoEscola_API.Models;

namespace ProjetoEscola_MySQL.Data
{
        public class EscolaContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public EscolaContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(Configuration.GetConnectionString("StringConexaoMySQL"),
            ServerVersion.AutoDetect(Configuration.GetConnectionString("StringConexaoMySQL")));
        }
        public DbSet<Aluno>? Aluno { get; set; }
        public DbSet<User>? Usuario { get; set; }
        public DbSet<Curso>? Curso { get; set; }
    }
}