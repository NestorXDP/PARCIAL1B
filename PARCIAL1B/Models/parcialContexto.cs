using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace PARCIAL1B.Models
{
    public class parcialContexto : DbContext
    {

            public parcialContexto(DbContextOptions<parcialContexto> options) : base(options)
            {
            }


            public DbSet<Platos> Platos { get; set; }
            public DbSet<Elementos> Elementos { get; set; }
            public DbSet<ElementosPorPlato> ElementosPorPlato { get; set; }
            public DbSet<PlatosPorCombo> PlatosPorCombo { get; set; }
        }

     }

