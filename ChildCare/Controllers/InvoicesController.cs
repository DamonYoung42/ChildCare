﻿using System;
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
        [Authorize(Roles = "Admin")]
        public ActionResult AdminFunctions()
        {
            //ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }
        [Authorize]
        // GET: Invoices
        public ActionResult Index()
        {
            if (User.IsInRole("Parent"))
            {
                var userId = User.Identity.GetUserId();
                var invoices = db.Invoices
                    .Include(i => i.ApplicationUser)
                    .Where(x => x.UserId == userId)
                    .OrderBy(x => x.DateDue);
                return View(invoices.ToList());
            }
            else
            {
                var invoices = db.Invoices
                    .Include(i => i.ApplicationUser)
                    .OrderBy(x => x.ApplicationUser.LastName)
                    .ThenBy(x => x.DateDue);
                return View(invoices.ToList());
            }

        }

        public JsonResult GetInvoices(string month, int year)
        {
            var invoices = db.Invoices
                .Include(a => a.ApplicationUser)
                .Where(x => x.Month == month)
                .Where(x => x.Year == year)
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ThenBy(a => a.ApplicationUser.LastName)
                .ThenBy(a => a.ApplicationUser.FirstName)
                .ToList();

            return Json(invoices, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnpaidInvoices(string month, int year)
        {
            var invoices = db.Invoices
                .Include(a => a.ApplicationUser)
                .Where(x => x.Month == month)
                .Where(x => x.Year == year)
                .Where(x => x.AmountPaid != x.TotalAmount)
                .OrderBy(x => x.ApplicationUser.LastName)
                .ThenBy(x => x.ApplicationUser.FirstName)
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
            //Invoice invoice = db.Invoices.Find(id);
            Invoice invoice = db.Invoices.Include(y => y.ApplicationUser).First(y => y.Id == id);
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
        public ActionResult Create([Bind(Include = "Id,Month,Year,TotalAmount, BilledAmount,DateDue,AmountPaid,DatePaid,UserId, Notes, Adjustments")] Invoice invoice)
        {

            if (ModelState.IsValid)
            {
               
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("AdminFunctions", "Invoices");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", invoice.UserId);
            //return View(invoice);
            return RedirectToAction("AdminFunctions", "Invoices");
        }

        [Authorize(Roles = "Admin")]
        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Invoice invoice = db.Invoices.Find(id);
            Invoice invoice = db.Invoices.Include(y => y.ApplicationUser).First(y => y.Id == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", invoice.UserId);
            return View(invoice);
        }

        public JsonResult TaxStatement(int year)
        {
            var statements = db.Invoices
                .Include(x => x.ApplicationUser)
                .Where(x => x.DatePaid.Year == year)
                .Select(x => new { UserId = x.ApplicationUser.Id, AmountPaid = x.AmountPaid, Email = x.ApplicationUser.Email, FirstName = x.ApplicationUser.FirstName, LastName = x.ApplicationUser.LastName, Phone = x.ApplicationUser.PhoneNumber })
                .GroupBy(x => x.UserId)
                .Select(x => new
                {
                    ParentId = x.Key,
                    AmountPaid = x.Sum(y => y.AmountPaid)
                });

            return Json(statements, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "Admin")]
        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Month,Year,TotalAmount, BilledAmount,DateDue,AmountPaid,DatePaid,UserId, Notes, Adjustments")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.TotalAmount = invoice.BilledAmount + invoice.Adjustments;
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
            //Invoice invoice = db.Invoices.Find(id);
            Invoice invoice = db.Invoices.Include(y => y.ApplicationUser).First(y => y.Id == id);
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
