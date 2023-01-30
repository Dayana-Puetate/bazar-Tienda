namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class imagenes_producto
    {
        [Key]
        public int idImagen { get; set; }

        [Column(TypeName = "text")]
        public string referencia { get; set; }

        public int? idProducto { get; set; }

        [StringLength(255)]
        public string size { get; set; }

        public virtual producto producto { get; set; }
    }
}
