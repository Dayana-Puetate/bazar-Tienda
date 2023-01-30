using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webIntegradorSeptimo.Negocio
{
    public class detallePedidoAuxiliar
    {
        public int? idProducto { get; set; }

        public decimal? precio { get; set; }

        public int? cantidad { get; set; }

        public int? descuento { get; set; }

        public decimal? subtotal { get; set; }

        public decimal? costoEnvio { get; set; }

        public decimal? costoIva { get; set; }

        public decimal? valorTotal { get; set; }
    }
}