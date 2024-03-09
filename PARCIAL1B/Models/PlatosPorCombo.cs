using System.ComponentModel.DataAnnotations;

namespace PARCIAL1B.Models
{
    public class PlatosPorCombo
    {
        [Key]

        public int EmpresaID { get; set; }

        public int ComboID { get; set; }

        public int PlatoID { get; set; }

        public int? Estado { get; set; }
    }
}
