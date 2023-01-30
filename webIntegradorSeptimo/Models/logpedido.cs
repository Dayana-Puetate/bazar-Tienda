namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("logpedido")]
    public partial class logpedido
    {
        [Key]
        public int idLog { get; set; }

        public int? idPedido { get; set; }

        public DateTime? fecha { get; set; }

        public int? idEstado { get; set; }

        public virtual estado estado { get; set; }

        public virtual pedido pedido { get; set; }
    }
}
