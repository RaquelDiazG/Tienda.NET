using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaOnline;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    public class OrdersController : Controller
    {
        private tiendaOnlineEntities db = new tiendaOnlineEntities();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/CreateWithProducts
        public ActionResult CreateWithProducts(CarritoCompra cc)
        {
            //Crear pedido
            Order pedido = new Order();
            //Guardar pedido
            db.Orders.Add(pedido);
            db.SaveChanges();
            //Añadir productos del carrito a la BBDD de pedidos
            Order pedido_bd = db.Orders.Find(pedido.Id);
            foreach (Product producto in cc)
            {
                Product producto_bd = db.Products.Find(producto.Id);
                //Comprobar disponiblidad
                if (producto_bd.Cantidad >= 1)
                {
                    OrderDetails details = new OrderDetails();
                    details.Order = pedido_bd;
                    details.Product = producto_bd;
                    details.Cantidad = producto.Cantidad;
                    db.OrderDetails.Add(details);
                    //Disminuir stock productos
                    producto_bd.Cantidad--;
                }
                else
                {
                    //Vaciar carrito
                    cc = new CarritoCompra();
                    //Redirigir a la vista de error
                    return RedirectToAction("ErrorBuy");
                }
            }

            //Actualizar pedido
            db.SaveChanges();
            //Vaciar carrito
            cc = new CarritoCompra();
            //Volver
            return RedirectToAction("Index", "Products");
        }


        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ErrorBuy()
        {
            return View();
        }
    }
}
