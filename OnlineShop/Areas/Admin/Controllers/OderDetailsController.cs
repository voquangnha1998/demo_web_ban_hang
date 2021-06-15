using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OderDetailsController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/OderDetails
        public ActionResult Index()
        {

            ViewBag.DemdonhangChuaDuyet = DemdonhangChuaDuyet();
            ViewBag.Demdonhang = Demdonhang();
            var odder = new OderDetailDao();
            var model = odder.ListOderDaDuyet();
            return View(model);
        }
        public ActionResult Indexchuaduyet()
        {

            ViewBag.DemdonhangChuaDuyet = DemdonhangChuaDuyet();
            ViewBag.Demdonhang = Demdonhang();
            var odder = new OderDetailDao();
            var model = odder.ListOderChuaDuyet();
            return View(model);

            /*var products = db.Products.Include(p => p.CategotyProduct);
            return View(products.ToList());*/
        }
        public int DemdonhangChuaDuyet()
        {
            int sl = db.Oders.Where(x=>x.Status==true).Count();
            return sl;
        }
        public int Demdonhang()
        {
            int sl = db.Oders.Where(x => x.Status == false).Count();
            return sl;
        }
        public ActionResult Duyet(int id)
        {
            Oder o = db.Oders.Where(d => d.ID == id).SingleOrDefault();
            o.Status = false;
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

           
        }
        // GET: Admin/OderDetails/Details/5
        public ActionResult Details(int orderId, int productId)
        {


            //var orderdatail = new OderDetailDao().ViewDetail(orderId, productId);
            //ViewBag.NewProducts = producdao.ListNewProduct(2);
            //ViewBag.Order = new OderDetailDao().ViewDetail(orderdatail.OderID, orderdatail.ProID);
            return View(db.OderDetails.Where(o => (o.OderID == orderId) && (o.ProID == productId)).FirstOrDefault());
        }

        // GET: Admin/OderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OderID = new SelectList(db.Oders, "ID", "ShipName");
            ViewBag.ProID = new SelectList(db.Products, "ID", "Name");
            return View();
        }

        // POST: Admin/OderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProID,OderID,Quantity,Price")] OderDetail oderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OderDetails.Add(oderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OderID = new SelectList(db.Oders, "ID", "ShipName", oderDetail.OderID);
            ViewBag.ProID = new SelectList(db.Products, "ID", "Name", oderDetail.ProID);
            return View(oderDetail);
        }

        // GET: Admin/OderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OderDetail oderDetail = db.OderDetails.Find(id);
            if (oderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OderID = new SelectList(db.Oders, "ID", "ShipName", oderDetail.OderID);
            ViewBag.ProID = new SelectList(db.Products, "ID", "Name", oderDetail.ProID);
            return View(oderDetail);
        }

        // POST: Admin/OderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProID,OderID,Quantity,Price")] OderDetail oderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OderID = new SelectList(db.Oders, "ID", "ShipName", oderDetail.OderID);
            ViewBag.ProID = new SelectList(db.Products, "ID", "Name", oderDetail.ProID);
            return View(oderDetail);
        }

        // GET: Admin/OderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OderDetail oderDetail = db.OderDetails.Find(id);
            if (oderDetail == null)
            {
                return HttpNotFound();
            }
            return View(oderDetail);
        }

        // POST: Admin/OderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OderDetail oderDetail = db.OderDetails.Find(id);
            db.OderDetails.Remove(oderDetail);
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
