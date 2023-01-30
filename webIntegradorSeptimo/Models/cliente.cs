namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cliente")]
    public partial class cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cliente()
        {
            pedido = new HashSet<pedido>();
            recuperaClave = new HashSet<recuperaClave>();
        }

        [Key]
        public int idCliente { get; set; }

        public int? idCobertura { get; set; }

        [StringLength(150)]
        public string nombre { get; set; }

        [StringLength(150)]
        public string apellido { get; set; }

        [StringLength(10)]
        public string celular { get; set; }

        [StringLength(500)]
        public string direccion { get; set; }

        [StringLength(100)]
        public string correo { get; set; }

        [StringLength(100)]
        public string contra { get; set; }

        public bool? activo { get; set; }

        public DateTime? fecha_registro { get; set; }

        public virtual cobertura cobertura { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido> pedido { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<recuperaClave> recuperaClave { get; set; }
    }
}
