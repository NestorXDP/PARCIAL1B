using System.ComponentModel.DataAnnotations;

namespace PARCIAL1B.Models
{
    public class ElementosPorPlato
    {
        [Key]

        public int EmpresaId { get; set; }

        public int PlatoID { get; set; }

        public int ElementoID { get; set; }

        public int? Cantidad { get; set; }

        public int? Estado { get; set; }

       
    }
}
