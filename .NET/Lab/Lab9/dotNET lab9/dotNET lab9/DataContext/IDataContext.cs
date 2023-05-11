using dotNET_lab9.Models;
using System.Collections.Generic;

namespace dotNET_lab9.DataContext
{
    public interface IDataContext
    {
        IEnumerable<Article> GetArticles();
        void AddArticle(Article article);
        void RemoveArticle(int id);
        void UpdateArticle(Article article);
        Article GetArticle(int id);
    }
}
