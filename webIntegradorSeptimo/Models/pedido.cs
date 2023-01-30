namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pedido")]
    public partial class pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pedido()
        {
            detallePedido = new HashSet<detallePedido>();
            devolucion = new HashSet<devolucion>();
            logpedido = new HashSet<logpedido>();
        }

        [Key]
        public int idPedido { get; set; }

        public DateTime? fecha_pedido { get; set; }

        public int? idEstado { get; set; }

        public int? idCliente { get; set; }

        [StringLength(500)]
        public string comentario { get; set; }

        public decimal? total { get; set; }

        public decimal? subtotal { get; set; }

        public decimal? iva { get; set; }

        public decimal? envio { get; set; }

        public virtual cliente cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detallePedido> detallePedido { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<devolucion> devolucion { get; set; }

        public virtual estado estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<logpedido> logpedido { get; set; }
    }
}
