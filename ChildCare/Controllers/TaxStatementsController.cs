using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChildCare.Models;
using Microsoft.AspNet.Identity;

namespace ChildCare.Controllers
{
    public class TaxStatementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaxStatements
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            if (User.IsInRole("Parent"))
            {
                var taxStatements = db.TaxStatements
                    .Include(t => t.ApplicationUser)
                    .Where(t => t.UserId == userId)
                    .OrderBy(t => t.Year);
                return View(taxStatements.ToList());
            }
            else
            {
                var taxStatements = db.TaxStatements
                    .Include(t => t.ApplicationUser)
                    .OrderBy(t => t.Year)
                    .OrderBy(t => t.ApplicationUser.LastName);
                return View(taxStatements.ToList());
            }

        }

        public JsonResult GetTaxStatements(int year)
        {
            var taxStatements = db.TaxStatements.Include(t => t.ApplicationUser)
                .Where(t => t.Year == year)
                .ToList();

            return Json(taxStatements, JsonRequestBehavior.AllowGet);

        }

        // GET: TaxStatements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxStatement taxStatement = db.TaxStatements.Find(id);
            if (taxStatement == null)
            {
                return HttpNotFound();
            }
            return View(taxStatement);
        }

        // GET: TaxStatements/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: TaxStatements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Year,AmountPaid,UserId")] TaxStatement taxStatement)
        {
            if (ModelState.IsValid)
            {
                db.TaxStatements.Add(taxStatement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", taxStatement.UserId);
            return View(taxStatement);
        }

        // GET: TaxStatements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxStatement taxStatement = db.TaxStatements.Find(id);
            if (taxStatement == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", taxStatement.UserId);
            return View(taxStatement);
        }

        // POST: TaxStatements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Year,AmountPaid,UserId")] TaxStatement taxStatement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxStatement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", taxStatement.UserId);
            return View(taxStatement);
        }

        // GET: TaxStatements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxStatement taxStatement = db.TaxStatements.Find(id);
            if (taxStatement == null)
            {
                return HttpNotFound();
            }
            return View(taxStatement);
        }

        // POST: TaxStatements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxStatement taxStatement = db.TaxStatements.Find(id);
            db.TaxStatements.Remove(taxStatement);
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
