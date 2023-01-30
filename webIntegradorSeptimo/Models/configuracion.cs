namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("configuracion")]
    public partial class configuracion
    {
        [Key]
        public int idConfiguracion { get; set; }

        [Column(TypeName = "text")]
        public string about { get; set; }

        [StringLength(15)]
        public string telefono { get; set; }

        public DateTime? fecha_registro { get; set; }

        [StringLength(250)]
        public string direccion { get; set; }
    }
}
