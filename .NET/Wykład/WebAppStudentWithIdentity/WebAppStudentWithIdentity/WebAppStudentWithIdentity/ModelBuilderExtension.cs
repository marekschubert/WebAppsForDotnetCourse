using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppStudentWithIdentity.Data;

namespace WebAppStudentWithIdentity
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    Id = 1,
                    Index = 123456,
                    Name = "Newman",
                    Gender = Gender.Male,
                    BirthDate = new DateTime(1999, 10, 10),
                    DepartmentId = 1,
                    Active = true,
                },
                new Student()
                {
                    Id = 2,
                    Index = 222222,
                    Name = "Yasmin",
                    Gender = Gender.Female,
                    BirthDate = new DateTime(2000, 2, 2),
                    DepartmentId = 2,
                    Active = false,
                }

                ); ;

        }
    }
}
