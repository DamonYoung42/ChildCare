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
    public class PickupPersonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PickupPersons
        [Authorize]
        public ActionResult Index()
        {

            //var pickupPersons = db.PickupPersons.Include(p => p.Child);
            if (User.IsInRole("Parent"))
            {
                var userId = User.Identity.GetUserId();
                var drivers = db.PickupPersons.Include(a => a.Child).Where(c => c.Child.UserId == userId).OrderBy(c => c.LastName).OrderBy(z => z.FirstName);
                return View(drivers.ToList());
            }
            else
            {
                var drivers = db.PickupPersons.Include(a => a.Child).OrderBy(y => y.LastName).OrderBy(z => z.FirstName);
                return View(drivers.ToList());
            }

        }

        // GET: PickupPersons/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupPerson pickupPerson = db.PickupPersons.Find(id);
            if (pickupPerson == null)
            {
                return HttpNotFound();
            }
            return View(pickupPerson);
        }

        // GET: PickupPersons/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ChildId = new SelectList(db.Children, "Id", "FirstName");
            return View();
        }

        // POST: PickupPersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Photo,ChildId,PhoneNumber,EmailAddress")] PickupPerson pickupPerson, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {

                if (photo != null && photo.ContentLength > 0)
                {
                    pickupPerson.Photo = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(photo.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Images/PickupPerson"), pickupPerson.Photo);
                    photo.SaveAs(path);
                }
                else
                {
                    pickupPerson.Photo = "no_photo.jpg";
                }

                db.PickupPersons.Add(pickupPerson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChildId = new SelectList(db.Children, "Id", "FirstName", pickupPerson.ChildId);
            return View(pickupPerson);
        }

        // GET: PickupPersons/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupPerson pickupPerson = db.PickupPersons.Find(id);
            if (pickupPerson == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChildId = new SelectList(db.Children, "Id", "FirstName", pickupPerson.ChildId);
            return View(pickupPerson);
        }

        // POST: PickupPersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Photo,ChildId,PhoneNumber,EmailAddress")] PickupPerson pickupPerson, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    pickupPerson.Photo = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(photo.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Images/PickupPerson"), pickupPerson.Photo);
                    photo.SaveAs(path);
                }
                db.Entry(pickupPerson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChildId = new SelectList(db.Children, "Id", "FirstName", pickupPerson.ChildId);
            return View(pickupPerson);
        }

        // GET: PickupPersons/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupPerson pickupPerson = db.PickupPersons.Find(id);
            if (pickupPerson == null)
            {
                return HttpNotFound();
            }
            return View(pickupPerson);
        }

        // POST: PickupPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            PickupPerson pickupPerson = db.PickupPersons.Find(id);
            db.PickupPersons.Remove(pickupPerson);
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
