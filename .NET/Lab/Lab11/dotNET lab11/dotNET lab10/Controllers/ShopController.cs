using dotNET_lab10.Data;
using dotNET_lab10.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace dotNET_lab10.Controllers
{
    public class ShopController : Controller
    {
        public const int WEEK = 3600 * 24 * 7;

        private readonly MyDbContext _context;
        public ShopController(MyDbContext context)
        {
            _context = context;
            //if (!HttpContext.Request.Cookies.ContainsKey("cart")) 
                //SetCookie("cart", new Cart().ToJson());
        }

       // [HttpPost("AddProductToCart/{id}")]
        public ActionResult AddArticleToCart(int id)
        {
            if(Request.Cookies.ContainsKey(CreateArticleCoookieKey(id)))
            {
                SetCookie(CreateArticleCoookieKey(id), (int.Parse(Request.Cookies[CreateArticleCoookieKey(id)]) + 1).ToString());
            }
            else
            {
                SetCookie(CreateArticleCoookieKey(id), "1");
            }
            return RedirectToAction(nameof(CartView));            
        }

        public ActionResult RemoveArticleFromCart(int id)
        {
            if (Request.Cookies.ContainsKey(CreateArticleCoookieKey(id)))
            {
                SetCookie(CreateArticleCoookieKey(id), (int.Parse(Request.Cookies[CreateArticleCoookieKey(id)]) - 1).ToString());
                if (Request.Cookies[CreateArticleCoookieKey(id)] == "1")
                {
                    Response.Cookies.Delete(CreateArticleCoookieKey(id));
                }
            }
            return RedirectToAction(nameof(CartView));
        }

        public IActionResult CartView()
        {
            var cookies = HttpContext.Request.Cookies;

            var articlesIds = cookies.Select(x => 
                                {
                                    var result = default(int);
                                    if(int.TryParse(x.Key.ToString().Remove(0, "article".Length), out result))
                                        return int.Parse(x.Key.ToString().Remove(0, "article".Length));
                                    return -1;
                                });

            var cartArticles = _context.Articles.Include(a => a.Category).Where(x => articlesIds.Contains(x.Id)).ToList();

            var cartArticlesWithQuantity = cartArticles.Select(x => new CartArticleViewModel
            {
                Article = x,
                Quantity = int.Parse(cookies["article" + x.Id])
            });

            return View(cartArticlesWithQuantity);
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
        public void SetCookie(string key, string value, int? numberOfSeconds = WEEK)
        {
            CookieOptions option = new CookieOptions();
            if (numberOfSeconds.HasValue)
                option.Expires = DateTime.Now.AddSeconds(numberOfSeconds.Value);
            Response.Cookies.Append(key, value, option);
        }

        public string CreateArticleCoookieKey(int id)
        {
            return "article" + id.ToString();
        }
    }
}
