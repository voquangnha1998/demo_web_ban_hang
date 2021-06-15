using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var producdao = new ProductDao();
            var categoryproductdao = new CategoryProductDao();
            ViewBag.NewProducts = producdao.ListNewProduct(2);
            ViewBag.NewProducts3 = producdao.ListNewProduct(4);
            ViewBag.IphoneProducts3 = producdao.ListProductIphone(4);
            ViewBag.CategoryProduct = categoryproductdao.ListCategoryProduct();
            ViewBag.ListProducts = producdao.ListNewProduct(8);
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[Common.Constants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}