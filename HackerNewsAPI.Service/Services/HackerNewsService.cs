using HackerNewsAPI.DTO;
using HackerNewsAPI.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsAPI.Service.Services
{
    public class HackerNewsService: IHackerNewsService
    {
        private HttpClient _httpClient;
        private IMemoryCache _cache;
       
        public HackerNewsService(IMemoryCache cache)
        {
            _cache=cache;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Get top 200 best stories
        /// </summary>
        /// <returns>returns list of stories</returns>
        public async Task<List<HackerNewsStory>> BestStoriesAsync()
        {
            var stories = new List<HackerNewsStory>();
            var response = await _httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
            if (response.IsSuccessStatusCode)
            {
                var storiesResponse = response.Content.ReadAsStringAsync().Result;
                var storyIds = JsonConvert.DeserializeObject<List<int>>(storiesResponse);
                var top200Stories = storyIds.OrderByDescending(x => x).ToList().Take(200);
                var tasks = top200Stories.Select(GetStoryAsync);
                stories = (await Task.WhenAll(tasks)).ToList();

            }

            return stories;

        }

        /// <summary>
        /// get stories by id
        /// </summary>
        /// <param name="id">int type parameter</param>
        /// <returns>returns story by id</returns>
        public async Task<HttpResponseMessage> GetStoryByIdAsync(int id)
        {
            return await _httpClient.GetAsync(string.Format("https://hacker-news.firebaseio.com/v0/item/{0}.json", id));
        }


        /// <summary>
        /// Gets data from cache response or create/set data in cache
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        private async Task<HackerNewsStory> GetStoryAsync(int storyId)
        {
            return await _cache.GetOrCreateAsync(storyId, async cacheEntry =>
            {
                try
                {
                    var response = await GetStoryByIdAsync(storyId);
                    if (response.IsSuccessStatusCode)
                    {
                        var storyResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<HackerNewsStory>(storyResponse) ?? new HackerNewsStory();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return new HackerNewsStory(); 
            });
        }

    }
}
