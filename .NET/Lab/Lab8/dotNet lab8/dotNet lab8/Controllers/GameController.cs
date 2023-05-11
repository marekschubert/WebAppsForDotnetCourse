using Microsoft.AspNetCore.Mvc;
using System;

namespace dotNet_lab8.Controllers
{
    public class GameController : Controller
    {
        static int randomNumber = 0;
        static int range = 0;
        static int count = 0;
        public IActionResult Draw()
        {
            Random random = new Random();
            randomNumber = random.Next(range);
            count = 0;
            ViewBag.Range = range;
            return View();
        }
        public IActionResult Set(int n)
        {
            range = n;
            ViewBag.Range = n;
            return View();
        }
        public IActionResult Guess(int guess)
        {
            count++;
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
    }
}
