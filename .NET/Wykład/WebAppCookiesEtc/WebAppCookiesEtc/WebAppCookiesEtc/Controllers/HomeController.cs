using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAppCookiesEtc.Models;

namespace WebAppCookiesEtc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetCookies()
        {
            SetCookie("short", "Short Cookie is active", 10);
            SetCookie("long", "Long Cookie is active", 60);
            SetCookie("veryLong", "Very long Cookie is active", 600);
            SetCookie("forSession", "Cookie for session is active");
            return View();
        }
        public IActionResult ShowCookies()
        {
            ViewData["short"] = Request.Cookies["short"];
            ViewData["long"] = Request.Cookies["long"];
            ViewData["veryLong"] = Request.Cookies["veryLong"];
            ViewData["forSession"] = Request.Cookies["forSession"];
            return View();
        }

        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("name", "Jane");
            HttpContext.Session.SetInt32("age", 25);
            HttpContext.Session.SetString("point", JsonConvert.SerializeObject( new Point(2,3)));
            return View();
        }

        public IActionResult ShowSession()
        {
            ViewData["name"] = HttpContext.Session.GetString("name");
            ViewData["age"] = HttpContext.Session.GetInt32("age");
            byte[] arr;
            HttpContext.Session.TryGetValue("point", out arr); // Point is a struct not an object
            if (arr != null)
            {
                Point p = JsonConvert.DeserializeObject<Point>(HttpContext.Session.GetString("point"));
                ViewData["x"] = p.X;
                ViewData["y"] = p.Y;
            }
            return View();
        }
        public void SetCookie(string key, string value, int? numberOfSeconds = null)
        {
            CookieOptions option = new CookieOptions();
            if (numberOfSeconds.HasValue)
                option.Expires = DateTime.Now.AddSeconds(numberOfSeconds.Value);
            Response.Cookies.Append(key, value, option);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
