namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("oferta")]
    public partial class oferta
    {
        [Key]
        public int idOferta { get; set; }

        public int? idProducto { get; set; }

        public int? porcentaje { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        public DateTime? fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }

        public bool? activo { get; set; }

        public virtual producto producto { get; set; }
    }
}
