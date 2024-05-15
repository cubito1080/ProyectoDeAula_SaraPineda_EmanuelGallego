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
    public class tbAguasController : Controller
    {
        private EpmEntities db = new EpmEntities();

        // GET: tbAguas
        public ActionResult Index()
        {
            var tbAgua = db.tbAgua.Include(t => t.tbCliente);
            return View(tbAgua.ToList());
        }

        // GET: tbAguas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAgua tbAgua = db.tbAgua.Find(id);
            if (tbAgua == null)
            {
                return HttpNotFound();
            }
            return View(tbAgua);
        }

        // GET: tbAguas/Create
        public ActionResult Create(int? id)
        {
            ViewBag.IdCliente = id;
            return View();
        }

        // POST: tbAguas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAgua, IdCliente, PromedioConsumo, ConsumoActual, PeriodoConsumo")] tbAgua tbAgua)
        {
            if (ModelState.IsValid)
            {
                db.tbAgua.Add(tbAgua);
                db.SaveChanges();
                return RedirectToAction("Create", "tbEnergias", new { id = tbAgua.IdCliente });
            }
            if (tbAgua != null)
            {
                ViewBag.IdCliente = tbAgua.IdCliente;
            }
            else
            {
                ViewBag.IdCliente = null;
            }

            return View(tbAgua);
        }


        // GET: tbAguas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAgua tbAgua = db.tbAgua.Find(id);
            if (tbAgua == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.tbCliente, "IdCliente", "Cedula", tbAgua.IdCliente);
            return View(tbAgua);
        }

        // POST: tbAguas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAgua,IdCliente,PromedioConsumo,ConsumoActual,PeriodoConsumo")] tbAgua tbAgua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAgua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.tbCliente, "IdCliente", "Cedula", tbAgua.IdCliente);
            return View(tbAgua);
        }

        // GET: tbAguas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAgua tbAgua = db.tbAgua.Find(id);
            if (tbAgua == null)
            {
                return HttpNotFound();
            }
            return View(tbAgua);
        }

        // POST: tbAguas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAgua tbAgua = db.tbAgua.Find(id);
            db.tbAgua.Remove(tbAgua);
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
