namespace webIntegradorSeptimo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("reposicion")]
    public partial class reposicion
    {
        [Key]
        public int idReposicion { get; set; }

        public int? idProducto { get; set; }

        public decimal? cantidad { get; set; }

        public DateTime? fecha { get; set; }

        public virtual producto producto { get; set; }
    }
}
