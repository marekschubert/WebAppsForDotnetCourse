using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace dotNET_lab7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateNGroups(4);
            SortTopics();
            SortTopics2();
            TransformStudents();
            Ex4();
        }

        static void SortTopics()
        {
            Console.WriteLine("\nEx2");
            var groupedTopics = Generator.GenerateStudentsWithTopicsEasy()
                                .SelectMany(x => x.Topics)
                                .GroupBy(x => x)
                                .Select(x => new { Topic = x.Key, Count = x.Count() })
                                .OrderByDescending(x => x.Count);
            
            foreach (var group in groupedTopics)
            {
                Console.WriteLine($"{group.Topic} {group.Count}");               
            }
        }

        static void SortTopics2()
        {
            Console.WriteLine("\nEx2");
            var groupedTopics = Generator.GenerateStudentsWithTopicsEasy()
                                .SelectMany(x => x.Topics, (x, y) => new {val = x.Gender, topics = y})
                                .GroupBy(x => new {x.val, x.topics})
                                .Select(x => new {Gender = x.Key.val, topic = x.Key.topics, Count = x.Count()})
                                //.Select(x => new {Gender = x.Gender, Topics = x.Topics.SelectMany(x => x).ToList()})
                               // .GroupBy(x => x.Topics)
                                //.Select(x => new { Topic = x.Key, Count = x.Count() })
                                .OrderBy(x => x.Gender)
                                .ThenByDescending(x => x.Count)
                                .ToList();

            groupedTopics.ForEach(x => Console.WriteLine(x));           
        }
        static void CreateNGroups(int n)
        {
            Console.WriteLine("\nEx1");
            var groupedStudents = Generator.GenerateStudentsWithTopicsEasy()
                                  .OrderBy(x => x.Name)
                                  .ThenBy(x => x.Index)
                                  .Select((v, i) => new { Value = v, IndexGroup = i / n })
                                  .GroupBy(i => i.IndexGroup, v => v.Value);
            foreach (var group in groupedStudents)
            {
                Console.WriteLine(group.Key);
                group.ToList().ForEach(s => Console.WriteLine(" " + s));
            }
           

          // groupedStudents.ToList().ForEach(x => Console.WriteLine(x));
        }
        static int MapTopicStrToInt(List<Topic> topics, string name)
        {
            foreach (var topic in topics)
            {
                if (topic.Name == name)
                    return topic.Id;
            }
            return -1;
        }
        static void TransformStudents()
        {
            Console.WriteLine("\nEx3");
            var studentsWithTopics = Generator.GenerateStudentsWithTopicsEasy();
            var topics = studentsWithTopics
                         .SelectMany(x => x.Topics)
                         .Distinct()
                         .Select((x, i) => new { Value = x, Id = i })
                         .Select(x => new Topic()
                         {
                             Id = x.Id,
                             Name = x.Value
                         })
                         .ToList();
            /* var students = studentsWithTopics
                            .Select(x => new Student()
                            {   Id = x.Id,
                                Index = x.Index,
                                Active = x.Active,
                                DepartmentId = x.DepartmentId,
                                Gender = x.Gender,
                                Name = x.Name,
                                TopicsId = x.Topics.ConvertAll(x => MapTopicStrToInt(topics, x))
                            });
            */

            var students = studentsWithTopics
                           .Select(x => new Student()
                           {
                               Id = x.Id,
                               Index = x.Index,
                               Active = x.Active,
                               DepartmentId = x.DepartmentId,
                               Gender = x.Gender,
                               Name = x.Name,
                               TopicsId = x.Topics.Join(topics,
                                                        topicStr => topicStr,
                                                        topicClass => topicClass.Name,
                                                        (x, topicClass) => new { topicName = x, id = topicClass.Id })
                                                        .Select(x => x.id)
                                                        .ToList()
                           });
            students.ToList().ForEach(x => Console.WriteLine(x));
        }

        static void Ex4()
        {
            Console.WriteLine("\nEx4");
            //Topic topic = new Topic();

            Type t = Type.GetType("dotNET_lab7.Topic");
            Object topic = Activator.CreateInstance(t);

            MethodInfo info = topic.GetType().GetMethod("Print", new Type[] { typeof(string) });
            info.Invoke(topic, new object[] { "rty"});

            info = topic.GetType().GetMethod("Print", new Type[] { typeof(string), typeof(int) });
            info.Invoke(topic, new object[] {"qwe", 3});

        }
    }
    
    

    public class Topic
    {
        public int Id { get; set; }   
        public string Name { get; set; }
        public Topic(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Topic()
        {
        }

        public void Print(string com)
        {
            Console.WriteLine($"Print: {com}");
        }
        public void Print(string com, int n)
        {
            Console.WriteLine(n);
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Print: {com}");
            }
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<int> TopicsId { get; set; }
        public Student(int id, int index, string name, Gender gender, bool active,
            int departmentId, List<int> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.TopicsId = topics;
        }

        public Student()
        {
        }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in TopicsId)
                result += str + ", ";
            return result;
        }


    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }

        public Person(string name, int age, bool isActive)
        {
            Name = name;
            Age = age;
            IsActive = isActive;
        }

    }


    public class Department
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public Department(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return $"{Id,2}), {Name,11}";
        }

    }

    public enum Gender
    {
        Female,
        Male
    }

    public class StudentWithTopics
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<string> Topics { get; set; }
        public StudentWithTopics(int id, int index, string name, Gender gender, bool active,
            int departmentId, List<string> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }

    public static class Generator
    {
        public static int[] GenerateIntsEasy()
        {
            return new int[] { 5, 3, 9, 7, 1, 2, 6, 7, 8 };
        }

        public static int[] GenerateIntsMany()
        {
            var result = new int[10000];
            Random random = new Random();
            for (int i = 0; i < result.Length; i++)
                result[i] = random.Next(1000);
            return result;
        }

        public static List<string> GenerateNamesEasy()
        {
            return new List<string>() {
                "Nowak",
                "Kowalski",
                "Schmidt",
                "Newman",
                "Bandingo",
                "Miniwiliger"
            };
        }
        public static List<StudentWithTopics> GenerateStudentsWithTopicsEasy()
        {
            return new List<StudentWithTopics>() {
            new StudentWithTopics(1,12345,"Nowak", Gender.Female,true,1,
                    new List<string>{"C#","PHP","algorithms"}),
            new StudentWithTopics(2, 13235, "Kowalski", Gender.Male, true,1,
                    new List<string>{"C#","C++","fuzzy logic"}),
            new StudentWithTopics(3, 13444, "Schmidt", Gender.Male, false,2,
                    new List<string>{"Basic","Java"}),
            new StudentWithTopics(4, 14000, "Newman", Gender.Female, false,3,
                    new List<string>{"JavaScript","neural networks"}),
            new StudentWithTopics(5, 14001, "Bandingo", Gender.Male, true,3,
                    new List<string>{"Java","C#"}),
            new StudentWithTopics(6, 14100, "Miniwiliger", Gender.Male, true,2,
                    new List<string>{"algorithms","web programming"}),
            new StudentWithTopics(11,22345,"Nowak", Gender.Female,true,2,
                    new List<string>{"C#","PHP","algorithms"}),
            new StudentWithTopics(12, 23235, "Nowak", Gender.Male, true,1,
                    new List<string>{"C#","C++","fuzzy logic"}),
            new StudentWithTopics(13, 23444, "Schmidt", Gender.Male, false,1,
                    new List<string>{"Basic","Java"}),
            new StudentWithTopics(14, 24000, "Newman", Gender.Female, false,1,
                    new List<string>{"JavaScript","neural networks"}),
            new StudentWithTopics(15, 24001, "Bandingo", Gender.Male, true,3,
                    new List<string>{"Java","C#"}),
            new StudentWithTopics(16, 24100, "Bandingo", Gender.Male, true,2,
                    new List<string>{"algorithms","web programming"}),
            };
        }
        public static List<Department> GenerateDepartmentsEasy()
        {
            return new List<Department>() {
            new Department(1,"Computer Science"),
            new Department(2,"Electronics"),
            new Department(3,"Mathematics"),
            new Department(4,"Mechanics")
            };
        }

    }

}
