using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChoQueVN.Areas.Admin.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            //huy session email
            HttpContext.Session.Remove("Account");
            //di chuyen den url /Login
            return Redirect("/Login");
        }
    }
}
