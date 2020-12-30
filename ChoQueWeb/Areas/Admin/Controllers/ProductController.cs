using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChoQueVN.Models;
using ChoQueVN.Models.Entities;
using ChoQueVN.Models.Globals;
using ChoQueVN.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ChoQueVN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        DataContext db = new DataContext();

        public IActionResult Index()
        {
            return View();
        }

        #region Category
        public IActionResult ProductCategory(int? page)
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
            int pageSize = 12;
            //kiem tra neu co bien page truyen vao thi lay no
            //con khong thi lay gia tri 1
            int pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            //lay cac ban ghi, sap xep theo id giam dan
            var listCategories = db.ProductCategories.OrderByDescending(x => x.ID).ToList();

            var list = new List<CategoryProductVM>();
            foreach(var item in listCategories)
            {
                var a = new CategoryProductVM();
                a.ID = item.ID;
                a.Name = item.Name;
                a.ParentID = item.ParentID;
                a.ParentName = item.ParentID != 0 ? db.ProductCategories.Where(x => x.ID == item.ParentID).FirstOrDefault().Name:"";
                list.Add(a);
            }
            
            return View("ProductCategory", list.ToPagedList(pageNumber, pageSize));
        }

        
        public IActionResult AddEditCategory(int ID)
        {
            if (ID == 0)
            {
                ViewBag.Header = "Thêm danh mục";
                ViewBag.FormAction = "/Admin/Product/AddEditCategoryPost";
                ViewBag.Categories = db.ProductCategories.OrderByDescending(x => x.ID).ToList();
                return View();
            }
            else
            {
                ViewBag.Header = "Sửa danh mục";
                ViewBag.FormAction = "/Admin/Product/AddEditCategoryPost?ID="+ID;
                var category = db.ProductCategories.Where(x => x.ID == ID).FirstOrDefault();
                ViewBag.Categories = db.ProductCategories.OrderByDescending(x => x.ID).ToList();

                var cate = new CategoryProductVM();
                cate.ID = category.ID;
                cate.Name = category.Name;
                cate.ParentID = category.ParentID;
                cate.ParentName = category.ParentID==0?"": db.ProductCategories.Where(x => x.ID == category.ParentID).FirstOrDefault().Name;

                return View("AddEditCategory",cate);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditCategoryPost(int ID=0)
        {
            var name = Request.Form["Category"].ToString();
            var parentID = Request.Form["ParentID"].ToString();
            if (ID == 0)
            {
                var category = new ProductCategory();

                category.Name = name;
                if (parentID != "")
                    category.ParentID = int.Parse(parentID);

                db.ProductCategories.Add(category);
                db.SaveChanges();
                return RedirectToAction("ProductCategory", "Product");
            }
            else
            {
                var oldCategory = db.ProductCategories.Where(x => x.ID == ID).FirstOrDefault();
                if (oldCategory == null)
                {
                    return null;
                }
                else
                {
                    oldCategory.Name = name;
                    oldCategory.ParentID = int.Parse(parentID);

                    db.ProductCategories.Update(oldCategory);
                    db.SaveChanges();
                    return RedirectToAction("ProductCategory", "Product");
                }
            }
        }

        public IActionResult DeleteCategory(int ID=0)
        {
            if (ID != 0)
            {
                var oldCategory = db.ProductCategories.Where(x => x.ID == ID).FirstOrDefault();
                db.ProductCategories.Remove(oldCategory);
                db.SaveChanges();
            }
            return RedirectToAction("ProductCategory", "Product");
        }
        #endregion

        #region Product
        public IActionResult Product(int? page)
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
            var listProduct = db.Products.OrderByDescending(x => x.ID).ToList();

            var list = new List<ProductVM>();
            foreach (var item in listProduct)
            {
                var a = new ProductVM();
                a.ID = item.ID;
                a.Name = item.Name;
                a.Description = item.Description;
                a.Price = item.Price;
                a.Discount = item.Discount;
                a.Image = item.Image;
                a.Content = item.Content;
                a.CategoryID = item.CategoryID;
                a.CategoryName = item.CategoryID == 0 ? "" : db.ProductCategories.Where(x => x.ID == item.CategoryID).FirstOrDefault().Name;
                a.Hot = item.Hot;
                list.Add(a);
            }

            return View("Product", list.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult AddEdit(int ID = 0)
        {
            if (ID == 0)
            {
                ViewBag.FormAction = "/Admin/Product/AddEditPost";
                ViewBag.Header = "Thêm sản phẩm";
                ViewBag.Categories = db.ProductCategories.OrderByDescending(x => x.ID).ToList();
                return View();
            }
            else
            {
                var oldItem = db.Products.Where(x => x.ID == ID).FirstOrDefault();

                var item = new ProductVM();
                item.ID = oldItem.ID;
                item.Name = oldItem.Name;
                item.Description = oldItem.Description;
                item.Price = oldItem.Price;
                item.Discount = oldItem.Discount;
                item.Content = oldItem.Content;
                item.CategoryID = oldItem.CategoryID;
                item.Image = oldItem.Image;
                item.CategoryName = db.ProductCategories.Where(x => x.ID == oldItem.CategoryID).FirstOrDefault().Name;
                item.Hot = oldItem.Hot;

                ViewBag.FormAction = "/Admin/Product/AddEditPost?ID="+ID;
                ViewBag.Header = "Sửa thông tin sản phẩm";
                ViewBag.Categories = db.ProductCategories.OrderByDescending(x => x.ID).ToList();
                return View(item);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditPost(IFormFile formFile,int ID=0)
        {
            var Name = Request.Form["Name"].ToString();
            var Description = Request.Form["Description"].ToString();
            var Price = Request.Form["Price"].ToString();
            var Discount = Request.Form["Discount"].ToString();
            var Content = Request.Form["Content"].ToString();
            var CategoryID = Request.Form["CategoryID"].ToString();
            var isHot = Request.Form["Hot"] != "" && Request.Form["Hot"] == "on" ? true : false; ;

            if (ID == 0)
            {
                Product item = new Product();

                item.Name = Name;
                item.Description = Description;
                item.Price = Price==""||Price==null?0:double.Parse(Price);
                item.Discount = Price == "" || Price == null ? 0 : double.Parse(Discount);
                item.Content = Content;
                item.CategoryID = int.Parse(CategoryID);
                item.Image = Images.ImgURL(formFile,"ImageProducts");
                item.Hot = isHot;

                db.Products.Add(item);
                db.SaveChanges();
                return RedirectToAction("Product","Product");
            }
            else
            {
                var oldItem = db.Products.Where(x => x.ID == ID).FirstOrDefault();

                oldItem.Name = Name;
                oldItem.Description = Description;
                oldItem.Price = Price == "" || Price == null ? 0 : double.Parse(Price);
                oldItem.Discount = Price == "" || Price == null ? 0 : double.Parse(Discount);
                oldItem.Content = Content;
                oldItem.CategoryID = int.Parse(CategoryID);
                oldItem.Hot = isHot;
                if (formFile != null)
                    oldItem.Image = Images.ImgURL(formFile, "ImageProducts");

                db.Products.Update(oldItem);
                db.SaveChanges();
                return RedirectToAction("Product", "Product");
            }
        }

        public IActionResult Delete(int ID=0)
        {
            if (ID != 0)
            {
                var oldItem = db.Products.Where(x => x.ID == ID).FirstOrDefault();
                Images.RemoveImgURL(oldItem.Image, "ImageProducts");

                db.Products.Remove(oldItem);
                db.SaveChanges();
            }
            return RedirectToAction("Product", "Product");
        }
        #endregion
    }
}
