using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChildCare.Models;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace ChildCare.Controllers
{
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult GenerateInvoices()
        {
            //ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // GET: Invoices
        public ActionResult Index()
        {
            if (User.IsInRole("Parent"))
            {
                var userId = User.Identity.GetUserId();
                var invoices = db.Invoices.Include(i => i.ApplicationUser).Where(x => x.UserId == userId);
                return View(invoices.ToList());
            }
            else
            {
                var invoices = db.Invoices.Include(i => i.ApplicationUser).OrderBy(x => x.ApplicationUser.LastName);
                return View(invoices.ToList());
            }

        }

        public JsonResult GetInvoices(string month, int year)
        {
            var invoices = db.Invoices
                .Include(a => a.ApplicationUser)
                .Where(x => x.Month == month)
                .Where(x => x.Year == year)
                .ToList();

            return Json(invoices, JsonRequestBehavior.AllowGet);
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        [Authorize(Roles = "Admin")]
        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");

            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem()
            {
                Text = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[x - 1] + " (" + x + ")",
                Value = x.ToString()
            }), "Value", "Text");

            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Now.Year, 10).Select(x => new SelectListItem()
            {
                Text = x.ToString(),
                Value = x.ToString()
            }), "Value", "Text");

            //ViewBag.Years = new SelectList(years.Select(y => new SelectListItem()
            //{
            //    Text = y.ToString(),
            //    Value = y.ToString()
            //}));
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.p
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Month,Year,AmountDue,DateDue,AmountPaid,DatePaid,UserId, Notes, Adjustments")] Invoice invoice)
        {

            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("GenerateInvoices", "Invoices");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", invoice.UserId);
            //return View(invoice);
            return RedirectToAction("GenerateInvoices", "Invoices");
        }

        [Authorize(Roles = "Admin")]
        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", invoice.UserId);
            return View(invoice);
        }
        [Authorize(Roles = "Admin")]
        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Month,Year,AmountDue,DateDue,AmountPaid,DatePaid,UserId, Notes, Adjustments")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.AmountDue += invoice.Adjustments;
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", invoice.UserId);
            return View(invoice);
        }

        [Authorize(Roles = "Admin")]
        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        [Authorize(Roles = "Admin")]
        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
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
