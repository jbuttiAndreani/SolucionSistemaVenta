using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Talle
    {
        public Talle()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdTalle { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
