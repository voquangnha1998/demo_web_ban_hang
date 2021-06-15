using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Areas.Admin.Model;
using OnlineShop.Common;
using System.Data.SqlClient;
using System.Configuration;
using Models.EF;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        WebDbContext db = new WebDbContext();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel admin)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.login(admin.UserName, Encryptor.EncryptMD5(admin.Pass));

                if (result == 1)
                {
                    /*var user = dao.Find(admin.UserName);
                    var userSession = new LoginModel();
                    userSession.UserName = user.UserName;
                    userSession.ID = user.ID;
                    userSession.Avartar = user.Avartar;*/
                    //ModelState.AddModelError("","Đăng nhập thành công");
                    Session.Add(Constants.USER_SESSION, admin);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công");
                }
            }
            return View("Index");
        }
        /*public ActionResult Welome(LoginModel admin)
        {
            string displayimg = (string)Session["UserName"];
            string mainconn = ConfigurationManager.ConnectionStrings["WebDbContext"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            string sql = "select Avatatr from Admin where User='" + displayimg + "'";
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql,sqlConnection);
            sqlCommand.Parameters.AddWithValue("UserName", Session["UserName"].ToString());
            SqlDataReader sdr = sqlCommand.ExecuteReader();
            if (sdr.Read())
            {
                string s = sdr["Avatar"].ToString();
                ViewData["Img"] = s;
            }
            sqlConnection.Close();
            return View();
        }*/
    }
}