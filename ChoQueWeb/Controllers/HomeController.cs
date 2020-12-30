using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using ChoQueVN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChoQueVN.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: HomeController
        public ActionResult Index()
        {
            var listProducts = db.Products.OrderByDescending(x => x.ID);
            var listCategories = db.ProductCategories.OrderByDescending(x => x.ID);

            ViewBag.hotProducts = db.Products.Where(x=>x.Hot==true).Take(12).OrderByDescending(x => x.ID);
            ViewBag.Categories = listCategories.ToList();

            //sản phẩm và đồ uống giảm cân ID=10
            ViewBag.Products10 = db.Products.Where(x =>db.ProductCategories.Where(o=>o.ParentID==10).Select(o=>o.ID).Contains(x.CategoryID)).Take(12).OrderByDescending(x => x.ID);

            //thực dưỡng và ăn chay ID=11
            ViewBag.Products11 = db.Products.Where(x => db.ProductCategories.Where(o => o.ParentID == 11).Select(o => o.ID).Contains(x.CategoryID)).Take(4).OrderByDescending(x => x.ID);

            //Hạt dinh dưỡng ID=12
            ViewBag.Products12 = db.Products.Where(x => db.ProductCategories.Where(o => o.ParentID == 12).Select(o => o.ID).Contains(x.CategoryID)).Take(4).OrderByDescending(x => x.ID);

            //Thông tin km. categoryID=4
            ViewBag.NewsKM = db.News.Where(x => x.CategoryID == 4).Take(4);

            //tin mới nhất
            ViewBag.NewNews = db.News.OrderByDescending(x => x.ID).Take(4);
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
