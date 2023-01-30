namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("recuperaClave")]
    public partial class recuperaClave
    {
        [Key]
        public int idRecupera { get; set; }

        public DateTime? fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }

        [StringLength(500)]
        public string validacion { get; set; }

        public bool? activo { get; set; }

        public int? idCliente { get; set; }

        public virtual cliente cliente { get; set; }
    }
}
