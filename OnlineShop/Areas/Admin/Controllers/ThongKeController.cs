using Models.DAO;
using Models.EF;
using OnlineShop.Areas.Admin.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: Admin/ThongKe
        WebDbContext db = new WebDbContext();
        public ActionResult Index()
        {
            var products = new ProductDao();

            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();
            ViewBag.ThongKeSoDH = ThongKeSoDH();
            ViewBag.ThongKeSoDHChuaDuyet = ThongKeSoDHChuaDuyet();
            ViewBag.ThongKeSanPham = ThongKeSanPham();
            
            var model = products.GetListBanChay();
            return View(model.ToPagedList(1, 5));
           
        }
        /*[HttpGet]
        public ActionResult GetListBanChay()
        {
            WebDbContext db = new WebDbContext();
            var result = (from product in db.Products
                          join orderdetail in db.OderDetails
                          on product.ID equals orderdetail.ProID
                          group orderdetail by product into g
                          let SoLuong = g.Sum(x=>x.Quantity)
                          orderby SoLuong descending
                          select new {
                              id= g.Key.ID,
                              name= g.Key.Name,
                              price= g.Key.Price,
                              quantity= SoLuong }).Take(3).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }*/

        public decimal ThongKeTongDoanhThu()
        {
            decimal tongDoangThu = db.OderDetails.Sum(x => x.Quantity * x.Price).Value;
            return tongDoangThu;
        }
        /* public float PTramTongDoanhThu()
         {
             DateTime now = DateTime.Now;
             int thang = now.Month;
             decimal doanhthuthangshientai = db.OderDetails.Sum(x => x.Quantity * x.Price).Value;
             var kqInnerJoin = from oder in db.Oders
                               join oderd in db.OderDetails
                               on oder.ID equals oderd.OderID 
                               where oder.CreateDate.Month == thang
                               select new
                               {
                                   Price = oderd.Price,
                                   Quantity = oderd.Quantity,
                                   Time = oder.CreateDate
                               };
             decimal tongTien = 0;
             tongTien = kqInnerJoin.Sum(x => x.Price * x.Quantity).Value;

             *//*foreach (var item in kqInnerJoin)
             {
                 tongTien += decimal.Parse((item.Quantity*item.Price).Value.ToString());
             }*//*

             var kqInnerJoin2 = from oder in db.Oders
                               join oderd in db.OderDetails
                               on oder.ID equals oderd.OderID
                               where oder.CreateDate.Month == thang-1
                               select new
                               {
                                   Price = oderd.Price,
                                   Quantity = oderd.Quantity,
                                   Time = oder.CreateDate
                               };

             decimal tongTien2 = 0;
             tongTien = kqInnerJoin.Sum(x => x.Price * x.Quantity).Value;
             *//*foreach (var item in kqInnerJoin)
             {
                 tongTien += decimal.Parse((item.Quantity * item.Price).Value.ToString());

             }*//*

                 decimal d =tongTien2 / tongTien;
                 float pt = (float)((1-d)*100);
                 return pt;



         }*/
        public decimal ThongKeDoanhThuThang(int thang, int nam)
        {
            var listDH = db.Oders.Where(x => x.CreateDate.Month == thang && x.CreateDate.Year == nam);
            decimal tongTien = 0;
            foreach (var item in listDH)
            {
                tongTien += decimal.Parse(item.OderDetails.Sum(x => x.Quantity * x.Price).Value.ToString());
            }
            return tongTien;
        }
        public int ThongKeSoDH()
        {
            int sl = db.Oders.Count();
            return sl;
        }
        public int ThongKeSoDHChuaDuyet()
        {
            int sl = db.Oders.Where(x => x.Status == true).Count();
            return sl;
        }
        public int ThongKeSanPham()
        {
            int sl = db.Products.Count();
            return sl;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetDataProduct()
        {
            WebDbContext context = new WebDbContext();
            var query = context.OderDetails.Include("Product")
                   .GroupBy(p => p.Product.Name)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetData()
        {
            int daduyet = db.Oders.Where(x => x.Status == false).Count();
            int chuaduyet = db.Oders.Where(x => x.Status == true).Count();
            


            Order obj = new Order();
            obj.Duyet = daduyet;
            obj.ChuaDuyet = chuaduyet;
            
            

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Order
        {
            public int Duyet { get; set; }
            public int ChuaDuyet { get; set; }
           

        }
        public ActionResult GetDataCategory()
        {
            int iphone = db.Products.Where(x => x.CatProID == 1).Count();
            int samsung = db.Products.Where(x => x.CatProID == 2).Count();
            int oppo = db.Products.Where(x => x.CatProID == 3).Count();
            int xiaomi = db.Products.Where(x => x.CatProID == 4).Count();
            int nokia = db.Products.Where(x => x.CatProID == 5).Count();

            Cato obj = new Cato();
            obj.Iphone = iphone;
            obj.Samsung = samsung;
            obj.Oppo = oppo;
            obj.Xiaomi = xiaomi;
            obj.Nokia = nokia;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Cato
        {
            public int Iphone { get; set; }
            public int Samsung { get; set; }
            public int Oppo { get; set; }
            public int Xiaomi { get; set; }
            public int Nokia { get; set; }
        }

    }
}