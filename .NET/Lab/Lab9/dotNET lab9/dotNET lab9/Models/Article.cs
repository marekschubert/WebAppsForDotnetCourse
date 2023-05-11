using System;

namespace dotNET_lab9.Models
{
    public enum Category { Warzywa, Owoce, Nabiał, Słodycze, Wypieki, Mięso }
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Category Category { get; set; }

        public Article()
        {
        }

        public Article(int id, string name, double price, DateTime expiryDate, Category category)
        {
            Id = id;
            Name = name;
            Price = price;
            ExpiryDate = expiryDate;
            Category = category;
        }

        public Article(string name, double price, DateTime expiryDate, Category category)
        {
            Name = name;
            Price = price;
            ExpiryDate = expiryDate;
            Category = category;
        }
    }
}
