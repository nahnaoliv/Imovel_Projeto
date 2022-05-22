using ImovelAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ImovelAPI.Context
{
    public class Contexto : DbContext
    {
        public IConfiguration Configuration { get; }
        public DbSet<Imovel> Imovel { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Configuration.GetConnectionString("Default"));
        }

        public Contexto(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
