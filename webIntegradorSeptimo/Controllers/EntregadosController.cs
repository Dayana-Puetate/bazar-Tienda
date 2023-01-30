using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;

namespace webIntegradorSeptimo.Controllers
{
    public class EntregadosController : Controller
    {
        // GET: Entregados
        private context_base db = new context_base();
        public ActionResult Index()
        {
            if (verificarSesion())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Administrador");
            }

        }

        public ActionResult Pedido()
        {
            if (verificarSesion())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Administrador");
            }

        }

        private bool verificarSesion()
        {
            bool res = false;
            foreach (var item in Session)
            {
                if (item.ToString().StartsWith("admin"))
                {
                    res = true;
                }
            }
            return res;

        }


        public string listaPedidos()
        {
            string html = "";

            try
            {
                var listaPedidos = db.pedido.Where(x => x.idEstado == 4).ToList();

                foreach (var item in listaPedidos)
                {
                    var estado = db.logpedido.Where(x => x.idPedido == item.idPedido).OrderByDescending(x => x.idLog).FirstOrDefault();
                    html += "<tr>" +
                           "<td valing='middle' class='text-center'>" + item.idPedido + "</td>" +
                           "<td valing='middle' class='text-cetner'>" + item.fecha_pedido + "</td>" +
                           "<td valing='middle' class='text-cetner'>" + estado.estado.estado1 + "</td>" +
                           "<td valing='middle' class='text-cetner'>" + estado.fecha + "</td>" +
                           "<td valing='middle' class='text-right'>" + item.total + "</td>" +
                           "<td valing='middle' class='text-center'><button class='btn btn-theme btn-sm' onclick='verPedido(" + item.idPedido + ")'><i class='fa fa-eye'></i></button></td>" +
                           "</tr>";
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return html;

        }

        public string unPedidoDetalle(int? id)
        {
            string html = "";

            try
            {

                if (id == null || id == 0)
                {
                    return "";
                }

                var lista = db.detallePedido.Where(x => x.idPedido == id).ToList();

                foreach (var item in lista)
                {

                    string src = db.imagenes_producto.Where(x => x.idProducto == item.idProducto).Select(x => x.referencia).FirstOrDefault();

                    html += "<tr>" +
                    "<td style='vertical-align:middle !important;' class='product-thumbnail text-center'>" +
                    "<img src='../../Imagenes/" + src + "' class='rounded-circle img-fluid shadow' style='width:50px;height:50px' alt='Image' >" +
                    "</td>" +
                    "<td style='vertical-align:middle !important;' class='product-name' style='max-width:290px'>" +
                    "<h5 class='h5 text-black'>" + item.producto.nombre + "</h5>" +
                    "</td>" +
                    "<td style='vertical-align:middle !important;' class='text-right'>" +
                    item.descuento + "%" +
                    "</td>" +
                    "<td style='vertical-align:middle !important;' class='text-right'>$" + item.precio + "</td>" +
                    "<td style='vertical-align:middle !important;' class='text-right'>" +
                        item.cantidad +
                    "</td>" +
                     "<td style='vertical-align:middle !important;' class='text-right'>" +
                     item.iva +
                    "</td>" +
                     "<td style='vertical-align:middle !important;' class='text-right'>" +
                     item.envio +
                    "</td>" +
                    "</div>" +
                    "</td>" +
                    "<td style='vertical-align:middle !important;' class='text-right'>$<span>" + item.total + "<span></td>" +
                    "</tr>";
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return html;

        }


     


      



       


       
    }
}