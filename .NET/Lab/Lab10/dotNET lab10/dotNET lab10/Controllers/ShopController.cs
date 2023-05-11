using dotNET_lab10.Data;
using dotNET_lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace dotNET_lab10.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDbContext _context;
        public ShopController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            var articles = _context.Articles.Include(a => a.Category).ToList();
            return View(articles);
        }
        
        [HttpPost]
        public IActionResult Index(string selectedCategoryValue)
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            
            if (string.IsNullOrEmpty(selectedCategoryValue))
            {
                var articles = _context.Articles.Include(a => a.Category);
                return View(articles);
            }
            
            var articlesPart = _context.Articles.Include(a => a.Category).Where(a => a.CategoryId == int.Parse(selectedCategoryValue));
            return View(articlesPart);
        }
    }
}
