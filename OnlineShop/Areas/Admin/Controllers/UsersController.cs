using System;
using System.Collections.Generic;
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
    public class UsersController : BaseController
    {
        private WebDbContext db = new WebDbContext();
        // GET: Admin/Users
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            var user = new UserDao();
            var model = user.ListAll();

            return View(model.ToPagedList(page, pagesize));
        }
        [HttpPost]
        public ActionResult Index(string searchString,int page= 1, int pagesize = 5)

        {

            var user = new UserDao();
            var model = user.ListWhereAll(searchString, page,pagesize);
            ViewBag.SearchString = searchString;
            if(searchString == "") {
                return RedirectToAction("Index");
            }
            return View(model.ToPagedList(page,pagesize));
        }
        public ActionResult Details(int? id)
        {
            return View(db.Users.Where(u => u.ID == id).FirstOrDefault());
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {  
                return HttpNotFound();
            }
            return View(user);*/
        }
        public ActionResult Create()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public ActionResult Create(User u, HttpPostedFileBase Imgfile)
        {
            

            try
            {
                Random r = new Random();
                int random = r.Next();
                if (u.Imgfile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(u.Imgfile.FileName);
                    string ex = Path.GetExtension(u.Imgfile.FileName);
                    filename = filename + ex;
                    u.Avartar = "~/Content/imguser/" +random +filename;
                    u.Imgfile.SaveAs(Path.Combine(Server.MapPath("~/Content/imguser/"), random+filename));

                }
                var pass = Encryptor.EncryptMD5(u.Pass);
                u.Pass = pass;
                db.Users.Add(u);
                db.SaveChanges();
                SetAlert("Tạo dùng thành công", "success");
                //ViewBag.msg = "Tạo người dùng thành công !!!!!";
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
            /*User user = new User();

            string path = uploadimage(Imgfile);
            if (path.Equals("-1"))
            {

            }
            else
            {
                user.Name = u.Name;
                user.Address = u.Address;
                user.Email = u.Email;
                user.Phone = u.Phone;
                user.UserName = u.UserName;
                var pass = Encryptor.EncryptMD5(u.Pass);
                user.Pass = pass;
                user.Avartar = path;
                user.Status = u.Status;
                
                db.Users.Add(user);
                db.SaveChanges();
                ViewBag.msg = "Thành công !!!!!";
                //insert xong về trang index.
                return RedirectToAction("Index");
            }
            
            return View();*/
        }
        /*public ActionResult Create(User model)
        {
           
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //Tìm tên đăng nhập có trung k
                //nếu trùng thì tar về trang create
                if (dao.Find(model.UserName) != null)
                {
                    SetAlert("Tên người dùng tồn tại. Mời nhập tên khác", "warning");
                    return RedirectToAction("Create", "User");
                }
                var pass = Encryptor.EncryptMD5(model.Pass);
                model.Pass = pass;
                string result = dao.Insert(model);
                if (!string.IsNullOrEmpty(result))
                {
                    SetAlert("Tạo mới người dùng thành công", "success");
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    SetAlert("Tạo mới người dùng không thành công", "error");
                    //ModelState.AddModelError("", "Tạo người dùng không thành công");
                }
            }
            return View();
        }*/
        public ActionResult Edit(int? id)
        {


            return View(db.Users.Where(u => u.ID == id).FirstOrDefault());
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);*/
        }
        [HttpPost]
        public ActionResult Edit(int id, User u)

        {
            try
            {
                Random r = new Random();
                int random = r.Next();
                if (u.Imgfile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(u.Imgfile.FileName);
                    string ex = Path.GetExtension(u.Imgfile.FileName);
                    filename = filename + ex;
                    u.Avartar = "~/Content/imguser/" + random + filename;
                    u.Imgfile.SaveAs(Path.Combine(Server.MapPath("~/Content/imguser/"), random + filename));

                }
                var pass = Encryptor.EncryptMD5(u.Pass);
                u.Pass = pass;
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật dùng thành công", "success");
                //ViewBag.msg = "Cập nhật người dùng thành công !!!!!";
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
            /*try{
                if (u.Imgfile != null) {
                    string filename = Path.GetFileNameWithoutExtension(u.Imgfile.FileName);
                    string ex = Path.GetExtension(u.Imgfile.FileName);
                    filename = filename + ex;
                    u.Avartar = "~/Content/imguser/" + filename;
                    u.Imgfile.SaveAs(Path.Combine(Server.MapPath("~/Content/imguser/"), filename));

                }
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }*/


        }
        public ActionResult Delete(int? id)
        {
            ViewBag.msg = "Xóa người dùng thành công !!!!!";
            return View(db.Users.Where(u => u.ID == id).FirstOrDefault());
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);*/
        }

        // POST: Admin/Users1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,User user)
        {
            try
            {
                user = db.Users.Where(u => u.ID == id).FirstOrDefault();
                db.Users.Remove(user);
                db.SaveChanges();
                SetAlert("Xóa người dùng thành công", "success");
                return RedirectToAction("Index");
            }catch(Exception e)
            {
                return View();
            }
        }
        public string uploadimage(HttpPostedFileBase file)
        {
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
                        path = Path.Combine(Server.MapPath("~/Content/imguser"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/imguser/" + random + Path.GetFileName(file.FileName);
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
    }

}