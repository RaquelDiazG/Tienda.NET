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
            cc.Add(p);
            //Redirigimos a la vista
            return RedirectToAction("Index", "Products");
        }

        public ActionResult DeleteProductCart(CarritoCompra cc, int id)
        {
            //Buscamos el producto
            Product p = db.Products.Find(id);
            //Añadimos el producto al carrito
            cc.Remove(p);
            //Redirigimos a la vista
            return RedirectToAction("Index");
        }

        public ActionResult Buy(CarritoCompra cc)
        {
            //Crear pedido
            Order pedido = new Order();
            //Añadir productos del carrito a la BBDD de pedidos
            foreach (Product p in cc)
            {
                pedido.Id = 0;
                db.Orders.Add(pedido);
                db.SaveChanges();
            }
            //Vaciar carrito
            cc = new CarritoCompra();
            //Volver
            return RedirectToAction("Index", "Products");
        }

        public ActionResult Back()
        {
            return RedirectToAction("Index", "Products");
        }
    }
}