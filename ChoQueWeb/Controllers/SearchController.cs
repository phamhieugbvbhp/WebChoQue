using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChoQueVN.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChoQueVN.Controllers
{
    public class SearchController : Controller
    {
        DataContext db = new DataContext();
        [HttpPost]
        public IActionResult Index()
        {
            var key = Request.Form["Search"].ToString();

            var list = db.Products.Where(x => x.Name.Contains(key) || x.Description.Contains(key) || x.Content.Contains(key)).ToList();
            ViewBag.sProducts = list;
            //Thông tin km. categoryID=4
            ViewBag.NewsKM = db.News.Where(x => x.CategoryID == 4).Take(4);

            //tin mới nhất
            ViewBag.NewNews = db.News.OrderByDescending(x => x.ID).Take(4);
            return View();
        }
    }
}
