using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotNET_lab10.Data;
using dotNET_lab10.Models;

namespace dotNET_lab12.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly dotNET_lab10.Data.MyDbContext _context;

        public IndexModel(dotNET_lab10.Data.MyDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }

        public async Task OnGetAsync()
        {
            Article = await _context.Articles
                .Include(a => a.Category).ToListAsync();
        }
    }
}
