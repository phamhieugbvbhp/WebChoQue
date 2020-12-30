using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChoQueVN.Models;
using ChoQueVN.Models.Globals;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ChoQueVN.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CheckLoginMiddleware
    {
        private readonly RequestDelegate _next;
        private DataContext db = new DataContext();

        public CheckLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private string GetIPAddress()
        {
            var IPAddress = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }

        private async Task abc(HttpContext httpContext)
        {
            httpContext.Session.SetString("Online", GetIPAddress());
            Online.online += 1;
            var visit = db.Statisticals.Where(x => x.ID == 1).FirstOrDefault();
            visit.Visit += 1;
            db.Statisticals.Update(visit);
            db.SaveChanges();
        }

        public Task Invoke(HttpContext httpContext)
        {
            ////Lấy đường dẫn url
            var path = httpContext.Request.Path.ToString();
            //nếu path bắt đầu bằng chữ/admin thì kiểm tra xem session đã tồn tại chưa => nếu chưa tồn tại thì di chuyển đến trang login
            path = path.ToLower();
            if (path != null && path.StartsWith("/admin"))
            {
                if (httpContext.Session.GetString("Account") == null)
                {
                    //di chuyển đến trang login
                    httpContext.Response.Redirect("/login");
                }
            }
            else if(path != null && !path.Equals("/admin") && !path.Equals("/login"))
            {
                //Thống kê người ghé vào web
                if (httpContext.Session.GetString("Online") != GetIPAddress())
                {
                    abc(httpContext);
                }
            }
            else
            {
                
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CheckLoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckLoginMiddleware>();
        }
    }
}
