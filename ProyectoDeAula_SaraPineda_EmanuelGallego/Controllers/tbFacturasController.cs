using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoDeAula_SaraPineda_EmanuelGallego.Models;

namespace ProyectoDeAula_SaraPineda_EmanuelGallego.Controllers
{
    public class tbFacturasController : Controller
    {
        private EpmEntities db = new EpmEntities();

        // GET: tbFacturas
        public ActionResult Index()
        {
            var tbFactura = db.tbFactura.Include(t => t.tbAgua).Include(t => t.tbEnergia).Include(t => t.tbPrecios);
            return View(tbFactura.ToList());
        }

        // GET: tbFacturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }

        // GET: tbFacturas/Create
        public ActionResult Create()
        {
            ViewBag.IdAgua = new SelectList(db.tbAgua, "IdAgua", "IdAgua");
            ViewBag.IdEnergia = new SelectList(db.tbEnergia, "IdEnergia", "IdEnergia");
            ViewBag.IdPrecios = new SelectList(db.tbPrecios, "IdPrecios", "Servicio");
            return View();
        }

        // POST: tbFacturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFactura,IdAgua,IdEnergia,IdPrecios,PagoAgua,PagoEnergia,PagoTotal")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                db.tbFactura.Add(tbFactura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAgua = new SelectList(db.tbAgua, "IdAgua", "IdAgua", tbFactura.IdAgua);
            ViewBag.IdEnergia = new SelectList(db.tbEnergia, "IdEnergia", "IdEnergia", tbFactura.IdEnergia);
            ViewBag.IdPrecios = new SelectList(db.tbPrecios, "IdPrecios", "Servicio", tbFactura.IdPrecios);
            return View(tbFactura);
        }

        // GET: tbFacturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAgua = new SelectList(db.tbAgua, "IdAgua", "IdAgua", tbFactura.IdAgua);
            ViewBag.IdEnergia = new SelectList(db.tbEnergia, "IdEnergia", "IdEnergia", tbFactura.IdEnergia);
            ViewBag.IdPrecios = new SelectList(db.tbPrecios, "IdPrecios", "Servicio", tbFactura.IdPrecios);
            return View(tbFactura);
        }

        // POST: tbFacturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFactura,IdAgua,IdEnergia,IdPrecios,PagoAgua,PagoEnergia,PagoTotal")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbFactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAgua = new SelectList(db.tbAgua, "IdAgua", "IdAgua", tbFactura.IdAgua);
            ViewBag.IdEnergia = new SelectList(db.tbEnergia, "IdEnergia", "IdEnergia", tbFactura.IdEnergia);
            ViewBag.IdPrecios = new SelectList(db.tbPrecios, "IdPrecios", "Servicio", tbFactura.IdPrecios);
            return View(tbFactura);
        }

        // GET: tbFacturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }

        // POST: tbFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbFactura tbFactura = db.tbFactura.Find(id);
            db.tbFactura.Remove(tbFactura);
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

        [HttpGet]
        public ActionResult BuscarFacturas()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarFacturas(string cedula)
        {
            // Buscar el cliente con la cédula proporcionada
            var cliente = db.tbCliente.FirstOrDefault(c => c.Cedula == cedula);

            if (cliente != null)
            {
                // Si el cliente existe, buscar las facturas asociadas a ese cliente
                var facturas = db.tbFactura.Where(f => f.tbAgua.IdCliente == cliente.IdCliente || f.tbEnergia.IdCliente == cliente.IdCliente).ToList();

                // Almacenar las facturas en TempData
                TempData["Facturas"] = facturas;

                return RedirectToAction("DetallesFacturas");
            }
            else
            {
                // Si el cliente no existe, agregar un mensaje de error al ModelState
                ModelState.AddModelError("Cedula", "No hay una persona con dicha cédula.");
            }

            return View();
        }

        [HttpGet]
        public ActionResult DetallesFacturas()
        {
            // Recuperar las facturas de TempData
            var facturas = TempData["Facturas"] as List<tbFactura>;

            return View(facturas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetallesFacturas(int id)
        {
            // Buscar los gastos de agua y energía del cliente
            var gastosAgua = db.tbAgua.Where(a => a.IdCliente == id).ToList();
            var gastosEnergia = db.tbEnergia.Where(e => e.IdCliente == id).ToList();

            // Buscar los precios de los servicios de agua y energía
            var precioAgua = db.tbPrecios.FirstOrDefault(p => p.Servicio == "agua").Precio;
            var precioEnergia = db.tbPrecios.FirstOrDefault(p => p.Servicio == "energia").Precio;

            // Crear una nueva factura para cada par de gastos de agua y energía
            for (int i = 0; i < gastosAgua.Count && i < gastosEnergia.Count; i++)
            {
                tbFactura factura = new tbFactura
                {
                    IdAgua = gastosAgua[i].IdAgua,
                    IdEnergia = gastosEnergia[i].IdEnergia,
                    PagoAgua = (decimal)(gastosAgua[i].ConsumoActual * (double)precioAgua), // Convertir el precio a double antes de la multiplicación
                    PagoEnergia = (decimal)(gastosEnergia[i].ConsumoActual * (double)precioEnergia), // Convertir el precio a double antes de la multiplicación
                    PagoTotal = (decimal)((gastosAgua[i].ConsumoActual * (double)precioAgua) + (gastosEnergia[i].ConsumoActual * (double)precioEnergia))
                };

                db.tbFactura.Add(factura);
            }

            db.SaveChanges();

            // Buscar las facturas asociadas al cliente
            var facturas = db.tbFactura.Where(f => f.tbAgua.IdCliente == id || f.tbEnergia.IdCliente == id).ToList();


            // Pasar las facturas a la vista
            return View(facturas);
        }




    }
}
