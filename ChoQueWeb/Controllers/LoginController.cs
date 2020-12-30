using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChoQueVN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChoQueVN.Controllers
{
    public class LoginController : Controller
    {
        DataContext db = new DataContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginPost()
        {
            string _account = Request.Form["Account"];
            string _password = Request.Form["Password"];

            //ma hoa password
            _password = Security.Encrypt(_password.ToString());

            var user = db.Users.Where(x => x.Account.Equals(_account) && x.Password.Equals(_password)).FirstOrDefault();
            if (user != null)
            {
                //đăng nhập thành công
                //sét biến session  để kiểm tra các trang trong admin
                HttpContext.Session.SetString("Account", _account);
                return Redirect("/Admin/Home");
            }
            else return RedirectToAction("Index", "Login");
        }
    }
}
