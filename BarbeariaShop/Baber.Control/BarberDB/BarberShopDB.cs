using Baber.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace Baber.Control.BarberDB
{
    public class BarberShopDB:DbContext
    {
        public DbSet<Faturamento> faturamento { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = "Server=localhost;DataBase=barbeariashop;Uid=root;Pwd=jenny210702@";
            var versao = new Version(8, 0, 41);
            var ServerVersion = new MySqlServerVersion(versao);
            optionsBuilder.UseMySql(connectionString, ServerVersion);
        }
    }
}
