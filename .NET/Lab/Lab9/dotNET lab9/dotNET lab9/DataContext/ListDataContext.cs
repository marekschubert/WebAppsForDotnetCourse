using dotNET_lab9.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace dotNET_lab9.DataContext
{
    public class ListDataContext : IDataContext
    {
        List<Article> _articles = new List<Article>(){
                new Article(1, "Papryka", 12.50, new System.DateTime(2022, 12, 9), Category.Warzywa),
                new Article(2, "Jabłko", 3.5, new System.DateTime(2022, 12, 15), Category.Owoce),
                new Article(3, "Jogurt", 2.5, new System.DateTime(2022, 12, 20), Category.Nabiał)
        };

        public void AddArticle(Article article)
        {
            int nextId = 1;
            if(_articles.Count != 0) nextId = _articles.Max(s => s.Id) + 1;
            article.Id = nextId;
            _articles.Add(article);
        }

        public Article GetArticle(int id)
        {
            return _articles.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Article> GetArticles()
        {
            return _articles;
        }

        public void RemoveArticle(int id)
        {
            Article article = _articles.FirstOrDefault(s => s.Id == id);
            if(article != null)
                _articles.Remove(article);
        }

        public void UpdateArticle(Article article)
        {
            //Article articleToUpdate = articles.FirstOrDefault(s => s.Id == article.Id);
            _articles = _articles.Select(s => (s.Id == article.Id) ? article : s).ToList();
        }
    }
}
