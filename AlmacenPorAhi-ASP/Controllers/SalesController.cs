using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlmacenPorAhiASP.Models;

namespace AlmacenPorAhi_ASP.Controllers
{
    public class SalesController : Controller
    {
        private AlmacenesDbContext db = new AlmacenesDbContext();

        // GET: Sales
        public ActionResult Index()
        {
            var ventas = db.Ventas.Include(v => v.Cliente).Include(v => v.Producto).Include(v => v.Usuario);
            return View(ventas.ToList());
        }

        // GET: Sales/GetProductPrice
        public JsonResult GetProductPrice(int productId)
        {
            var producto = db.Productos.Find(productId);
            if (producto != null)
            {
                return Json(producto.Precio, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        // GET: Sales/GetProductDetails
        public JsonResult GetProductDetails(int productId)
        {
            var producto = db.Productos.Find(productId);
            if (producto != null)
            {
                var data = new
                {
                    Precio = producto.Precio,
                    Stock = producto.Stock
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre");
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre");
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "FirstName");
            return View();
        }

        // POST: Sales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,ClienteId,ProductoId,UsuarioId,Cantidad,Total")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                var producto = db.Productos.Find(venta.ProductoId);
                if (producto != null && producto.Stock >= venta.Cantidad)
                {
                    producto.Stock -= venta.Cantidad;
                    db.Ventas.Add(venta);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Stock insuficiente para el producto seleccionado.");
                }
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", venta.ClienteId);
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre", venta.ProductoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "FirstName", venta.UsuarioId);
            return View(venta);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", venta.ClienteId);
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre", venta.ProductoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "FirstName", venta.UsuarioId);
            return View(venta);
        }

        // POST: Sales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,ClienteId,ProductoId,UsuarioId,Cantidad,Total")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", venta.ClienteId);
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre", venta.ProductoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "FirstName", venta.UsuarioId);
            return View(venta);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Ventas.Find(id);
            db.Ventas.Remove(venta);
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
    }
}
