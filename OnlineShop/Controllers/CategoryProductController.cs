using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CategoryProductController : Controller
    {
        // GET: CategoryProduct
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category(long id)
        {
            var category = new CategoryProductDao().ViewDetail(id);
            ViewBag.Category = category;
            var model = new ProductDao().ListByCategoryID(id);
            return View(model);
        }
    }
}