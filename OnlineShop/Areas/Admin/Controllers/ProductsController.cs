using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;
using OnlineShop.Areas.Admin.Model;
using OnlineShop.Common;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/Products
        public ActionResult Index(int page = 1, int pagesize = 5)
        {

            var products = new UserDao();
            var model = products.ListProductAll();
            return View(model.ToPagedList(page, pagesize));

            /*var products = db.Products.Include(p => p.CategotyProduct);
            return View(products.ToList());*/
        }
        /*public ActionResult Index(int page = 1, int pagesize = 5)
        {
            var products = new UserDao();
            var model = products.ListAll();

            return View(model.ToPagedList(page, pagesize));
        }*/
        [HttpPost]
        /*public ActionResult Index(string searchString, int page = 1, int pagesize = 5)

        {

            var products = new UserDao();
            var model = products.ListProductWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            if (searchString == "")
            {
                return RedirectToAction("Index");
            }
            return View(model.ToPagedList(page, pagesize));
        }*/
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)

        {

            var pro = new UserDao();
            var model = pro.ListProductWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            if (searchString == "")
            {
                return RedirectToAction("Index");
            }
            return View(model.ToPagedList(pagesize, pagesize));
        }
        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            return View(db.Products.Where(p => p.ID == id).FirstOrDefault());
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);*/
        }

        // GET: Admin/Products/Create
        public ActionResult Create()

        {
            Product p = new Product();
            ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", p.CatProID);
            return View(p);

        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryProductDao();

            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product p, HttpPostedFileBase Imgfile)
        {
            /*try
            {
                Random r = new Random();
                int random = r.Next();
                if (p.Imgfile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(p.Imgfile.FileName);
                    string ex = Path.GetExtension(p.Imgfile.FileName);
                    filename = filename + ex;
                    p.Image = "~/Content/upload/" + random + filename;
                    p.Imgfile.SaveAs(Path.Combine(Server.MapPath("~/Content/upload/"), random + filename));

                }
                db.Products.Add(p);
                db.SaveChanges();
                SetAlert("Tạo sản phẩm thành công", "success");
                
                //ViewBag.msg = "Tạo người dùng thành công !!!!!";
                return RedirectToAction("Index");
                
            }
            catch
            {
                ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", p.CatProID);
                SetViewBag(p.CatProID);
                return View();
            }
           
            ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", p.CatProID);*/
            //ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", p.CatProID);

            Product product = new Product();

            string path = Uploadimage(Imgfile);
            if (path.Equals("-1"))
            {

            }
            else
            {
                product.Name = p.Name;
                product.CatProID = p.CatProID;
                product.Image = path;
                product.MoreImage = p.MoreImage;
                product.Price = p.Price;
                product.PriceKM = p.PriceKM;
                product.Quantity = p.Quantity;
                product.RemainingAmount = p.RemainingAmount;
                product.Detail = p.Detail;
                product.CauHinh = p.CauHinh;
                product.CreateDate = DateTime.Now;
                product.Status = p.Status;
                db.Products.Add(product);
               
                db.SaveChanges();
                SetAlert("Tạo sản phẩm thành công", "success");
                ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", product.CatProID);
                //insert xong về trang index.
                return RedirectToAction("Index");
            }
            ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", product.CatProID);
            //mới thêm vào
            //ModelState.Clear();
            return View();
        }
        
        public string Uploadimage(HttpPostedFileBase file){
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);
                        SetAlert("File uploaded successfully", "success");
                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;

        }
        //public ActionResult Create([Bind(Include = "ID,Name,CatProID,Image,MoreImage,Price,PriceKM,Quantity,RemainingAmount,Detail,CauHinh,Status")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", product.CatProID);
        //    return View(product);
        //}

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            Product product = db.Products.Find(id);
            ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", product.CatProID);
            //return View(product);
            return View(db.Products.Where(p => p.ID == id).FirstOrDefault());
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", product.CatProID);
            return View(product);*/

        }
        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Product p)
        {
            try
            {
                Random r = new Random();
                int random = r.Next();
                if (p.Imgfile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(p.Imgfile.FileName);
                    string ex = Path.GetExtension(p.Imgfile.FileName);
                    filename = filename + ex;
                    p.Image = "~/Content/upload/" + random + filename;
                    p.Imgfile.SaveAs(Path.Combine(Server.MapPath("~/Content/upload/"), random + filename));

                }
                
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật dùng thành công", "success");
                ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", p.CatProID);
                //ViewBag.msg = "Cập nhật người dùng thành công !!!!!";
                return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
            
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //

            string path = uploadimage(Imgfile);
            if (path.Equals("-1"))
            {

            }
            else
            {
                product.Name = p.Name;
                product.CatProID = p.CatProID;
                product.Image = path;
                product.MoreImage = p.MoreImage;
                product.Price = p.Price;
                product.PriceKM = p.PriceKM;
                product.Quantity = p.Quantity;
                product.RemainingAmount = p.RemainingAmount;
                product.Detail = p.Detail;
                product.CauHinh = p.CauHinh;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                //ViewBag.msg = "Thành công !!!!!";
                //insert xong về trang index.
                return RedirectToAction("Index");
            }
            ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", product.CatProID);
            //mới thêm vào
            //ModelState.Clear();
            return View();*/
        }

        
        
        /*public ActionResult Edit([Bind(Include = "ID,Name,CatProID,Image,MoreImage,Price,PriceKM,Quantity,RemainingAmount,Detail,CauHinh,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatProID = new SelectList(db.CategotyProducts, "ID", "Name", product.CatProID);
            return View(product);
        }*/

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            
            return View(db.Products.Where(u => u.ID == id).FirstOrDefault());
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);*/
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            SetAlert("Xóa sản phẩm thành công", "success");
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
        public ActionResult ChooseCategory()
        {
            CategotyProduct cate = new CategotyProduct();
            cate.CateCollection = db.CategotyProducts.ToList<CategotyProduct>();
            return PartialView(cate);
        }
    }
}
