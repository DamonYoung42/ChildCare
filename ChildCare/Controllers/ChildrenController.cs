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
using System.IO;
using static System.Net.WebRequestMethods;

namespace ChildCare.Controllers
{
    public class ChildrenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Children
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            if (User.IsInRole("Parent"))
            {
                var children = db.Children.Include(a => a.Teacher).Where(c => c.UserId == userId);
                return View(children.ToList());
            }
            else
            {
                var children = db.Children.Include(a => a.Teacher).OrderBy(y => y.LastName);
                //var children = db.Children.Include(a => a.Teacher)
                //    .Join(db.Users, b => b.UserId, c => c.Id, (b, c) => new { b, c.Email, c.PhoneNumber })
                //    .OrderBy(d => d.b.LastName)
                //    .OrderBy(z => z.b.FirstName);

                //var students = new List<Child>();
                //foreach (var child  in children)
                //{
                //    students.Add(new Child()
                //    {
                //        FirstName = child.b.FirstName,
                //        LastName = child.b.LastName,
                //        GradeLevel = child.b.GradeLevel,
                //        Medications = child.b.Medications,
                //        Notes = child.b.Notes,
                //        Id = child.b.Id,
                //        UserId = child.b.UserId,
                //        Teacher = child.b.Teacher,
                //        Photo = child.b.Photo,
                //        Email = child.Email,
                //        Phone = child.PhoneNumber

                //    });
                //}

                return View(children.ToList());
            }


        }

        // GET: Children/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Include(a => a.Teacher).Where(c => c.Id == id).First();
            //Child child = db.Children.Find(id).;
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        public JsonResult GetFutureCalendarEvents(int Id)
        {
            //INITIAL WORKING VERSION
            //var events = db.Attendances.Where(x => x.ChildId == Id).ToArray();
            //return Json(events, JsonRequestBehavior.AllowGet);

            var events = db.Attendances
                .Where(x => x.ChildId == Id)
                .Where(x => x.PickupTime >= DateTime.Now);

            return Json(events.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPastCalendarEvents(int Id)
        {
            //INITIAL WORKING VERSION
            //var events = db.Attendances.Where(x => x.ChildId == Id).ToArray();
            //return Json(events, JsonRequestBehavior.AllowGet);

            var events = db.Attendances
                .Where(x => x.ChildId == Id)
                .Where(x => x.PickupTime < DateTime.Now);

            return Json(events.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Children/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.TeacherId = new SelectList(db.Teachers.OrderBy(x => x.LastName), "Id", "LastName");
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Photo,UserId,Medications,Notes, TeacherId")] Child child, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {

                if (photo != null && photo.ContentLength > 0)
                {
                    child.Photo = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(photo.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Images/Child"), child.Photo);
                    photo.SaveAs(path);
                }
                else
                {
                    child.Photo = "no_photo.jpg";
                }
                //child.GradeLevel = child.Teacher.GradeLevel;
                child.UserId = User.Identity.GetUserId();
                db.Children.Add(child);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", child.UserId);
            ViewBag.TeacherID = new SelectList(db.Teachers.OrderBy(x => x.LastName), "Id", "LastName", child.TeacherId);
            return View(child);
        }

        // GET: Children/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", child.UserId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "LastName", child.TeacherId);
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Photo,UserId,Medications,Notes,TeacherId")] Child child, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    child.Photo = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(photo.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Images/Child"), child.Photo);
                    photo.SaveAs(path);
                }


                db.Entry(child).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "LastName", child.TeacherId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", child.UserId);
            return View(child);
        }

        // GET: Children/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Include(a => a.Teacher).Where(c => c.Id == id).First();
            //Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Child child = db.Children.Find(id);
            db.Children.Remove(child);
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
