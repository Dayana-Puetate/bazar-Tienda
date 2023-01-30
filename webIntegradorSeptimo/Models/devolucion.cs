namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("devolucion")]
    public partial class devolucion
    {
        [Key]
        public int idDevolucion { get; set; }

        public DateTime? fecha { get; set; }

        public int? idPedido { get; set; }

        public virtual pedido pedido { get; set; }
    }
}
