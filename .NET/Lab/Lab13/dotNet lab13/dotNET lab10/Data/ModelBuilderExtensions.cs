using dotNET_lab10.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace dotNET_lab10.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category(1, "Żywność"),
                new Category(2, "Zabawki"),
                new Category(3, "Narzędzia"),
                new Category(4, "Kwiaty")
                );

           /* modelBuilder.Entity<Article>().HasData(
                new Article()
                {
                    Id = 1,
                    Name = "Jabłko",
                    Price = 13.4,
                    CategoryId = 1
                   // Category = Categories.FirstOrDefaultAsync(x => x.Id == 1).Result,
                }
            );*/
        }
    }
}
