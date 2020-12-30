using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChoQueVN.Models;
using Microsoft.AspNetCore.Mvc;
//su dung cac class ben trong thu muc Models
//phan trang
using X.PagedList;

namespace ChoQueVN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        DataContext db = new DataContext();

        public IActionResult Index(int? page)
        {
            //---
            //lay bien key truyen tu url
            string key = Request.Query["key"];
            if (key == null)
                key = "";
            //lay bien where truyen tu url
            string where = Request.Query["where"];
            //---
            //quy dinh so ban ghi tren mot trang
            int pageSize = 6;
            //kiem tra neu co bien page truyen vao thi lay no
            //con khong thi lay gia tri 1
            int pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            //lay cac ban ghi, sap xep theo id giam dan
            var listuser = db.Users.OrderByDescending(x => x.ID).ToList();
            //ViewBag.ListUsers = listuser;
            return View("Index",listuser.ToPagedList(pageNumber,pageSize));
        }

        public IActionResult AddEdit(int ID)
        {
            if (ID == null || ID == 0)
            {
                ViewBag.Header = "Thêm tài khoản";
                ViewBag.ActionForm = "/Admin/User/AddEditPost";
                return View();
            }
            else
            {
                ViewBag.Header = "Sửa thông tin tài khoản";
                ViewBag.ActionForm = "/Admin/User/AddEditPost?ID="+ID;
                var user = db.Users.Where(x => x.ID == ID).FirstOrDefault();
                var _user = user;
                _user.Password = user.Password==null?"": Security.Decrypt(user.Password);
                return View("AddEdit",_user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditPost(int ID = 0)
        {
            var name = Request.Form["Name"].ToString();
            var account = Request.Form["Account"].ToString();
            var password = Request.Form["Password"].ToString();

            if (ID == 0)
            {
                if (db.Users.Where(x => x.Account.Equals(account)).Any())//tafi khoan trung thi khong cho them
                {
                    ViewBag.Error = "Tài khoản đã được sử dụng";
                    return View("AddEdit");
                }
                var user = new User();
                user.Name = name;
                user.Account = account;
                user.Password = Security.Encrypt(password);

                db.Users.Add(user);
                db.SaveChangesAsync();

                ViewBag.msg = "Thêm thành công";
                return RedirectToAction("Index", "User");
            }
            else
            {
                var oldUser = db.Users.Where(x => x.ID == ID).FirstOrDefault();

                oldUser.Name = name;
                oldUser.Password = Security.Encrypt(password);

                db.Users.Update(oldUser);
                db.SaveChangesAsync();

                ViewBag.msg = "Sửa thành công";
                return RedirectToAction("Index", "User");
            }
        }

        //[HttpPost]
        public IActionResult Delete(int ID)
        {
            if(ID == 0){
                ViewBag.meg = "Xoá không thành công";
                return RedirectToAction("Index", "User");
            }
            else
            {
                var oldUser = db.Users.Where(x => x.ID == ID).FirstOrDefault();
                db.Users.Remove(oldUser);
                db.SaveChangesAsync();
                ViewBag.msg = "Xoá thành công";
                return RedirectToAction("Index", "User");
            }
        }
    }
}
