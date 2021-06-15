using Models.EF;
using OnlineShop.Areas.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ThongKeBieuDoController : Controller
    {
        WebDbContext db = new WebDbContext();
        // GET: Admin/ThongKeBieuDo
        public ActionResult Index()
        {
            WebDbContext context = new WebDbContext();
            
            var query = (from order in db.Oders
                         join orderdetail in db.OderDetails on order.ID equals orderdetail.OderID
                         join product in db.Products on orderdetail.ProID equals product.ID

                         where order.Status == false
                         select new
                         {
                             month = order.CreateDate.Hour + ":" + order.CreateDate.Minute + "<br>" + order.CreateDate.Day + "/" + order.CreateDate.Month + "/" + order.CreateDate.Year,
                             ttien = orderdetail.Price * orderdetail.Quantity,
                             loinhuan = (orderdetail.Price * orderdetail.Quantity) - (orderdetail.Quantity * product.RemainingAmount)

                         }).ToList();
            
            /*ViewBag.Message = query;
            ViewBag.NumTimes = query.Count();*/
            return View(query);
        }
        

        
        public ActionResult GetDataProfit()
        {
            WebDbContext context = new WebDbContext();

            var query = (from order in db.Oders
                         join orderdetail in db.OderDetails on order.ID equals orderdetail.OderID
                         join product in db.Products on orderdetail.ProID equals product.ID

                         where order.Status == false
                         select new
                         {
                             month = order.CreateDate.Hour+":"+ order.CreateDate.Minute + "<br>" + order.CreateDate.Day+"/"+order.CreateDate.Month + "/" + order.CreateDate.Year,
                             ttien = orderdetail.Price * orderdetail.Quantity,
                             loinhuan = (orderdetail.Price * orderdetail.Quantity) - (orderdetail.Quantity * product.RemainingAmount)

                         })
                         /*.OrderBy(x => x.month).ToList()*/;
            return Json(query, JsonRequestBehavior.AllowGet);
            //Price: giá bán cho khách hàng, Quantity: số lượng, RemainingAmount: giá nhập của sản phẩm
            /*var result1 = (from orderdetail in db.OderDetails
                           join order in (from item in db.Oders
                                          group item by item.CreateDate.Month into g
                                          select new { ID = g.Key })
                           on orderdetail.OderID equals order.ID
                           join product in db.Products on orderdetail.ProID equals product.ID

                           select new
                           {
                               month = order.ID,
                               ttien = orderdetail.Price * orderdetail.Quantity,
                               loinhuan = orderdetail.Price * orderdetail.Quantity - orderdetail.Quantity * product.RemainingAmount,
                           }).GroupBy(p => p.month,
                                 (k, c) => new
                                 {
                                     month = k,
                                     ttien= c.Sum(t=>t.ttien),
                                     loinhuan = c.Sum(t => t.loinhuan),
                                     
                                 }
                                ).ToList();*/




            /*var result = (from orderdetail in db.OderDetails
                          join order in (from item in db.Oders
                                         group item by item.CreateDate.Month into g
                                         select new { ID = g.Key, Month = g.Select(item => item.CreateDate.Month) })
                         on orderdetail.OderID equals order.ID
                          join product in db.Products on orderdetail.ProID equals product.ID
                          select new
                          {
                              Month = order.ID,
                              ttien = orderdetail.Price * orderdetail.Quantity,
                              loinhuan = orderdetail.Price * orderdetail.Quantity - orderdetail.Quantity * product.RemainingAmount,
                          }).ToList();*/

            /*var result1 = (from order in db.Oders
                           join orderdetail in (from item in db.OderDetails
                                                group item by item.OderID into g
                                                select new { ID = g.Key, TongTien = g.Sum(item => item.Quantity * item.Price), Soluong = g.Select(e => e.Quantity) })
                           on order.ID equals orderdetail.ID
                           join product in db.Products on orderdetail.ID equals product.ID
                           
                           select new
                           {
                               Month = orderdetail.ID,
                               ttien = orderdetail.TongTien,

                               loinhuan = orderdetail.TongTien - product.RemainingAmount,
                           }).ToList();*/

        }
       
    }
}
