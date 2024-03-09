﻿using System.ComponentModel.DataAnnotations;

namespace PARCIAL1B.Models
{
    public class Elementos
    {
        [Key]

        public int EmpresaID { get; set; }

        public string Elemento { get; set; }

        public int CantidadMinima { get; set; }

        public int? UnidadMedia { get; set; }

        public int? Costo { get; set; }

        public string Estado { get; set; }

    }
}
