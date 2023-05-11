using Microsoft.AspNetCore.Mvc;
using System;

namespace dotNet_lab8.Controllers
{
    public class ToolController : Controller
    {
        static Result Calc(double a, double b, double c)
        {
            Result result = new Result();
            if(a == 0 && b == 0 && c == 0)
            {
                result.Solutions = 3;
                return result;
            }
            if (a == 0 && b == 0) return result;
            if (a == 0)
            {
                result.X[0] = (-c) / b;
                result.X[0] = Math.Round(result.X[0], 5);
                result.Solutions = 1;
                return result;
            }
            else
            {
                double delta = Math.Sqrt(b * b - 4 * a * c);
                if (!delta.Equals(double.NaN))
                {
                    double x1 = (-b - delta) / (2 * a);
                    double x2 = (-b + delta) / (2 * a);
                    result.X[0] = x1;
                    result.X[0] = Math.Round(result.X[0], 5);
                    result.Solutions = 1;
                    if (!(x1 == x2))
                    {
                        result.X[1] = x2;
                        result.X[1] = Math.Round(result.X[1], 5);
                        result.Solutions = 2;
                    }
                }
            }

            return result;
        }
        public IActionResult Solve(double a, double b, double c)
        {
            ViewBag.A = a;
            ViewBag.B = b;
            ViewBag.C = c;

            Result result = Calc(a, b, c);

            switch (result.Solutions)
            {
                case 0: 
                    ViewBag.Color = "red";
                    ViewBag.Result = "Brak rozwiązań";
                    break;
                case 1:
                    ViewBag.Color = "blue";
                    ViewBag.Result = $"Jedno rozwiązanie: {result.X[0]}";
                    break;
                case 2:
                    ViewBag.Color = "green";
                    ViewBag.Result = $"Dwa rozwiązania: {result.X[0]} oraz {result.X[1]}";
                    break;
                case 3:
                    ViewBag.Color = "yellow";
                    ViewBag.Result = "Równanie tożsamościowe";
                    break;
            }
            return View();
        }
    }

    class Result
    {
        private double[] x = { double.NaN, double.NaN };
        public double[] X { get => x; set => x = value; }
        public int Solutions { get => solutions; set => solutions = value; }

        private int solutions = 0;
        public Result()
        {

        }

    }
}
