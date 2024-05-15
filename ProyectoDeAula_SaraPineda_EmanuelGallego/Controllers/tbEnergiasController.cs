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
    public class tbEnergiasController : Controller
    {
        private EpmEntities db = new EpmEntities();

        // GET: tbEnergias
        public ActionResult Index()
        {
            var tbEnergia = db.tbEnergia.Include(t => t.tbCliente);
            return View(tbEnergia.ToList());
        }

        // GET: tbEnergias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEnergia tbEnergia = db.tbEnergia.Find(id);
            if (tbEnergia == null)
            {
                return HttpNotFound();
            }
            return View(tbEnergia);
        }

        // GET: tbEnergias/Create
        public ActionResult Create(int? id)
        {
            ViewBag.IdCliente = id;
            return View();
        }

        // POST: tbEnergias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEnergia,IdCliente,MetaAhorro,ConsumoActual,PeriodoConsumo")] tbEnergia tbEnergia)
        {
            if (ModelState.IsValid)
            {
                db.tbEnergia.Add(tbEnergia);
                db.SaveChanges();
                return RedirectToAction("Index", "tbClientes", new { id = tbEnergia.IdCliente });
            }

            ViewBag.IdCliente = new SelectList(db.tbCliente, "IdCliente", "Cedula", tbEnergia.IdCliente);
            return View(tbEnergia);
        }

        // GET: tbEnergias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEnergia tbEnergia = db.tbEnergia.Find(id);
            if (tbEnergia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.tbCliente, "IdCliente", "Cedula", tbEnergia.IdCliente);
            return View(tbEnergia);
        }

        // POST: tbEnergias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEnergia,IdCliente,MetaAhorro,ConsumoActual,PeriodoConsumo")] tbEnergia tbEnergia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEnergia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.tbCliente, "IdCliente", "Cedula", tbEnergia.IdCliente);
            return View(tbEnergia);
        }

        // GET: tbEnergias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEnergia tbEnergia = db.tbEnergia.Find(id);
            if (tbEnergia == null)
            {
                return HttpNotFound();
            }
            return View(tbEnergia);
        }

        // POST: tbEnergias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEnergia tbEnergia = db.tbEnergia.Find(id);
            db.tbEnergia.Remove(tbEnergia);
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
