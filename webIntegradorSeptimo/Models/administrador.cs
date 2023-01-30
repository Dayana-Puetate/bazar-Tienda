namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("administrador")]
    public partial class administrador
    {
        [Key]
        public int idAdministrador { get; set; }

        [StringLength(13)]
        public string cedula { get; set; }

        [StringLength(250)]
        public string nombre { get; set; }

        [StringLength(250)]
        public string apellido { get; set; }

        [StringLength(10)]
        public string telefono { get; set; }

        [StringLength(250)]
        public string mail { get; set; }

        [StringLength(100)]
        public string usuario { get; set; }

        [StringLength(100)]
        public string contra { get; set; }

        public bool? activo { get; set; }

        public DateTime? fecha_registro { get; set; }
    }
}
