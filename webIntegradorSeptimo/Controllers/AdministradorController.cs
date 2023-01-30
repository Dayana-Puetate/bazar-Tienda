using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;

namespace webIntegradorSeptimo.Controllers
{
    public class AdministradorController : Controller
    {
        private context_base db = new context_base();

        // GET: Administrador
        public ActionResult Index()
        {
            if (verificarSesion())
            {
                return RedirectToAction("Bienvenida", "Administrador");
            }
            else
            {
                return View();
            }

        }


        public ActionResult Bienvenida()
        {
            if (verificarSesion())
            {
                return View();
            }
            else
            {
              return  RedirectToAction("Index", "Administrador");
            }

        }

        public ActionResult EditarCuenta()
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

        public string salir()
        {
            try
            {
                Session.Remove("adminNombre");
                Session.Remove("adminId");
                Session.Remove("adminUsuario");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
 

            return "ok";
        }




        public ActionResult login(string usuario, string contra)
        {
            administrador cu = new administrador();
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                cu = db.administrador.Where(x => x.usuario == usuario && x.contra == contra && x.activo == true).FirstOrDefault();
                if (cu != null)
                {
                    Session["adminNombre"] = cu.nombre + " " + cu.apellido;
                    Session["adminId"] = cu.idAdministrador;
                    Session["adminUsuario"] = cu.usuario;


                    var hola = Session.Count;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Json(cu,JsonRequestBehavior.AllowGet);

        }


        public ActionResult datosCuenta()
        {
            administrador cu = new administrador();
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                int id = (int)Session["adminId"];
                cu = db.administrador.Find(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Json(cu, JsonRequestBehavior.AllowGet);

        }

        public string guardar(administrador form)
        {
            string res = "";
            try
            {
                if (form.idAdministrador == 0)
                {
                    form.activo = true;
                    form.fecha_registro = DateTime.Now;
                    db.administrador.Add(form);
                    db.SaveChanges();
                    res = "ok";
                }
                else
                {
                    form.activo = true;
                    form.fecha_registro = DateTime.Now;
                    db.Entry(form).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    res = "ok";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                res="Error en la conexión";
            }
            return res;
        }

    }
}