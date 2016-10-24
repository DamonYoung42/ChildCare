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
    public class AttendancesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Index()
        {
            var today = DateTime.Now.Date;
            var attendances = db.Attendances
                .Include(a => a.Child)
                .Include(c => c.Child.Teacher)
                .OrderBy(x => x.Child.LastName)
                .ThenBy(x => x.Child.FirstName)
                .Where(x => x.Date == today);

            return View(attendances.ToList());
        }
        // GET: Attendances
        //public ActionResult Index(int Id)
        //{
        //    var attendances = db.Attendances.Include(a => a.Child).Where(x => x.ChildId == Id).OrderBy(x => x.Date);
        //    return View(attendances.ToList());
        //}

        public JsonResult Invoice(int month, int year)
        {

            var attendances = db.Attendances
                .Where(x => x.Date.Month == month)
                .Where(x => x.Date.Year == year)
                .Select(x => new { AmountBilled = x.AmountBilled, ChildId = x.ChildId, ParentId = x.Child.UserId, FirstName = x.Child.ApplicationUser.FirstName, LastName = x.Child.ApplicationUser.LastName, Email = x.Child.ApplicationUser.Email, Phone = x.Child.ApplicationUser.PhoneNumber })
                .GroupBy(x => x.ParentId)
              
                .Select(x => new
                 {
                     ParentId = x.Key,
                     AmountDue = x.Sum(y => y.AmountBilled)
                 }

                ).ToList();

            return Json(attendances, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCurrentBalance(int month, int year)
        {
            var userId = User.Identity.GetUserId();
            var attendances = db.Attendances
                .Where(x => x.Child.UserId == userId)
                .Where(x => x.Date.Month == month)
                .Where(x => x.Date.Year == year)
                .Select(x => new { AmountBilled = x.AmountBilled, ChildId = x.ChildId, ParentId = x.Child.UserId, FirstName = x.Child.ApplicationUser.FirstName, LastName = x.Child.ApplicationUser.LastName, Email = x.Child.ApplicationUser.Email, Phone = x.Child.ApplicationUser.PhoneNumber })
                .GroupBy(x => x.ParentId)
                .Select(x => new
                {
                    ParentId = x.Key,
                    AmountDue = x.Sum(y => y.AmountBilled)
                }

                ).ToList();

            return Json(attendances, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PickupToday(int Id)
        {
            DateTime today = DateTime.Today;
            var attendanceRecord = db.Attendances.Include(a => a.Child)
                .Join(db.Users, b => b.Child.UserId, c => c.Id, (b, c) => new { b, c.Email, c.PhoneNumber, c.FirstName })
                .Where(x => x.b.ChildId == Id && x.b.Date == today)
                .ToList();

            //var attendanceRecord = db.Attendances.Include(a => a.Child)
            //    .Where(x => x.ChildId == Id && x.Date == today)
            //    .ToList();
            return Json(attendanceRecord, JsonRequestBehavior.AllowGet);
        }



        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create()
        {
            ViewBag.ChildId = new SelectList(db.Children, "Id", "FirstName");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,PickupTime,ChildId, start, end, editable, allDay, title, AmountBilled")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                //return View(attendance);
                //return RedirectToAction("Details", "Children", new { Id = attendance.ChildId });
            }

            ViewBag.ChildId = new SelectList(db.Children, "Id", "FirstName", attendance.ChildId);
            //return View(attendance);
            return RedirectToAction("Details", "Children", new { Id = attendance.ChildId });
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChildId = new SelectList(db.Children, "Id", "FirstName", attendance.ChildId);
            return View(attendance);
        }

        

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,PickupTime,ChildId, Child, start, end, editable, allDay, title, AmountBilled")] Attendance attendance, string firstName, Boolean sendTextSMS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                if (sendTextSMS)
                {
                    TwilioSMS SMSMessage = new TwilioSMS();
                    var message = firstName + " was signed out of child care at " + attendance.PickupTime.ToShortTimeString() + ".You have been billed $" + attendance.AmountBilled + ".";
                    SMSMessage.SendSMS(message);
                }

                //return RedirectToAction("Details", "Children");
            }
            ViewBag.ChildId = new SelectList(db.Children, "Id", "FirstName", attendance.ChildId);
            //return View(attendance);
            return RedirectToAction("Index", "Attendances");
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
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
