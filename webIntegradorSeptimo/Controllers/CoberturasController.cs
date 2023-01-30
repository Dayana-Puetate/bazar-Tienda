using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;

namespace webIntegradorSeptimo.Controllers
{
    public class CoberturasController : Controller
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


        public ActionResult Cobertura()
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
                List<cobertura> lista = new List<cobertura>();

                lista = db.cobertura.ToList();
                int cont = 1;
                foreach (var item in lista)
                {
                    html += "<tr>" +
                        "<td>" + cont.ToString() + "</td>" +
                        "<td>" + item.ciudad + "</td>" +
                        "<td class='text-center' style='font-size:15px;'>" +
                        "<span class='pr-1 texto-verde' style='cursor:pointer' title='Editar " + item.ciudad + "' onclick='redirect(" + item.idCobertura + ")'><i class='fa fa-edit'></i></span>" +
                        "<span class='pl-1 text-danger' style='cursor:pointer' title='Eliminar " + item.ciudad + "' onclick='preguntar(" + item.idCobertura + ")'><i class='fa fa-trash'></i></span>" +
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
            cobertura ca = new cobertura();
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                ca = db.cobertura.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            return Json(ca, JsonRequestBehavior.AllowGet);
        }

        public string guardar(cobertura form)
        {
            string res = "";
            try
            {
                if (verificaRepetido(form))
                {
                    return "repe";
                }

                if (form.idCobertura == 0)
                {
                    db.cobertura.Add(form);
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


        private bool verificaRepetido(cobertura form)
        {
            bool res = false;
            try
            {
                var ant = db.cobertura.AsNoTracking().Where(x => x.idCobertura == form.idCobertura).FirstOrDefault();
                var bus = form.ciudad.Trim().ToUpper();
                var cant = db.cobertura.Where(x => x.ciudad.ToUpper() == bus).Count();
                if (ant != null)
                {
                    if (ant.ciudad.ToUpper().Trim() == form.ciudad.ToUpper().Trim())
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
                var obj = db.cobertura.Find(id);
                if (obj != null)
                {
                    db.cobertura.Remove(obj);
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