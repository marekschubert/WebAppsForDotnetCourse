using System.Collections.Generic;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace dotNET_lab10.Models
{
    public class Cart
    {
        public Dictionary<int, int> allArticles { get; set; } //key: article.Id, value: quantity


        public void AddOneArticle(int id)
        {
            if(allArticles.ContainsKey(id))
            {
                allArticles[id]++;
            }
            else
            {
                allArticles.Add(id, 1);
            }
        }

        public void RemoveOneArticle(int id)
        {
            if (allArticles.ContainsKey(id))
            {
                if (allArticles[id] == 1)
                {
                    allArticles.Remove(id);
                    return;
                }
                allArticles[id]--;
            }
        }

        public string ToJson()
        {
            return "";
           // return JsonConvert.SerializeObject(this);
        }

    }
}
