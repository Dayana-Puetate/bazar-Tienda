using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webIntegradorSeptimo.Models;

namespace webIntegradorSeptimo.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Producto()
        {
            return View();
        }
        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult Busqueda()
        {
            return View();
        }
        public ActionResult Carrito()
        {
            return View();
        }

        public ActionResult Cuenta()
        {
            if (verificarSesion() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Registro()
        {
            if (verificarSesion() == false)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Gracias()
        {
            if (verificarSesion() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        public ActionResult Pedidos()
        {
            if (verificarSesion() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Pedido()
        {
            if (verificarSesion() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        private bool verificarSesion()
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

    }
}