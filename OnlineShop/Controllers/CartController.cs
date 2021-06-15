using Models.DAO;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Models.EF;
using System.Data.Entity;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        WebDbContext db =new  WebDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new UserDao().ViewDetail(productId);
            var cart = Session[CartSession];
           
            if(cart!= null)
            {
                var list= (List<CartItem>)cart;
                if(list.Exists(x=>x.Product.ID == productId))
                {
                    foreach(var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    // tạo mới đối tượng cartitem
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //gán vào sesion
                Session[CartSession] = list;
            }
            else
            {
                // tạo mói đối tượng cartitem
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gán vào session
                Session[CartSession] = list;

            }
            return RedirectToAction("Index");
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address)
        {
            WebDbContext db = new WebDbContext();
            var order = new Oder();
            order.CreateDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipPhone = mobile;
            order.ShipName = shipName;
            order.Status = true;


            try
            {
                var id = new OderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OderDetail();
                    orderDetail.ProID = item.Product.ID;
                    orderDetail.OderID = (int)id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);
                    
                       // giảm số lương sản phẩm khi có đơn hàng
                    Product p = db.Products.Find(item.Product.ID);
                    p.Quantity -= item.Quantity;
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();



                    //total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
               
                /*string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);*/
            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }
        public bool UpdateQUantity(int productId, int quantity)
        {
            WebDbContext db = new WebDbContext();

            var product = new UserDao().ViewDetail(productId);
            if (product.Quantity < quantity)
            {
                return false;
            }
            product.Quantity -= quantity;
            return true; 
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}