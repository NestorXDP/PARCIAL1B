using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace PARCIAL1B.Models
{
    public class parcialContexto : DbContext
    {
        public class parcialContexto : DbContext
        {
            public parcialContexto(DbContextOptions<parcialContexto> options) : base(options)
            {
            }

            public DbSet<PARCIAL1B> PARCIAL1B { get; set; }
        }
}
