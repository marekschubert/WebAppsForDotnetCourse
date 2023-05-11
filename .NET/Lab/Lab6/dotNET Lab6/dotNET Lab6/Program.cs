using System;

namespace dotNET_Lab6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ex1();
            Ex2();
            Ex3();
            Ex4();
            DrawCard("Ryszardo", "Rys", 'O', 3, 30);

            DrawCard("Tekst", borderChar: 'X', width: 40);

            int a = 1, b = 2, c = 3, d = 4, e = 5, f = 6, g = 7, h = 8, i = 9, j = 10, k = 11, l = 12, m = 13;
            double n = -1.2, o = 1.2, p = 2.3, q = 3.2, r = -4.3;
            string s1 = "a", s2 = "bbbbb", s3 = "cccccc", s4 = "dddddddddd", s5 = "eeeeeeeeee", s6 = "fff";
            bool b1, b2, b3, b4;
            b1 = b2 = b3 = b4 = true;
            Program program = new Program();
            Console.WriteLine(CountMyTypes(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s1, s2, s3, s4, s5, s6, b1, b2, b3, b4, program));
        }
        static void Ex1()
        {
            Console.WriteLine("\nEx1:\n");
            (string name, string surname, int age, double salary) person = ("Adam", "Mickiewicz", 12, 34.5);
            PrintTupleEx1(person);
        }
        static void PrintTupleEx1((string name, string surname, int age, double salary) person)
        {
            System.Console.WriteLine($"Imie {person.Item1} Nazwisko {person.Item2} wiek {person.Item3} płaca {person.Item4}");
            System.Console.WriteLine($"Imie {person.name} Nazwisko {person.surname} wiek {person.age} płaca {person.salary}");

            (var name, var surname, var age, var salary) = (person.name, person.surname, person.age, person.salary);
            System.Console.WriteLine($"Imie {name} Nazwisko {surname} wiek {age} płaca {salary}");
        }

        static void Ex2()
        {
            Console.WriteLine("\nEx2:\n");
            string @class = "Zmienna class";
            Console.WriteLine(@class);
        }
        static void Ex3()
        {
            Console.WriteLine("\nEx3:\n");
            int[] tab1 = new int[5] { 10, 15, 16, 8, 6 };

            Array.Sort(tab1); // 1
            Array.ForEach(tab1, n => Console.WriteLine(n)); // 2
            Array.Reverse(tab1); // 3
            Array.ForEach(tab1, n => Console.WriteLine(n));
            Console.WriteLine(Array.BinarySearch(tab1, 5)); // 4

            int[] tab2 = new int[3];
            Array.Copy(tab1, tab2, 3); // 6
            Array.ForEach(tab2, n => Console.WriteLine(n));

            Console.WriteLine(Array.IndexOf(tab2, 15));
        }
        static void Ex4()
        {
            Console.WriteLine("\nEx4:\n");
            var person = new { name = "Adam", surname = "Mickiewicz", age = 12, salary = 34.5 };
            PrintTupleEx4(person);
        }

        static void PrintTupleEx4(dynamic person)
        {
            //System.Console.WriteLine($"Imie {person.Item1} Nazwisko {person.Item2} wiek {person.Item3} płaca {person.Item4}");
            System.Console.WriteLine($"Imie {person.name} Nazwisko {person.surname} wiek {person.age} płaca {person.salary}");

            //(var name, var surname, var age, var salary) = (person.name, person.surname, person.age, person.salary);
            //System.Console.WriteLine($"Imie {name} Nazwisko {surname} wiek {age} płaca {salary}");
        }

        static void DrawCard(string line1, string line2 = "2. Linia", char borderChar = 'X', int borderWidth = 2, int width = 20)//„Ryszard”,”Rys”,”X”,2,20)
        {
            Console.WriteLine("\nEx5:\n");
            if (width < Math.Max(line1.Length, line2.Length) + 2 * borderWidth)
            {
                width = Math.Max(line1.Length, line2.Length) + 2 * borderWidth;
            }

            DrawBorderLines(borderChar, borderWidth, width);

            DrawCardLine(line1, borderChar, borderWidth, width);
            DrawCardLine(line2, borderChar, borderWidth, width);

            DrawBorderLines(borderChar, borderWidth, width);
        }

        static void DrawBorderLines(char borderChar, int borderWidth, int width)
        {
            for (int i = 0; i < borderWidth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(borderChar);
                }
                Console.WriteLine();
            }
        }
        static void DrawBorderSide(int count, char borderChar)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(borderChar);
            }
        }
        static void DrawCardLine(string line, char borderChar, int borderWidth, int width)
        {
            DrawBorderSide(borderWidth, borderChar);
            int spaceCountLine = (width - 2 * borderWidth - line.Length) / 2;
            DrawSpaces(spaceCountLine);
            Console.Write(line);
            if ((width % 2 == 0 && line.Length % 2 == 1) || (width % 2 == 1 && line.Length % 2 == 0))
                spaceCountLine++;
            DrawSpaces(spaceCountLine);
            DrawBorderSide(borderWidth, borderChar);
            Console.WriteLine();
        }
        static void DrawSpaces(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(" ");
            }
        }
  
        static (int countEvenInt, int countPositiveDouble, int countStrings, int countRest) CountMyTypes(params Object[] p)
        {
            Console.WriteLine("\nEx6:\n");
            int countEvenInt = 0, countPositiveDouble = 0, countStrings = 0, countRest = 0;
            foreach (var item in p)
            {
                switch (item)
                {
                    case int i:
                        //Console.WriteLine("1");
                        if(i % 2 == 0) countEvenInt++;
                        break;
                    case double d:
                        //Console.WriteLine("2");
                        if (d > 0) countPositiveDouble++;
                        break;
                    case string s:
                        //Console.WriteLine("3");
                        if(s.Length >= 5) countStrings++;
                        break;
                    default:
                        //Console.WriteLine("4");
                        countRest++;
                        break;
                }
            }
            return (countEvenInt, countPositiveDouble, countStrings, countRest);
        }
    }
}
