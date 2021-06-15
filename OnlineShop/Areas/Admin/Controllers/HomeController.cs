using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.EF;
using OnlineShop.Areas.Admin.Model;
using OnlineShop.Common;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private WebDbContext db = new WebDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.DemdonhangChuaDuyet = DemdonhangChuaDuyet();
            ViewBag.Demdonhang = DemdonhangDaDuyet();
            var session = (LoginModel)Session[Constants.USER_SESSION];
            if (session == null)
                return RedirectToAction("Index", "Login");
            return View();
        }
        public ActionResult Logout()
        {
            Session[Constants.USER_SESSION] = null;

            return RedirectToAction("Index", "Login");
        }
        public int DemdonhangChuaDuyet()
        {
            int sl = db.Oders.Where(x => x.Status == true).Count();
            return sl;
        }
        public int DemdonhangDaDuyet()
        {
            int sl = db.Oders.Where(x => x.Status == false).Count();
            return sl;
        }
    }
}