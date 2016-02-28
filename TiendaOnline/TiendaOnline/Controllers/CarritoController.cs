using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    public class CarritoController : Controller
    {

        private tiendaOnlineEntities db = new tiendaOnlineEntities();

        public ActionResult Index(CarritoCompra cc)
        {
            return View(cc);
        }

        // GET: Carrito
        public ActionResult AddProductCart(CarritoCompra cc, int id)
        {
            //Buscamos el producto
            Product p = db.Products.Find(id);
            //Añadimos el producto al carrito
            Product producto_encontrado = cc.FirstOrDefault(x => x.Id == id);
            if (producto_encontrado!=null)
            {
                producto_encontrado.Cantidad++;
            }
            else {
                p.Cantidad = 1;
                cc.Add(p);
            }
            //Redirigimos a la vista
            return RedirectToAction("Index", "Products");
        }

        public ActionResult DeleteProductCart(CarritoCompra cc, int id)
        {
            //Buscamos el producto
            Product p = cc.FirstOrDefault(x => x.Id == id);
            //Eliminamos el producto del carrito
            if (p.Cantidad!=1)
            {
                p.Cantidad--;
            }
            else {
                cc.Remove(p);
            }
            //Redirigimos a la vista
            return RedirectToAction("Index");
        }

        public ActionResult Back()
        {
            return RedirectToAction("Index", "Products");
        }
    }
}