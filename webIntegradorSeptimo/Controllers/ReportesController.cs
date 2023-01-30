using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;

namespace webIntegradorSeptimo.Controllers
{
    public class ReportesController : Controller
    {
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


        public string reporte(int? idEstado,DateTime? desde,DateTime? hasta)
        {
            string html = "";
            try
            {

                var lista = db.pedido.AsNoTracking().Where(x => x.idEstado == idEstado).ToList();
                foreach (var item in lista.OrderByDescending(x=>x.idPedido))
                {
                    var f1 = DateTime.Parse(desde.ToString()) - DateTime.Parse(item.fecha_pedido.ToString());
                    var f2 = DateTime.Parse(item.fecha_pedido.ToString()) - DateTime.Parse(hasta.ToString());
                    var t1 = f1.TotalDays;
                    var t2 = f2.TotalDays;
                    if(t1<0 && t2 <1)
                    {
                        html += "<tr>" +
                       "<td>" + item.idPedido + "</td>" +
                       "<td>" + item.fecha_pedido + "</td>" +
                       "<td>" + item.cliente.cobertura.ciudad + "</td>" +
                       "<td>" + item.cliente.nombre + " " + item.cliente.apellido + "</td>" +
                       "<td>" + item.subtotal + "</td>" +
                       "<td>" + item.iva + "</td>" +
                       "<td>" + item.envio + "</td>" +
                       "<td>" + item.total + "</td>" +
                       "</tr>";
                    }
                   
                }

                if (html != "")
                {
                    var en="<tr class='font-weight-bold'>" +
                        "<td>Pedido#</td>"+
                        "<td>Fecha</td>" +
                        "<td>Ciudad</td>" +
                        "<td>Cliente</td>" +
                        "<td>Subtotal</td>" +
                        "<td>Iva</td>" +
                        "<td>Envio</td>" +
                        "<td>Total</td>" +
                        "</tr>";

                    html = en + html;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return html;
        }

        public string comboEstados()
        {
            string html = "";
            try
            {
                var lista = db.estado.AsNoTracking().Where(x => x.activo==true).ToList();
                foreach (var item in lista)
                {
                    html += "<option value='"+item.idEstado+"'>"+item.estado1+"</option>";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return html;
        }



    }
}