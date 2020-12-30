using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChoQueVN.Models;
using ChoQueVN.Models.Connect;
using ChoQueVN.Models.Entities;
using Microsoft.AspNetCore.Http;
using ChoQueVN.Models.Globals;
using X.PagedList;

namespace ChoQueVN.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class NewsController : Controller
    {
        Data data = new Data();
        DataContext db = new DataContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewsCategory(int? page)
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

            string sql = "Select *from CategoryNews";
            var dr = data.dr(sql);
            List<CategoryNews> li = new List<CategoryNews>();
            while (dr.Read())
            {
                var cate = new CategoryNews();
                cate.ID = int.Parse(dr["ID"].ToString());
                cate.Name = dr["Name"].ToString();
                li.Add(cate);
            }
            return View("NewsCategory", li.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult News(int ?page)
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

            string sql = "Select *from News";
            var dr = data.dr(sql);
            List<News> list = new List<News>();
            while (dr.Read())
            {
                var ne = new News();
                ne.ID = int.Parse(dr["ID"].ToString());
                ne.Name = dr["Name"].ToString();
                ne.Description = dr["Description"].ToString();
                ne.Image = dr["Image"].ToString();
                list.Add(ne);
            }
            return View("News", list.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Addedit(int id)
        {
            if (id == 0)
            {
                ViewBag.ActionForm = "/Admin/News/Addeditpost";
                ViewBag.Categories = db.CategoryNews.OrderByDescending(x => x.ID).ToList();
                return View();
            }
            else
            {
                string sql = $"Select * from News where News.ID={id}";
                var dr = data.dr(sql);
                var news = new News();
                while (dr.Read())
                {
                    news.Name = dr["Name"].ToString();
                    news.Description = dr["Description"].ToString();
                    news.Image = dr["Image"].ToString();
                    news.ID = int.Parse(dr["ID"].ToString());
                    news.CategoryID = int.Parse(dr["CategoryID"].ToString());
                    news.Content = dr["Content"].ToString();
                    ViewBag.Actionform = "/Admin/News/Addeditpost?Id=" + id;
                }
                ViewBag.Categories = db.CategoryNews.OrderByDescending(x => x.ID).ToList();
                return View("Addedit", news);

            }
        }

        public IActionResult AddeditCategory(int id)
        {
            if (id == 0)
            {
                ViewBag.ActionForm = "/Admin/News/AddeditCategorypost";
                return View();
            }
            else
            {
                string sql = $"select *from CategoryNews where CategoryNews.ID={id}";
                var dr = data.dr(sql);
                var category = new CategoryNews();
                while (dr.Read())
                {
                    category.Name = dr["Name"].ToString();
                    category.ID = int.Parse(dr["ID"].ToString());
                    ViewBag.ActionForm = "/Admin/News/AddeditCategorypost?Id=" + id;
                }
                return View("AddeditCategory", category);
            }
        }

        [HttpPost]
        public IActionResult AddeditCategorypost(int id)
        {
            var name = Request.Form["Category"].ToString();
            if (id == 0)
            {
                string sql = $"Insert into CategoryNews values(N'{name}')";
                data.execute(sql);
                return Redirect("/admin/News/NewsCategory");
            }
            else
            {
                string sql = $"update CategoryNews set Name=N'{name}' where CategoryNews.ID={id}";
                data.execute(sql);
                return Redirect("/admin/News/NewsCategory");
            }
        }
        public IActionResult Addeditpost(int id, IFormFile formFile)
        {
            var name = Request.Form["Name"].ToString();
            var categoryID= Request.Form["CategoryID"].ToString();
            var description = Request.Form["Description"].ToString();
            var content = Request.Form["Content"].ToString();
            //var image = Request.Form["Image"].ToString();
            if (id == 0)
            {
                var _formFile = formFile != null ? Images.ImgURL(formFile, "ImagesNews") : "";
                string sql = $"Insert into News values(N'{name}',N'{description}',N'{_formFile}'),{categoryID},N'{content}'";
                data.execute(sql);
                return Redirect("/admin/News/News");
            }
            else
            {
                if (formFile != null)
                {
                    string sql = $"update News set News.Name=N'{name}', News.Description=N'{description}', News.Image='{Images.ImgURL(formFile, "ImagesNews")}',News.CategoryID={categoryID},News.Content=N'{content}' where News.ID={id}";
                    data.execute(sql);
                }
                else
                {
                    string sql = $"update News set News.Name=N'{name}', News.Description=N'{description}',News.CategoryID={categoryID},News.Content=N'{content}' where News.ID={id}";
                    data.execute(sql);
                }
                return Redirect("/admin/News/News");
            }
        }
        public IActionResult DeleteCategory(int id)
        {
            string sql = $"delete CategoryNews where ID={id}";
            data.execute(sql);
            return Redirect("/admin/News/NewsCategory");
        }
        public IActionResult Deletenews(int id)
        {
            var dr = data.dr($"select News.Image from News where News.ID={id}");
            while (dr.Read())
            {
                Images.RemoveImgURL(dr["Image"].ToString(), "ImagesNews");
            }

            string sql = $"Delete News where ID={id}";
            data.execute(sql);
            return Redirect("/admin/News/News");
        }
    }
}
