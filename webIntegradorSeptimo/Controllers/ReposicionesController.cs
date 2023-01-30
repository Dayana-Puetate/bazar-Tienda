using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;

namespace webIntegradorSeptimo.Controllers
{
    public class ReposicionesController : Controller
    {
        // GET: Reposiciones
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


        public ActionResult reposicion()
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

        public string listar()
        {
            string html = "";
            try
            {
                List<reposicion> lista = new List<reposicion>();

                lista = db.reposicion.ToList();
                int cont = 1;
                foreach (var item in lista)
                {
                    html += "<tr>" +
                        "<td>" + cont.ToString() + "</td>" +
                        "<td>" + item.fecha + "</td>" +
                        "<td>" + item.producto.nombre + "</td>" +
                        "<td>" + item.cantidad + "</td>" +
                        "</tr>";
                    cont++;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                html = "error";
            }

            return html;
        }

        public string guardar(reposicion form)
        {
            string res = "";
            try
            {
                    db.reposicion.Add(form);
                    form.fecha = DateTime.Now;
                    db.SaveChanges();

                    var pro=db.producto.Find(form.idProducto);
                    pro.stock = pro.stock + form.cantidad;
                    db.Entry(pro).State = EntityState.Modified;
                    db.SaveChanges();
                    res = "ok";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                res = "Error en la conexión";
            }
            return res;
        }


        public string comboProducto()
        {
            string html = "";
            try
            {
                var lista = db.producto.ToList();
                foreach (var item in lista)
                {
                    html += "<option value='"+item.idProducto+"'>"+item.nombre+"</option>";
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