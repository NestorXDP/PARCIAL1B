﻿using System.ComponentModel.DataAnnotations;

namespace PARCIAL1B.Models
{
    public class ElementosPorPlato
    {
        [Key]

        public int EmpresaId { get; set; }

        public int PlatoID { get; set; }

        public int ElementoID { get; set; }

        public decimal Cantidad { get; set; }

        public string Estado { get; set; }

       
    }
}
