using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;

namespace webIntegradorSeptimo.Controllers
{
    public class ConfiguracionesController : Controller
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



        public ActionResult unDato()
        {
            configuracion ca = new configuracion();
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                ca = db.configuracion.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            return Json(ca, JsonRequestBehavior.AllowGet);
        }

        public string guardar(configuracion form)
        {
            string res = "";
            try
            {

                form.fecha_registro = DateTime.Now;
                if (form.idConfiguracion == 0)
                {
                    db.configuracion.Add(form);
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





    }
}