using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;

namespace webIntegradorSeptimo.Controllers
{
    public class OfertasController : Controller
    {
        // GET: Ofertas
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


        public ActionResult Oferta()
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
                List<oferta> lista = new List<oferta>();

                lista = db.oferta.ToList();
                int cont = 1;
                foreach (var item in lista)
                {
                    var activo = "";
                    if (item.activo==true)
                    {
                        activo = "<span class='text-center text-success'>SI</span>";
                    }
                    else
                    {
                        activo = "<span class='text-center text-danger'>NO</span>";
                    }
                    html += "<tr>" +
                        "<td>" + cont.ToString() + "</td>" +
                        "<td>" + item.producto.nombre + "</td>" +
                        "<td class='text-right'>" + item.porcentaje + "%</td>" +
                        "<td>" + DateTime.Parse(item.fecha_inicio.ToString()).ToShortDateString() + "</td>" +
                        "<td>" + DateTime.Parse(item.fecha_fin.ToString()).ToShortDateString() + "</td>" +
                        "<td class='text-center'>" + activo + "</td>" +
                        "<td class='text-center' style='font-size:15px;'>" +
                        "<span class='pr-1 texto-verde' style='cursor:pointer' title='Editar " + item.producto.nombre + "' onclick='redirect(" + item.idOferta + ")'><i class='fa fa-edit'></i></span>" +
                        "<span class='pl-1 text-danger' style='cursor:pointer' title='Eliminar " + item.producto.nombre + "' onclick='preguntar(" + item.idOferta + ")'><i class='fa fa-trash'></i></span>" +
                        "</td>" +
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






        public ActionResult unDato(int id)
        {
            oferta ca = new oferta();
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                ca = db.oferta.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            return Json(ca, JsonRequestBehavior.AllowGet);
        }

        public string guardar(oferta form)
        {
            string res = "";
            try
            {
                if (verificaRepetido(form,form.idOferta))
                {
                    return "repe";
                }

                if (form.idOferta == 0)
                {
                    db.oferta.Add(form);
                    db.SaveChanges();
                    res = "ok";
                }
                else
                {

                    db.Entry(form).State = EntityState.Modified;
                    db.SaveChanges();
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


        private bool verificaRepetido(oferta form,int id)
        {
            bool res = false;
            var hoy = DateTime.Now;
            try
            {
                if (form.activo == false)
                {
                    return false;
                }

                if (id == 0)
                {
                    var ant = db.oferta.AsNoTracking().Where(x => x.idProducto == form.idProducto && hoy < x.fecha_fin && x.activo == true).FirstOrDefault();
                    if (ant != null)
                    {
                        return true;
                    }
                }else
                {
                    var ant = db.oferta.AsNoTracking().Where(x => x.idProducto == form.idProducto && x.idOferta != id &&   hoy < x.fecha_fin && x.activo == true).FirstOrDefault();
                    if (ant != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                
                }



                
        
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return res;

        }


        public string eliminar(int id)
        {
            string res = "";
            try
            {
                var obj = db.oferta.Find(id);
                if (obj != null)
                {
                    db.oferta.Remove(obj);
                    db.SaveChanges();
                    res = "ok";
                }

            }
            catch (Exception ex)
            {
                res = ex.Message;
                Console.WriteLine(ex.Message);
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