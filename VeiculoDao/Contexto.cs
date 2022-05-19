using Microsoft.EntityFrameworkCore;
using VeiculoModelsLibrary.Models;

namespace VeiculoDao
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        public DbSet<Veiculo> Veiculos { get; set; }

    }
}