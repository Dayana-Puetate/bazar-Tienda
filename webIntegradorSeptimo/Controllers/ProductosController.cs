using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using webIntegradorSeptimo.Models;
using webIntegradorSeptimo.Negocio;

namespace webIntegradorSeptimo.Controllers
{
    public class ProductosController : Controller
    {

        private context_base db = new context_base();
        private Validadores val = new Validadores();
        // GET: Productos
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


        public ActionResult Producto()
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
                List<producto> lista = new List<producto>();

                lista = db.producto.Include(x=>x.categoria).ToList();
                int cont = 1;
                foreach (var item in lista)
                {
                    string envio = "No";
                    if (item.paga_envio==true)
                    {
                        envio = "Si";
                    }
                    var img = db.imagenes_producto.Where(x => x.idProducto == item.idProducto).Select(x => x.referencia).FirstOrDefault();
                    html += "<tr>" +
                        "<td class='text-center align-middle' valign='middle'>" + cont.ToString() + "</td>" +
                        "<td class='text-center align-middle'>" +
                        "<img src='../../Imagenes/"+img+ "' style='width:50px;height:50px;border-radius:50%;border:1px solid #DEE2E6' />" +
                        "</td>" +
                        "<td class='align-middle'>" + item.categoria.nombre + "</td>" +
                        "<td class='align-middle'>" + item.nombre + "</td>" +
                        "<td class='text-right align-middle'>" + item.pvp + "</td>" +
                        "<td class='text-right align-middle'>" + item.stock + "</td>" +
                        "<td class='text-center align-middle'>" + envio + "</td>" +
                        "<td class='text-center align-middle' style='font-size:15px;'>" +
                        "<span class='pr-1 texto-verde' style='cursor:pointer' title='Editar " + item.nombre + "' onclick='redirect(" + item.idProducto + ")'><i class='fa fa-edit'></i></span>" +
                        "<span class='pl-1 text-danger' style='cursor:pointer' title='Eliminar " + item.nombre + "' onclick='preguntar(" + item.idProducto + ")'><i class='fa fa-trash'></i></span>" +
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
            producto ca = new producto();
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                ca = db.producto.Include(x => x.categoria).Where(y => y.idProducto == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }


            return Json(ca,JsonRequestBehavior.AllowGet) ;
        }


        public string guardar(HttpPostedFileBase[] files, producto form,string pvp,string stock,string iva,string vectorEliminarImagenes)
        {
            string res = "";
            bool edita = false;
            try
            {
                if (verificaRepetido(form))
                {
                    return "repe";
                }

                if (form.idProducto == 0)
                {
                    form.pvp = val.convertDecimal(pvp);
                    form.stock = val.convertDecimal(stock);
                    form.iva = val.convertDecimal(iva);
                    form.fecha_registro = DateTime.Now;
                    db.producto.Add(form);
                    db.SaveChanges();
                    res = "ok";
                }
                else
                {
                    var fecha_registro = db.producto.AsNoTracking().Where(x => x.idProducto == form.idProducto).Select(x=>x.fecha_registro).FirstOrDefault();

                    form.pvp = val.convertDecimal(pvp);
                    form.stock = val.convertDecimal(stock);
                    form.iva = val.convertDecimal(iva);
                    form.fecha_registro = fecha_registro;
                    db.Entry(form).State = EntityState.Modified;
                    db.SaveChanges();
                    res = "ok";
                    edita = true;

                }

                int id = db.producto.Where(x => x.nombre == form.nombre && x.idCategoria == form.idCategoria).Select(x => x.idProducto).FirstOrDefault();
                subirImagenes(id, files,vectorEliminarImagenes,edita);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return res;
        }
       

        private void subirImagenes(int id,HttpPostedFileBase[] files,string vector,bool edita)
        {


            try
            {

                if (edita)
                {
                    if (vector != "")
                    {
                        string cod = "$7@(10";
                        var v = vector.Split(new string[] { cod }, StringSplitOptions.None);
                        foreach (var eliminar in v)
                        {
                            if (eliminar != "")
                            {
                                var img = db.imagenes_producto.Where(x => x.referencia.StartsWith(eliminar.ToString())).First();
                                db.imagenes_producto.Remove(img);
                                db.SaveChanges();
                                string savepath = Path.Combine(Server.MapPath("~/Imagenes/"));
                                string savefile = Path.Combine(savepath, eliminar);
                                var fileDel = new FileInfo(savefile);
                                fileDel.Delete();
                            }
                        }

                    }
                }

                if (files ==null) {
                    return;
                }

                int cont = db.imagenes_producto.AsNoTracking().Where(x => x.idProducto == id).Count() + 1;

                foreach (var item in files)
                {

                    string url = id.ToString() + "_" + cont.ToString() + "_" + item.FileName;
                    string savepath = Path.Combine(Server.MapPath("~/Imagenes/"));
                    string savefile = Path.Combine(savepath, url);
                    item.SaveAs(savefile);

                    imagenes_producto img = new imagenes_producto();
                    img.idProducto = id;
                    img.referencia = url;
                    img.size = item.ContentLength.ToString();
                    db.imagenes_producto.Add(img);
                    db.SaveChanges();
                }



            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }




        private bool verificaRepetido(producto form)
        {
            bool res = false;
            try
            {
                var ant = db.producto.AsNoTracking().Where(x => x.idProducto == form.idProducto && x.idCategoria==form.idCategoria).FirstOrDefault();
                var bus = form.nombre.Trim().ToUpper();
                var cant = db.producto.Where(x => x.nombre.ToUpper() == bus && x.idCategoria==form.idCategoria).Count();
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


        public ActionResult imagenes(int id)
        {
            List<imagenes_producto> lista = new List<imagenes_producto>();
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                lista = db.imagenes_producto.Where(x => x.idProducto == id).ToList();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public string eliminar(int id)
        {
            string res = "";
            try
            {

                var lista = db.imagenes_producto.AsNoTracking().Where(x => x.idProducto == id).ToList();

 

                db.imagenes_producto.RemoveRange(db.imagenes_producto.Where(x => x.idProducto == id));
                var obj = db.producto.Find(id);
                if (obj != null)
                {
                    db.producto.Remove(obj);
                    db.SaveChanges();
                    res = "ok";
                    foreach (var item in lista)
                    {
                        string savepath = Path.Combine(Server.MapPath("~/Imagenes/"));
                        string savefile = Path.Combine(savepath, item.referencia);
                        var fileDel = new FileInfo(savefile);
                        fileDel.Delete();
                    }

                }

            }
            catch (Exception ex)
            {
                res = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return res;
        }


        public string comboCategorias()
        {
            string html = "";
            try
            {
                foreach (var item in db.categoria.ToList())
                {
                    html += "<option value='"+item.idCategoria+"'>"+item.nombre+"</option>";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return html;
        }

        public string listarOfertas()
        {
            string html = "";
            var hoy = DateTime.Now;
            try
            {
                List<oferta> lista = new List<oferta>();
                lista = db.oferta.Where(x => x.fecha_fin >= hoy && x.activo == true && x.producto.stock>=1).ToList();
                foreach (var item in lista)
                {
                    var img = db.imagenes_producto.Where(x => x.idProducto == item.idProducto).Select(x => x.referencia).FirstOrDefault();
                    double des = 1-(Convert.ToDouble(item.porcentaje)/100);
                    var precioFinal = des *Convert.ToDouble(item.producto.pvp);

                    html += "<div class='text-center item mb-4 item-v2 shadow-sm' style='min-height:525px;max-height:525px;border:1px solid #4ECDC4'> " +
                            "<span class='onsale'>Oferta</span> "+
                            "<a href = '../../Tienda/Producto/?id=" + item.producto.idProducto+ "' ><img class='pb-1 pt-2 ml-auto mr-auto'  src='../../Imagenes/" + img+ "' alt='Image' style='max-height:300px;max-width:300px;min-height:300px;min-width:300px'></a>" +
                            "<div style='min-height:104px;margin: 0 auto;'>" +
                            "<h3 class='text-dark'><a href='../../Tienda/Producto/?id=" + item.producto.idProducto + "' > "+item.producto.nombre+"</a></h3>" +
                            "</div>" +
                            "<div style='border-top:1px solid #4ECDC4'>"+
                            "<p><small class='text-dark'><strong class='text-primary'>STOCK: </strong>" + item.producto.stock+"</small></p>"+
                            "<p class='price'>ANTES <strong style='text-decoration: line-through !important;' class='text-danger'>$" + item.producto.pvp+"</strong></p>" +
                            "<h5 class='price'><strong class='text-primary'>-"+item.porcentaje+ "%</strong><strong class='text-muted' > AHORA </strong> " +
                            "<strong>$"+Math.Round(precioFinal, 2) + "</strong></h5>"+
                            "</div>"+
                            "</div>";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());              
            }

            return html;
        }



        public string dropdownCategorias()
        {
            string html = "";
            try
            {
                List<categoria> lista = new List<categoria>();
                lista = db.categoria.ToList();
                foreach (var item in lista)
                {
                    var cantProducto = db.producto.Where(x=>x.idCategoria==item.idCategoria && x.stock>=1).Count();
                    if (cantProducto > 0)
                    {
                        html += "<li><a href='../../Tienda/Categoria/?id=" + item.idCategoria + "'>" + item.nombre + " <span class=''>(" + cantProducto + ")</span></a></li>";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return html;
        }


        public string listarProductosCategorias(int id)
        {
            string html = "";
            string paginas = "";
            var hoy = DateTime.Now;

            try
            {
                var listaProductos = db.producto.Where(x => x.idCategoria == id && x.stock>=1).ToList();
                var total = listaProductos.Count();

                if (total == 0)
                {
                    return "";
                }
                double paginadores = Convert.ToDouble(total)/6;
                double redondeado = Math.Round(paginadores, 0);
                if (paginadores > redondeado)
                {
                    redondeado += 1; 
                }

                var cont = 1;
                var cont2 = 1;
                paginas += "<div class='row mt-5'><div class='col-md-12 text-center'><div class='site-block-27'><ul>";
                for (var i = 1; i <= redondeado; i++)
                {
                    var activo = "";
                    if (i == 1)
                    {
                        activo = "active";
                    }
                    paginas += "<li class='"+activo+" paginadores' onclick='paginar(\"#tabCategoria" + i + "\",this)' style='cursor:pointer'><span>" + i + "</span></li>";
                }
                paginas += "</ul></div></div></div>";

                foreach (var item in listaProductos)
                {
                    var img = db.imagenes_producto.Where(x => x.idProducto == item.idProducto).Select(x => x.referencia).FirstOrDefault();
                    var oferta = db.oferta.Where(x => x.fecha_fin >= hoy && x.activo == true && x.idProducto == item.idProducto).FirstOrDefault();
                    var tira = "";
                    var mostrarPrecio = "<p class='price'>$"+item.pvp+"</p>";
                    var porcen = "";
                    if (oferta != null)
                    {
                        tira = "<span class='onsale'>Oferta</span>";
                        double des = 1 - (Convert.ToDouble(oferta.porcentaje) / 100);
                        var precioFinal = des * Convert.ToDouble(item.pvp);
                        porcen = "<strong class='text-primary font-weight-bold pl-2'>-" + oferta.porcentaje + "% </strong>";
                        mostrarPrecio = "<p class='price'><del class='text-danger'>$" + item.pvp + "</del> &mdash; $" + Math.Round(precioFinal, 2) + porcen+"</p>";

                    }
                    if (cont == 1)
                    {
                        html += "<div class='active row tabCategoria' id='tabCategoria"+cont2+"'>";
                    }



                    html += "<div class='col-sm-6 col-lg-4 text-center item mb-4 item-v2 ' style='min-height:525px;max-height:525px;border:1px solid #4ECDC4'>" +
                    tira+
                    "<a href='../../Tienda/Producto/?id="+item.idProducto+ "' > <img src='../../Imagenes/" + img + "' alt='Image' class='pb-1 pt-2 ml-auto mr-auto' style='max-height:300px;max-width:300px;min-height:300px;min-width:300px'></a>" +
                    "<div class='w-100' style='min-height:104px;margin: 0 auto;'>" +
                    "<h3 class='text-dark'><a href='../../Tienda/Producto/?id=" + item.idProducto + "'>"+item.nombre+"</a></h3>"+
                    "</div>"+
                    "<p><small class='text-dark'><strong class='text-primary'>STOCK: </strong>" + item.stock + "</small></p>" +
                    mostrarPrecio +
                    "</div>";
                    if (cont % 6 == 0 && cont < listaProductos.Count())
                    {
                        cont2 += 1;
                        html += "</div>";
                        html += "<div class='row tabCategoria' id='tabCategoria" + cont2 + "' hidden>";
                    }

                    if (cont == listaProductos.Count())
                    {
                        html += "</div>";
                    }
                    cont++;
                }




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return html+paginas;
        }




        public string buscarProductos(string id)
        {
            string html = "";
            string paginas = "";
            var hoy = DateTime.Now;

            try
            {
                var listaProductos = db.producto.Where(x => (x.nombre.ToUpper().Trim()+""+x.pvp).Contains(id.ToUpper().Trim()) && x.stock>=1).ToList();
                var total = listaProductos.Count();
                if (total == 0)
                {
                    return "";
                }
                double paginadores = Convert.ToDouble(total) / 6;
                double redondeado = Math.Round(paginadores, 0);
                if (paginadores > redondeado)
                {
                    redondeado += 1;
                }

                var cont = 1;
                var cont2 = 1;
                paginas += "<div class='row mt-5'><div class='col-md-12 text-center'><div class='site-block-27'><ul>";
                for (var i = 1; i <= redondeado; i++)
                {
                    var activo = "";
                    if (i == 1)
                    {
                        activo = "active";
                    }
                    paginas += "<li class='" + activo + " paginadores' onclick='paginar(\"#tabCategoria" + i + "\",this)' style='cursor:pointer'><span>" + i + "</span></li>";
                }
                paginas += "</ul></div></div></div>";

                foreach (var item in listaProductos)
                {
                    var img = db.imagenes_producto.Where(x => x.idProducto == item.idProducto).Select(x => x.referencia).FirstOrDefault();
                    var oferta = db.oferta.Where(x => x.fecha_fin >= hoy && x.activo == true && x.idProducto == item.idProducto).FirstOrDefault();
                    var tira = "";
                    var mostrarPrecio = "<p class='price'>$" + item.pvp + "</p>";
                    var porcen = "";
                    if (oferta != null)
                    {
                        tira = "<span class='onsale'>Oferta</span>";
                        double des = 1 - (Convert.ToDouble(oferta.porcentaje) / 100);
                        var precioFinal = des * Convert.ToDouble(item.pvp);
                        porcen = "<strong class='text-primary font-weight-bold pl-2'>-" + oferta.porcentaje + "% </strong>";
                        mostrarPrecio = "<p class='price'><del class='text-danger'>$" + item.pvp + "</del> &mdash; $" + Math.Round(precioFinal, 2) + porcen + "</p>";

                    }
                    if (cont == 1)
                    {
                        html += "<div class='active row tabCategoria' id='tabCategoria" + cont2 + "'>";
                    }



                    html += "<div class='col-sm-6 col-lg-4 text-center item mb-4 item-v2 ' style='min-height:525px;max-height:525px;border:1px solid #4ECDC4'>" +
                    tira +
                    "<a href='../../Tienda/Producto/?id=" + item.idProducto + "' > <img src='../../Imagenes/" + img + "' alt='Image' class='pb-1 pt-2 ml-auto mr-auto' style='max-height:300px;max-width:300px;min-height:300px;min-width:300px'></a>" +
                    "<div class='w-100' style='min-height:104px;margin: 0 auto;'>" +
                    "<h3 class='text-dark'><a href='../../Tienda/Producto/?id=" + item.idProducto + "'>" + item.nombre + "</a></h3>" +
                    "</div>" +
                    "<p><small class='text-dark'><strong class='text-primary'>STOCK: </strong>" + item.stock + "</small></p>" +
                    mostrarPrecio +
                    "</div>";
                    if (cont % 6 == 0 && cont < listaProductos.Count())
                    {
                        cont2 += 1;
                        html += "</div>";
                        html += "<div class='row tabCategoria' id='tabCategoria" + cont2 + "' hidden>";
                    }

                    if (cont == listaProductos.Count())
                    {
                        html += "</div>";
                    }
                    cont++;
                }




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return html + paginas;
        }


        public ActionResult ofertaProducto(int id)
        {
            oferta ca = new oferta();
            var hoy = DateTime.Now;
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                ca = db.oferta.Where(x => x.fecha_fin >= hoy && x.activo == true && x.idProducto == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            return Json(ca, JsonRequestBehavior.AllowGet);
        }

        public string sliderImagenesProducto(int id)
        {
            string html = "";
            try
            {
                var listaImagenes = db.imagenes_producto.Where(x => x.idProducto == id).ToList();
                var cantImagenes = listaImagenes.Count();
                if (cantImagenes == 0)
                {
                    return "";
                }

                html += "<div class='carousel-inner'>";
                var cont = 0;
                foreach (var item in listaImagenes)
                {
                    var activo = "";
                    var idAuxiliar = "";
                    if (cont == 0)
                    {
                        activo = " active ";
                        idAuxiliar = " id='imagenPrincipalReferencia' ";
                        
                    }

                    html += "<div class='carousel-item "+activo+"'>"+
                            "<img src='../../Imagenes/"+item.referencia+"' "+idAuxiliar+" class='d-block w-100 ml-auto mr-auto m-3 rounded shadow'"+
                            "style='max-height: 42em;min-height: 42em;max-width: 400px;' alt='Imagen'>"+
                           "</div>";
                    cont++;
                }
                html += "</div>";
                var indicators = "";
                if (cantImagenes > 1)
                {
                     indicators= "<a class='carousel-control-prev' href='#carouselExampleIndicators' role='button' data-slide='prev'>" +
                        "<span class='fa fa-chevron-circle-left fa-2x text-primary' aria-hidden='true'></span>" +
                        "<span class='sr-only'>Anterior</span>" +
                        "</a>" +
                        "<a class='carousel-control-next' href='#carouselExampleIndicators' role='button' data-slide='next'>" +
                        "<span class='fa fa-chevron-circle-right fa-2x text-primary' aria-hidden='true'></span>" +
                        "<span class='sr-only'>Siguiente</span>" +
                        "</a>";
                }

                html += indicators;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            return html;
        }



    }
}