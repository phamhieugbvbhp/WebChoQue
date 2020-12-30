using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChoQueVN.Models;
using ChoQueVN.Models.Globals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChoQueVN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public IActionResult Index()
        {
            var product = db.Bills.GroupBy(x => x.ProductID).ToList();

            //var result=from p in db.Bills.ToList()
            //           group p by p.ProductID into billG
            //           select new
            //           {
            //               ProductName=db.Products.Where(x=>x.ID==billG.Key),
            //               BestBuy=billG.Max(x=>x.)
            //           }
            var a = product.Max(x=>x.Count());
            var b = new string[2];
            for(int i = 0; i < product.Count; i++)
            {
                if (product[i].Count() == a)
                {
                    var name = db.Products.Where(x => x.ID == int.Parse(product[i].Key.ToString())).FirstOrDefault();
                    b[0]=name==null?"":name.Name;
                    b[1]=a.ToString();
                }    
            }

            ViewBag.Online = Online.online;
            ViewBag.Visit = db.Statisticals.Where(x => x.ID == 1).FirstOrDefault().Visit;
            ViewBag.BestBuy = b;
            return View();
        }
    }
}
