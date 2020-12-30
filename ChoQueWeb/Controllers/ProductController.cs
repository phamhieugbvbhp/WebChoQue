using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChoQueVN.Models;
using ChoQueVN.Models.Entities;
using ChoQueVN.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ChoQueVN.Controllers
{
    public class ProductController : Controller
    {
        DataContext db=new DataContext();
        public IActionResult Index(int id,int? page)
        {
            //---
            //lay bien key truyen tu url
            string key = Request.Query["key"];
            
            //lay bien where truyen tu url
            string where = Request.Query["where"];
            //---
            //quy dinh so ban ghi tren mot trang
            int pageSize = 6;
            //kiem tra neu co bien page truyen vao thi lay no
            //con khong thi lay gia tri 1
            int pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;

            if (id == 0)
            {
                var _id = Request.Query["id"].ToString();

                id = int.Parse(_id.Substring(_id.LastIndexOf('?')+4));
            }

            var listProduct = db.Products.Where(x => x.CategoryID == id).OrderByDescending(x => x.ID).ToList().Take(9);

            if (key == null)
                key = "";
            else
            {
                if (key == "priceAsc")
                {
                    listProduct = db.Products.Where(x => x.CategoryID == id).OrderBy(x => x.Price).ToList().Take(9);
                }
                else if (key == "priceAsc")
                { 
                    listProduct = db.Products.Where(x => x.CategoryID == id).OrderByDescending(x => x.Price).ToList().Take(9); 
                }
                else if (key == "priceAsc")
                { 
                    listProduct = db.Products.Where(x => x.CategoryID == id).OrderBy(x => x.Name).ToList().Take(9); 
                }
                else if (key == "priceAsc")
                { 
                    listProduct = db.Products.Where(x => x.CategoryID == id).OrderByDescending(x => x.Name).ToList().Take(9); 
                }
                else
                    listProduct = db.Products.Where(x => x.CategoryID == id).OrderByDescending(x => x.ID).ToList().Take(9);
            }

            var listCategory = db.ProductCategories.Where(x => x.ParentID != 0).ToList();
            var listNews = db.News.OrderByDescending(x => x.ID).Take(6);
            var _category = db.ProductCategories.Where(x => x.ID == id).FirstOrDefault();
            var ParentCategory = db.ProductCategories.Where(x=>x.ID==db.ProductCategories.Where(o=>o.ID==id).FirstOrDefault().ParentID).FirstOrDefault();
            if (ParentCategory != null)
            {
                var category = db.ProductCategories.Where(x => x.ID == id).FirstOrDefault();
                var a = new CategoryProductVM();
                a.ID = category.ID;
                a.Name = category.Name;
                a.ParentID = category.ParentID;
                a.ParentName = db.ProductCategories.Where(x => x.ID == category.ParentID).FirstOrDefault().Name;
                ViewBag.tit = a;
            }

            ViewBag.listCategory = listCategory;
            ViewBag.listNews = listNews;
            ViewBag.Category = _category;
            ViewBag.Style = "display:none";

            //Thông tin km. categoryID=4
            ViewBag.NewsKM = db.News.Where(x => x.CategoryID == 4).Take(4);

            //tin mới nhất
            ViewBag.NewNews = db.News.OrderByDescending(x => x.ID).Take(4);

            return View("Index",listProduct.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Detail(int id)
        {
            var item = db.Products.Where(x => x.ID == id).FirstOrDefault();
            var ParentCategory = db.ProductCategories.Where(x => x.ID == db.ProductCategories.Where(o => o.ID == id).FirstOrDefault().ParentID).FirstOrDefault();
            if (ParentCategory != null)
            {
                var category = db.ProductCategories.Where(x => x.ID == id).FirstOrDefault();
                var a = new CategoryProductVM();
                a.ID = category.ID;
                a.Name = category.Name;
                a.ParentID = category.ParentID;
                a.ParentName = db.ProductCategories.Where(x => x.ID == category.ParentID).FirstOrDefault().Name;
                ViewBag.tit = a;
            }
            ViewBag.item = item;
            var listNews = db.News.OrderByDescending(x => x.ID).Take(4);
            ViewBag.listNews = listNews;
            ViewBag.Style = "display:none";

            //Thông tin km. categoryID=4
            ViewBag.NewsKM = db.News.Where(x => x.CategoryID == 4).Take(4);

            //tin mới nhất
            ViewBag.NewNews = db.News.OrderByDescending(x => x.ID).Take(4);
            return View();
        }

        [HttpPost]
        public IActionResult Buy(int ID)
        {
            //var productID = Request.Form["Product"];
            var name = Request.Form["Name"].ToString();
            var address = Request.Form["Address"].ToString();
            var phone = Request.Form["Phone"].ToString();
            var email = Request.Form["Email"].ToString();

            var bill = new Bill();
            bill.NameCus = name;
            bill.Address = address;
            bill.Email = email;
            bill.Phone = phone;
            bill.ProductID = ID;

            db.Bills.Add(bill);
            db.SaveChangesAsync();

            Response.WriteAsync("<script>alert('Dat mua thanh cong, quay lại trang chu sau it giay!');window.location.href='https://localhost:44336/'</script>");
            return Redirect("/Home");
        }
    }
}
