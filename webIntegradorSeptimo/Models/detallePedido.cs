namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("detallePedido")]
    public partial class detallePedido
    {
        [Key]
        public int idDetalle { get; set; }

        public int? idProducto { get; set; }

        public decimal? precio { get; set; }

        public int? cantidad { get; set; }

        public int? descuento { get; set; }

        public int? idPedido { get; set; }

        public decimal? subtotal { get; set; }

        public decimal? envio { get; set; }

        public decimal? iva { get; set; }

        public decimal? total { get; set; }

        public virtual producto producto { get; set; }

        public virtual pedido pedido { get; set; }
    }
}
