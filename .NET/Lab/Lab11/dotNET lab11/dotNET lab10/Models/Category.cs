using Microsoft.CodeAnalysis.Operations;
using System.Collections.Generic;

namespace dotNET_lab10.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<Article> Articles { get; set; }

        public Category()
        {
        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Category(string name)
        {
            Name = name;
        }
    }
}
