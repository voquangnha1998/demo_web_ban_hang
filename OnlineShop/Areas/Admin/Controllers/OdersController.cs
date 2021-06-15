using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.EF;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OdersController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/Oders
        public ActionResult Index()
        {
            var oders = db.Oders.Include(o => o.User);
            return View(oders.ToList());
        }
        public ActionResult Duyet(int id)
        {
            Oder o = db.Oders.Where(d => d.ID == id).SingleOrDefault();
            o.Status = false;
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");



            /*o.Status = false;
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            //SetAlert("Cập nhật dùng thành công", "success");
            //ViewBag.msg = "Cập nhật người dùng thành công !!!!!";
            return RedirectToAction("Index");*/
        }
        // GET: Admin/Oders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oder oder = db.Oders.Find(id);
            if (oder == null)
            {
                return HttpNotFound();
            }
            return View(oder);
        }

        // GET: Admin/Oders/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        // POST: Admin/Oders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,ShipName,ShipPhone,ShipAddress,CreateDate,Status")] Oder oder)
        {
            if (ModelState.IsValid)
            {
                db.Oders.Add(oder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", oder.UserID);
            return View(oder);
        }

        // GET: Admin/Oders/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oder oder = db.Oders.Find(id);
            if (oder == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", oder.UserID);
            return View(oder);
        }

        // POST: Admin/Oders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,ShipName,ShipPhone,ShipAddress,CreateDate,Status")] Oder oder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", oder.UserID);
            return View(oder);
        }

        // GET: Admin/Oders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oder oder = db.Oders.Find(id);
            if (oder == null)
            {
                return HttpNotFound();
            }
            return View(oder);
        }

        // POST: Admin/Oders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oder oder = db.Oders.Find(id);
            db.Oders.Remove(oder);
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
