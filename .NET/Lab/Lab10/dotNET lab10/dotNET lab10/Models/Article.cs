using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace dotNET_lab10.Models
{
    public class Article
    {
        public int Id { get; set; } 
        public string Name{ get; set; }   
        public double Price { get; set; }
        public string Image { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Article()
        {
        }

        public Article(int id, string name, double price, string image, Category category)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
            Category = category;
        }

        public Article(int id, string name, double price, Category category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }

        public Article(string name, double price, string image, Category category)
        {
            Name = name;
            Price = price;
            Image = image;
            Category = category;
        }

        public Article(string name, double price, Category category)
        {
            Name = name;
            Price = price;
            Category = category;
        }
    }
}
