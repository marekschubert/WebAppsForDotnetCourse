using dotNET_lab9.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace dotNET_lab9.DataContext
{
    public class DictionaryDataContext : IDataContext
    {
        private Dictionary<int, Article> _articles = new Dictionary<int, Article>()
        {
            {1, new Article(1, "Papryka", 12.50, new System.DateTime(2022, 12, 9), Category.Warzywa) },
            {2, new Article(2, "Jabłko", 3.5, new System.DateTime(2022, 12, 15), Category.Owoce) },
            {3, new Article(3, "Jogurt", 2.5, new System.DateTime(2022, 12, 20), Category.Nabiał) }
        };
        public void AddArticle(Article article)
        {
            int nextId = 1;
            if (_articles.Count != 0) nextId = _articles.Max(s => s.Key) + 1;
            article.Id = nextId;
            _articles.Add(article.Id, article);
        }

        public Article GetArticle(int id)
        {
            return _articles[id];
        }

        public IEnumerable<Article> GetArticles()
        {
            return _articles.Select(s => s.Value);
        }

        public void RemoveArticle(int id)
        {
            _articles.Remove(id);
        }

        public void UpdateArticle(Article article)
        {
            _articles[article.Id] = article;
        }
    }
}
