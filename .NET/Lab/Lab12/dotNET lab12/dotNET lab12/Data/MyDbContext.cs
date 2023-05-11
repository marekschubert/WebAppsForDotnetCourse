using dotNET_lab10.Models;
using Microsoft.EntityFrameworkCore;

namespace dotNET_lab10.Data
{
    public class MyDbContext:DbContext
    {
        public string UploadFolderPath { get; set; }
        public string UploadFolder { get; set; }
        public string DefaultImagePath { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            UploadFolderPath = "wwwroot/upload/";
            UploadFolder = "upload/";
            DefaultImagePath = "image/noimage.png";
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
