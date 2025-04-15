using HackerNewsAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsAPI.Service.Services
{
    public class HackerNewsService: IHackerNewsService
    {
        private static HttpClient httpClient;
        static HackerNewsService()
        {
            httpClient = new HttpClient();
            
        }

        public async Task<HttpResponseMessage> BestStoriesAsync()
        {
            return await httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");

        }

        public async Task<HttpResponseMessage> GetStoryByIdAsync(int id)
        {
            return await httpClient.GetAsync(string.Format("https://hacker-news.firebaseio.com/v0/item/{0}.json", id));
        }
    }
}
