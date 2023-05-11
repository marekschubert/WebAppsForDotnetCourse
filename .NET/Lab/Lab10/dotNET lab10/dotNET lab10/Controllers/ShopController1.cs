using dotNET_lab10.Data;
using dotNET_lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace dotNET_lab10.Controllers
{
    public class ShopController1 : Controller
    {
        private readonly MyDbContext _context;
        public ShopController1(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string selectedListItem)
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            var myDbContext = _context.Articles.Include(a => a.Category);
            return View(new ShopArticleViewModel { articles = myDbContext, selectedListItem = selectedListItem });

        }
    }
}
