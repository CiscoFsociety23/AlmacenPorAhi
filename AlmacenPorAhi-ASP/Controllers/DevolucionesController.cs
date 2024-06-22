using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlmacenPorAhiASP.Models;
using AlmacenPorAhi_ASP.Models;

namespace AlmacenPorAhi_ASP.Controllers
{
    public class DevolucionesController : Controller
    {
        private AlmacenesDbContext db = new AlmacenesDbContext();

        // GET: Devoluciones
        public ActionResult Index()
        {
            var devoluciones = db.Devoluciones.Include(d => d.Venta);
            return View(devoluciones.ToList());
        }

        // GET: Devoluciones/Create
        public ActionResult Create()
        {
            ViewBag.VentaId = new SelectList(db.Ventas, "Id", "Id");
            return View();
        }

        public JsonResult ObtenerDatosVenta(int ventaId)
        {
            var venta = db.Ventas.Include("Cliente").Include("Producto").FirstOrDefault(v => v.Id == ventaId);
            if (venta != null)
            {
                return Json(new
                {
                    NombreCliente = venta.Cliente.Nombre,
                    NombreProducto = venta.Producto.Nombre,
                    TotalDevolucion = venta.Total
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        // POST: Devoluciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VentaId,NombreCliente,NombreProducto,Fecha,TotalDevolucion,Estado")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                // Cargar los detalles de la venta seleccionada
                var venta = db.Ventas.Include("Cliente").Include("Producto").FirstOrDefault(v => v.Id == devolucion.VentaId);
                if (venta != null)
                {
                    devolucion.NombreCliente = venta.Cliente.Nombre;
                    devolucion.NombreProducto = venta.Producto.Nombre;
                    devolucion.TotalDevolucion = venta.Total;

                    db.Devoluciones.Add(devolucion);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.VentaId = new SelectList(db.Ventas, "Id", "Id", devolucion.VentaId);
            return View(devolucion);
        }

        // GET: Devoluciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devoluciones.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            ViewBag.VentaId = new SelectList(db.Ventas, "Id", "Id", devolucion.VentaId);
            return View(devolucion);
        }

        // POST: Devoluciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VentaId,NombreCliente,NombreProducto,Fecha,TotalDevolucion,Estado")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(devolucion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VentaId = new SelectList(db.Ventas, "Id", "Id", devolucion.VentaId);
            return View(devolucion);
        }

        // GET: Devoluciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devoluciones.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            return View(devolucion);
        }

        // POST: Devoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Devolucion devolucion = db.Devoluciones.Find(id);
            db.Devoluciones.Remove(devolucion);
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
