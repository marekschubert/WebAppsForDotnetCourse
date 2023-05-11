using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNET_lab10.Models
{
    public class ShopArticleViewModel
    {
        public IEnumerable<Article> articles;

        public string selectedListItem;

    }
}
