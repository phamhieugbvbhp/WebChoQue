using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChoQueVN.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChoQueVN.Controllers
{
    public class NewsController : Controller
    {
        DataContext db = new DataContext();
        public IActionResult Index(int id)
        {
            var listNews = db.News.Where(x => x.CategoryID == id).Take(6).ToList();
            ViewBag.tit = db.CategoryNews.Where(x => x.ID == id).FirstOrDefault();
            ViewBag.listNews = listNews;
            ViewBag.Style = "display:none";
            //Thông tin km. categoryID=4
            ViewBag.NewsKM = db.News.Where(x => x.CategoryID == 4).Take(4);

            //tin mới nhất
            ViewBag.NewNews = db.News.OrderByDescending(x => x.ID).Take(4);
            return View();
        }

        public IActionResult Detail(int id)
        {
            var item = db.News.Where(x => x.ID == id).FirstOrDefault();
            ViewBag.Style = "display:none";
            ViewBag.item = item;
            //Thông tin km. categoryID=4
            ViewBag.NewsKM = db.News.Where(x => x.CategoryID == 4).Take(4);

            //tin mới nhất
            ViewBag.NewNews = db.News.OrderByDescending(x => x.ID).Take(4);
            return View();
        }
    }
}
