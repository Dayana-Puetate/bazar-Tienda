using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;

namespace webIntegradorSeptimo.Controllers
{
    public class CategoriasController : Controller
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


        public ActionResult Categoria()
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
                List<categoria> lista = new List<categoria>();

                lista = db.categoria.ToList();
                int cont = 1;
                foreach (var item in lista)
                {
                    html += "<tr>" +
                        "<td>" + cont.ToString() + "</td>" +
                        "<td>" + item.nombre + "</td>" +
                        "<td class='text-center' style='font-size:15px;'>" +
                        "<span class='pr-1 texto-verde' style='cursor:pointer' title='Editar " + item.nombre + "' onclick='redirect(" + item.idCategoria + ")'><i class='fa fa-edit'></i></span>" +
                        "<span class='pl-1 text-danger' style='cursor:pointer' title='Eliminar " + item.nombre + "' onclick='preguntar(" + item.idCategoria + ")'><i class='fa fa-trash'></i></span>" +
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
            categoria ca = new categoria();
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                ca = db.categoria.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            return Json(ca, JsonRequestBehavior.AllowGet);
        }

        public string guardar(categoria form)
        {
            string res = "";
            try
            {
                if (verificaRepetido(form))
                {
                    return "repe";
                }

                if (form.idCategoria == 0)
                {
                    db.categoria.Add(form);
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


        private bool verificaRepetido(categoria form) 
        {
            bool res = false;
            try
            {
                var ant = db.categoria.AsNoTracking().Where(x=>x.idCategoria==form.idCategoria).FirstOrDefault();
                var bus = form.nombre.Trim().ToUpper();
                var cant = db.categoria.Where(x => x.nombre.ToUpper() == bus).Count();
                if (ant != null)
                {
                    if (ant.nombre.ToUpper().Trim() == form.nombre.ToUpper().Trim())
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


        public string eliminar(int id)
        {
            string res = "";
            try
            {
                var obj = db.categoria.Find(id);
                if (obj != null)
                {
                    db.categoria.Remove(obj);
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

    
    }
}