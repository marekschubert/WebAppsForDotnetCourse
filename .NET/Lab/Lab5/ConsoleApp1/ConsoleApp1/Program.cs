using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a, b, c;

            /*
            Console.WriteLine("Podaj a: ");
            a = Convert.ToDouble(Console.ReadLine()); //

            Console.WriteLine("Podaj b: ");
            b = Convert.ToDouble(Console.ReadLine()); //

            Console.WriteLine("Podaj c: ");
            c = Convert.ToDouble(Console.ReadLine()); //

            Result result = Calc(a, b, c);

            PrintResult(result);
            
            */
            double secondBiggest = FindSecondBiggestValue();
            if (!Double.IsNaN(secondBiggest)) Console.WriteLine(secondBiggest);
            else Console.WriteLine("Brak rozwiązania");

        }

        public static double FindSecondBiggestValue()
        {
            double tmp;
            double max1 = double.MinValue, max2 = double.MinValue;
            int n = 0;

            Console.WriteLine("Podaj n:");
            n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                tmp = Convert.ToDouble(Console.ReadLine());
                if(tmp > max1)
                {
                    max2 = max1;
                    max1 = tmp;
                }
                else if(tmp > max2 && tmp < max1)
                {
                    max2 = tmp;
                }
                //Console.WriteLine($"Max1: {max1}, max2: {max2}"); 
            }
            if (max2 == double.MinValue) return double.NaN;
            else return max2;
        }

        static void PrintResult(Result result)
        {
            if (result.Solutions == 0)
            {
                Console.WriteLine("Brak rozwiązań w zbiorze liczb rzeczywistych.");
            }
            else if (result.Solutions == 1)
            {
                Console.WriteLine($"Jedno rozwiązanie \nx0 = {result.X[0]}");
            }
            else if (result.Solutions == 2)
            {
                Console.WriteLine("Dwa rozwiązania \nx1 = {0}\nx2 = {1}", result.X[0], result.X[1]);
            }
        }
        static Result Calc(double a, double b, double c)
        {
            Result result = new Result();
            if(a == 0 && b == 0) return result;
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
    }
    internal class Result
    {
        private double[] x = { double.NaN, double.NaN };
        public double[] X { get => x; set => x = value; }
        public int Solutions { get => solutions; set => solutions = value; }

        private int solutions = 0;
        public Result()
        {

        }

    }

    internal class FindMax
    {
        public int v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20 = int.MinValue;
    }
}
