using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaOnline.Models.Binders
{
    public class CarritoCompraModelBinder : IModelBinder
    {
        private const string SessionKey = "carrito";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CarritoCompra carrito = (controllerContext.HttpContext.Session != null) ? (controllerContext.HttpContext.Session[SessionKey] as CarritoCompra) : null;

            if (carrito == null)
            {
                carrito = new CarritoCompra();
                controllerContext.HttpContext.Session[SessionKey] = carrito;
            }

            return carrito;
        }
    }
}