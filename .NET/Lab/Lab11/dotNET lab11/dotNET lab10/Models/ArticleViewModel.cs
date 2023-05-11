using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNET_lab10.Models
{
	public class ArticleViewModel
	{
        public string Name { get; set; }
        public double Price { get; set; }
        public IFormFile FormFile { get; set; }
        public int CategoryId { get; set; }


    }
}
