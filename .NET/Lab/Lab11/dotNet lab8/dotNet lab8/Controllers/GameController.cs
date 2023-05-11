using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;

namespace dotNet_lab8.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Draw()
        {
            Random random = new Random();

            var range = HttpContext.Session.GetInt32("n");
            HttpContext.Session.SetInt32("randomNumber", random.Next((int)range));
            HttpContext.Session.SetInt32("count", 0);

            ViewBag.Range = range;
            return View();
        }
        public IActionResult Set(int n)
        {
            HttpContext.Session.SetInt32("n", n);
            ViewBag.Range = n;
            return View();
        }
        public IActionResult Guess(int guess)
        {
            IncreaseCount();
            var count = HttpContext.Session.GetInt32("count");
            var randomNumber = HttpContext.Session.GetInt32("randomNumber");
            if (guess < randomNumber)
            {
                ViewBag.Comment = $"Za mała liczba, próba nr {count}";
                ViewBag.Color = "blue";
            }
            else if (guess > randomNumber)
            {
                ViewBag.Comment = $"Za duża liczba, próba nr {count}";
                ViewBag.Color = "red";
            }
            else
            {
                ViewBag.Comment = $"Udało się! Liczba prób: {count}";
                ViewBag.Color = "green";
            }
            return View();
        }

        public void IncreaseCount()
        {
            var oldCount = HttpContext.Session.GetInt32("count");
            HttpContext.Session.SetInt32("count", (int)(oldCount + 1));
        }

    }
}
