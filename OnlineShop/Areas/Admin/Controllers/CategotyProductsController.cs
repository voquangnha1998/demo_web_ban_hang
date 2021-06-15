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
    public class CategotyProductsController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/CategotyProducts
        public ActionResult Index()
        {
            return View(db.CategotyProducts.ToList());
        }

        // GET: Admin/CategotyProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategotyProduct categotyProduct = db.CategotyProducts.Find(id);
            if (categotyProduct == null)
            {
                return HttpNotFound();
            }
            return View(categotyProduct);
        }

        // GET: Admin/CategotyProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategotyProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] CategotyProduct categotyProduct)
        {
            if (ModelState.IsValid)
            {
                db.CategotyProducts.Add(categotyProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categotyProduct);
        }

        // GET: Admin/CategotyProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategotyProduct categotyProduct = db.CategotyProducts.Find(id);
            if (categotyProduct == null)
            {
                return HttpNotFound();
            }
            return View(categotyProduct);
        }

        // POST: Admin/CategotyProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] CategotyProduct categotyProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categotyProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categotyProduct);
        }

        // GET: Admin/CategotyProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategotyProduct categotyProduct = db.CategotyProducts.Find(id);
            if (categotyProduct == null)
            {
                return HttpNotFound();
            }
            return View(categotyProduct);
        }

        // POST: Admin/CategotyProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategotyProduct categotyProduct = db.CategotyProducts.Find(id);
            db.CategotyProducts.Remove(categotyProduct);
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
