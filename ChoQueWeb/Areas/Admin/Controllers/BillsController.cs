using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChoQueVN.Models;
using ChoQueVN.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ChoQueVN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillsController : Controller
    {
        DataContext db = new DataContext();
        public IActionResult Index(int? page)
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

            var bills = db.Bills.OrderByDescending(x => x.ID).ToList();
            var ListBills = new List<BillsVM>();

            foreach(var item in bills)
            {
                var bill = new BillsVM();
                bill.ID = item.ID;
                bill.NameCus = item.NameCus;
                bill.Email = item.Email;
                bill.Phone = item.Phone;
                bill.Address = item.Address;
                bill.NameProduct = item.ProductID == 0 ? "" : db.Products.Where(x => x.ID == item.ProductID).FirstOrDefault().Name;
                ListBills.Add(bill);
            }

            return View("Index", ListBills.ToPagedList(pageNumber, pageSize));
        }
    }
}
