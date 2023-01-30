using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;
using webIntegradorSeptimo.Negocio;

namespace webIntegradorSeptimo.Controllers
{
    public class HomeController : Controller
    {
        private context_base db = new context_base();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }


        public bool verificarSesion()
        {
            bool res = false;
            foreach (var item in Session)
            {
                if (item.ToString().StartsWith("user"))
                {
                    res = true;
                }
            }
            return res;

        }


        public string salir()
        {
            try
            {
                Session.Remove("userNombre");
                Session.Remove("userId");
                Session.Remove("userCorreo");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


            return "ok";
        }



        public ActionResult unDato()
        {
            cliente ca = new cliente();

            
            try
            {
                int? id = Convert.ToInt32(Convert.ToString(Session["userId"]));
                if (id!=null || id != 0)
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    ca = db.cliente.Find(id);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return RedirectToAction("Index", "Home");
            }

            return Json(ca, JsonRequestBehavior.AllowGet);
        }

        public string guardar(cliente form)
        {
            string res = "";
            try
            {
                if (verificaRepetido(form))
                {
                    return "repe";
                }

                if (form.idCliente == 0)
                {
                    form.activo = true;
                    form.fecha_registro = DateTime.Now;
                    db.cliente.Add(form);
                    db.SaveChanges();
                    res = "ok";
                }
                else
                {
                    var ant = db.cliente.AsNoTracking().Where(x => x.idCliente == form.idCliente).FirstOrDefault();
                    db.Entry(form).State = EntityState.Modified;
                    db.SaveChanges();
                    string mensaje = "Los datos de su cuenta han sido actualizados";
                    string correoAnterior = "";
                    string correoActual = form.correo;
                    if (ant.contra != form.contra)
                    {
                        mensaje = "La contraseña se ha cambiado";
                    }else if (ant.correo != form.correo)
                    {
                        mensaje = "Su correo ha cambiado a:  "+correoActual;
                        correoAnterior = ant.correo;
                    }

                    enviarCorreoCuenta(mensaje, correoAnterior, correoActual);
                    res = "ok";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                res = "Error en la conexión";
            }
            return res;
        }


        private bool verificaRepetido(cliente form)
        {
            bool res = false;
            try
            {
                var ant = db.cliente.AsNoTracking().Where(x => x.idCliente == form.idCliente).FirstOrDefault();
                var bus = form.correo.Trim().ToUpper();
                var cant = db.cliente.Where(x => x.correo.ToUpper() == bus).Count();
                if (ant != null)
                {
                    if (ant.correo.ToUpper().Trim() == form.correo.ToUpper().Trim())
                    {
                        return false;
                    }
                }
                if (cant > 0)
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return res;

        }

        public string comboCobertura()
        {
            string html = "";
            try
            {
                var lista = db.cobertura.ToList();
                foreach (var item in lista)
                {
                    html += "<option value='" + item.idCobertura + "'>" + item.ciudad + "</option>";
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            return html;

        }

        public string login(string correo,string contra)
        {
            string res = "ok";
            try
            {
                var user = db.cliente.Where(x => x.contra == contra && x.correo == correo).FirstOrDefault();
                if (user == null)
                {
                    return "El usuario y/o la contraseña son incorrectos";
                }
                Session["userNombre"] = user.nombre;
                Session["userId"] = user.idCliente;
                Session["userCorreo"] = user.correo;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return res;
        }


        public string registrarPedido(string detalle)
        {
            string res = "";
            List<detallePedido> listaDetalle = new List<detallePedido>();
            try
            {
                var hoy = DateTime.Now;
                var idCliente = Convert.ToInt32(Convert.ToString(Session["userId"]));
                decimal? total = 0;
                decimal? subtotal = 0;
                decimal? iva = 0;
                decimal? envio = 0;
                var aux = JsonConvert.DeserializeObject<List<detallePedidoAuxiliar>>(detalle);
                foreach (var item in aux)
                {
                    total = total + item.valorTotal;
                    subtotal = subtotal + item.subtotal;
                    iva = iva + item.costoIva;
                    envio = envio + item.costoEnvio;
                    var pr = db.producto.Find(item.idProducto);
                    if (pr.stock < item.cantidad)
                    {
                        return "Lo sentimos no poseemos el stock para: " + pr.nombre;
                    }

                }
                pedido ped = new pedido();
                ped.idCliente = idCliente;
                ped.fecha_pedido = hoy;
                ped.comentario = "";
                ped.idEstado = 1;
                ped.total = total;
                ped.subtotal = subtotal;
                ped.envio = envio;
                ped.iva = iva;
   

                db.pedido.Add(ped);
                db.SaveChanges();

                var cl = db.pedido.Where(x => x.idCliente == idCliente).OrderByDescending(x=>x.idPedido).FirstOrDefault();
                int? idPedido = cl.idPedido;
                logpedido logPed = new logpedido();
                logPed.idEstado = 1;
                logPed.idPedido = idPedido;
                logPed.fecha = hoy;
                db.logpedido.Add(logPed);
                db.SaveChanges();
                foreach (var item in aux)
                {
                    detallePedido det = new detallePedido();
                    det.idPedido = idPedido;
                    det.idProducto = item.idProducto;
                    det.iva = item.costoIva;
                    det.precio = item.precio;
                    det.descuento = item.descuento;
                    det.envio = item.costoEnvio;
                    det.iva = item.costoIva;
                    det.total = item.valorTotal;
                    det.cantidad = item.cantidad;
                    db.detallePedido.Add(det);
                    var pr = db.producto.Find(item.idProducto);
                    pr.stock = pr.stock - item.cantidad;
                    db.Entry(pr).State = EntityState.Modified;
                }
                db.SaveChanges();
                enviarCorreo(idPedido,idCliente,total);

                res = "ok";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

               
            return res;
        }

        public string listaPedidos()
        {
            string html = "";

            try
            {
                var idCliente = Convert.ToInt32(Convert.ToString(Session["userId"]));
                var listaPedidos = db.pedido.Where(x => x.idCliente == idCliente).ToList();

                foreach (var item in listaPedidos)
                {
                    var estado = db.logpedido.Where(x => x.idPedido == item.idPedido).OrderByDescending(x => x.idLog).FirstOrDefault();
                    html +="<tr>"+
                           "<td class='text-center'>"+item.idPedido+"</td>"+
                           "<td class='text-cetner'>" + DateTime.Parse(item.fecha_pedido.ToString()).ToLongDateString() + "</td>" +
                           "<td class='text-cetner'>" +estado.estado.estado1+"</td>"+
                           "<td class='text-cetner'>" + DateTime.Parse(estado.fecha.ToString()).ToLongDateString() + "</td>" +
                           "<td class='text-right'>" +item.total+"</td>"+
                           "<td class='text-center'><button class='btn btn-primary btn-sm' onclick='verPedido("+item.idPedido+")'><i class='fa fa-eye'></i></button></td>" +
                           "</tr>";
                }
                

                
                                

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return html;

        }

        public ActionResult unPedido(int? id)
        {
            pedido p = new pedido();
            try
            {
                if(id!=0 || id != null)
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    p = db.pedido.AsNoTracking().Where(x => x.idPedido == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        public string unPedidoDetalle(int? id)
        {
            string html = "";

            try
            {

                if(id==null || id == 0)
                {
                    return "";
                }

                var lista = db.detallePedido.Where(x => x.idPedido == id).ToList();

                foreach (var item in lista)
                {

                    string src = db.imagenes_producto.Where(x => x.idProducto == item.idProducto).Select(x => x.referencia).FirstOrDefault();

                    html += "<tr>" +
                    "<td class='product-thumbnail'>" +
                    "<img src='../../Imagenes/" + src + "' class='rounded-circle img-fluid shadow' style='width:70px;height:70px' alt='Image' >" +
                    "</td>" +
                    "<td class='product-name' style='max-width:290px'>" +
                    "<h2 class='h5 text-black'>" + item.producto.nombre + "</h2>" +
                    "</td>" +
                    "<td>" +
                    item.descuento + "%" +
                    "</td>" +
                    "<td>$" + item.precio + "</td>" +
                    "<td>" +
                        item.cantidad +
                    "</td>" +
                     "<td>" +
                     item.iva +
                    "</td>" +
                     "<td>" +
                     item.envio +
                    "</td>" +
                    "</div>" +
                    "</td>" +
                    "<td>$<span>" + item.total + "<span></td>"+
                    "</tr>";
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return html;

        }

        public string trackingPedido(int? id)
        {
            string html = "";

            try
            {
                if (id == null || id == 0)
                {
                    return "";
                }

                var lista = db.logpedido.AsNoTracking().Where(x => x.idPedido == id).ToList();
                foreach (var item in lista)
                {
                    html += "<tr>" +
                        "<td>"+DateTime.Parse(item.fecha.ToString()).ToLongDateString()+"</td>"+
                        "<td>"+item.estado.estado1+"</td>" +
                        "</tr>";
                }

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

                var pedido = db.pedido.AsNoTracking().Where(x => x.idPedido == id && x.idEstado==1).FirstOrDefault();
                if (pedido == null)
                {
                    return "";
                }
                var ant = DateTime.Parse(pedido.fecha_pedido.ToString());
                var cont = (DateTime.Now-ant).TotalDays;


                if (cont >3)
                {
                    res = "";
                }
                else
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


        public string cancelarPedido(int? id)
        {
            string res = "";

            try
            {
                if (id == null || id == 0)
                {
                    return "";
                }

                var ped = db.pedido.Find(id);
                ped.idEstado = 5;
                db.Entry(ped).State = EntityState.Modified;
                logpedido log = new logpedido();
                log.idEstado = 5;
                log.idPedido = id;
                log.fecha = DateTime.Now;
                db.logpedido.Add(log);
                db.SaveChanges();
                var listaProductos = db.detallePedido.AsNoTracking().Where(x => x.idPedido == id).ToList();
                foreach (var item in listaProductos)
                {
                    var produc = db.producto.Find(item.idProducto);
                    produc.stock = produc.stock + item.cantidad;
                    db.Entry(produc).State = EntityState.Modified;
                    db.SaveChanges();
                }

                res = "ok";
                enviarCorreoEstado(id, 5);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            return res;
        }

        public string enviarCorreo(int? idPedido,int idCliente,decimal? total)
        {
            string dirCor = "~/Correos/pedidoRecibido.html";
            var patch = Server.MapPath(dirCor);
            var hoy = DateTime.Now;
            StringBuilder emailHtml = new StringBuilder(System.IO.File.ReadAllText(patch));
            var cliente = db.cliente.AsNoTracking().Where(x => x.idCliente == idCliente).FirstOrDefault();
            emailHtml.Replace("CL!3NT3", cliente.nombre+" "+cliente.apellido);
            emailHtml.Replace("NUM3R0", "COD#"+idPedido.ToString());
            emailHtml.Replace("T0T4L", "$"+total.ToString()); 
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress("sa.danielsvr61@uniandes.edu.ec");
            correo.To.Add(cliente.correo);
            correo.Subject = "BAZAR HA RECIBIDO TÚ PEDIDO";
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


        public string enviarCorreoCuenta(string mensaje,string emailAnterior,string email)
        {
            string dirCor = "~/Correos/datosActualizados.html";
            var patch = Server.MapPath(dirCor);
            var hoy = DateTime.Now;
            StringBuilder emailHtml = new StringBuilder(System.IO.File.ReadAllText(patch));

            emailHtml.Replace("M3NS4J3",mensaje);

            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress("sa.danielsvr61@uniandes.edu.ec");
            correo.To.Add(email);
            if (emailAnterior != "")
            {
                correo.To.Add(emailAnterior);
            }
            correo.Subject = "BAZAR HA REALIZADO CAMBIOS EN SU CUENTA";
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


        public string enviarCorreoEstado(int? idPedido, int? idEstado)
        {
            string dirCor = "~/Correos/pedidoActividad.html";
            var patch = Server.MapPath(dirCor);
            var hoy = DateTime.Now;
            StringBuilder emailHtml = new StringBuilder(System.IO.File.ReadAllText(patch));
            var pedido = db.pedido.Where(x => x.idPedido == idPedido).FirstOrDefault();
            var estado = db.estado.Where(x => x.idEstado == idEstado).FirstOrDefault();
            emailHtml.Replace("M3NS4J3", "EL pedido COD#"+idPedido.ToString()+" ha sido "+estado.estado1);
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress("sa.danielsvr61@uniandes.edu.ec");
            correo.To.Add(pedido.cliente.correo);
            correo.Subject = "BAZAR PEDIDO "+estado.estado1;
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