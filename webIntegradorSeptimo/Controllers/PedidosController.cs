using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;

namespace webIntegradorSeptimo.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
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
                var listaPedidos = db.pedido.Where(x => x.idEstado<4).ToList();

                foreach (var item in listaPedidos)
                {
                    var estado = db.logpedido.Where(x => x.idPedido == item.idPedido).OrderByDescending(x => x.idLog).FirstOrDefault();
                    html += "<tr>" +
                           "<td valing='middle' class='text-center'>" + item.idPedido + "</td>" +
                           "<td valing='middle' class='text-cetner'>" + item.fecha_pedido+ "</td>" +
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


        public string datosEnvio(int? id)
        {
            string html = "";

            try
            {

                if (id == null || id == 0)
                {
                    return "";
                }

                var item = db.pedido.Where(x => x.idPedido == id).FirstOrDefault();

                    html += "<tr>" +
                        "<td class='font-weight-bold'>Nombre<td>" +
                        "<td>" + item.cliente.nombre + " " + item.cliente.apellido + "<td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td class='font-weight-bold'>Ciudad<td>" +
                        "<td>" + item.cliente.cobertura.ciudad + "<td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td class='font-weight-bold'>Dirección<td>" +
                        "<td>" + item.cliente.direccion+ "<td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td class='font-weight-bold'>Email<td>" +
                        "<td>" + item.cliente.correo + "<td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td class='font-weight-bold'>Teléfono<td>" +
                        "<td>" + item.cliente.celular + "<td>" +
                        "</tr>";          
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return html;

        }


        public string puedeCancelarPedido(int? id)
        {
            string res = "";

            try
            {
                if (id == null || id == 0)
                {
                    return "";
                }

                var pedido = db.pedido.AsNoTracking().Where(x => x.idPedido == id && x.idEstado == 1).Count();
                if (pedido == 0)
                {
                    return "";
                }else
                {
                    res = "ok";
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            return res;
        }


        public string botonAccion(int? id)
        {
            string res = "";

            try
            {
                if (id == null || id == 0)
                {
                    return "";
                }

                var cobrado = db.pedido.AsNoTracking().Where(x => x.idPedido == id && x.idEstado == 1).Count();
                if (cobrado == 1)
                {
                    return "<button type='button' class='btn btn-theme' id='btnGuardarEstado' onclick='guardarEstado(\"" + id + "\",\""+2+"\")'>COBRADO</button>";
                }

                var enviado = db.pedido.AsNoTracking().Where(x => x.idPedido == id && x.idEstado == 2).Count();
                if (enviado == 1)
                {
                    return "<button type='button' class='btn btn-theme' id='btnGuardarEstado' onclick='guardarEstado(\"" + id + "\",\"" + 3 + "\")'>ENVIADO</button>";
                }

                var entregado = db.pedido.AsNoTracking().Where(x => x.idPedido == id && x.idEstado == 3).Count();
                if (entregado == 1)
                {
                    return "<button type='button' class='btn btn-theme' id='btnGuardarEstado' onclick='guardarEstado(\"" + id + "\",\"" + 4 + "\")'>ENTREGADO</button>";
                }




            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            return res;
        }



        public string guardarEstado(int? id,int? idEstado)
        {
            string res = "";

            try
            {
                if (id == null || id == 0 || idEstado==null || idEstado==0)
                {
                    return "";
                }

                var ped = db.pedido.Find(id);
                ped.idEstado = idEstado;
                db.Entry(ped).State = EntityState.Modified;
                logpedido log = new logpedido();
                log.idEstado = idEstado;
                log.idPedido = id;
                log.fecha = DateTime.Now;
                db.logpedido.Add(log);
                db.SaveChanges();
                res = "ok";
                enviarCorreoEstado(id, idEstado);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            return res;
        }


        public string enviarCorreoEstado(int? idPedido, int? idEstado)
        {
            string dirCor = "~/Correos/pedidoActividad.html";
            var patch = Server.MapPath(dirCor);
            var hoy = DateTime.Now;
            StringBuilder emailHtml = new StringBuilder(System.IO.File.ReadAllText(patch));
            var pedido = db.pedido.Where(x => x.idPedido == idPedido).FirstOrDefault();
            var estado = db.estado.Where(x => x.idEstado == idEstado).FirstOrDefault();
            emailHtml.Replace("M3NS4J3", "EL pedido COD#" + idPedido.ToString() + " ha sido " + estado.estado1);
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress("sa.danielsvr61@uniandes.edu.ec");
            correo.To.Add(pedido.cliente.correo);
            correo.Subject = "BAZAR PEDIDO " + estado.estado1;
            correo.Body = emailHtml.ToString();
            correo.IsBodyHtml = true;
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.office365.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("sa.danielsvr61@uniandes.edu.ec", "2461549dsvr");
            try
            {
                smtp.Send(correo);
                return "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "error";
            }

        }


    }
}