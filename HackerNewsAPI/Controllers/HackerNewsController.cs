using HackerNewsAPI.DTO;
using HackerNewsAPI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Security.Principal;

namespace HackerNewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;
        private IMemoryCache _cache;
        public HackerNewsController(IHackerNewsService hackerNewsService, IMemoryCache cache) 
        {
            _hackerNewsService=hackerNewsService;
            _cache=cache;
        }

        /// <summary>
        /// Get best stories
        /// </summary>
        /// <returns> Best stories .</returns>
       
        [HttpGet("Index")]
        public async Task<IActionResult> Index(string? search = "")
        {
            try
            {
                List<HackerNewsStory> stories = new List<HackerNewsStory>();

                var response = await _hackerNewsService.BestStoriesAsync();
                if (response.IsSuccessStatusCode)
                {
                    var storiesResponse = response.Content.ReadAsStringAsync().Result;
                    var bestIds = JsonConvert.DeserializeObject<List<int>>(storiesResponse);

                    var tasks = bestIds.Select(GetStoryAsync);
                    stories = (await Task.WhenAll(tasks)).ToList();
                    var d = stories.Where(x => x.Url == null || x.Url == "");
                    if (!String.IsNullOrEmpty(search))
                    {
                        var searchItem = search.ToLower();
                        stories = stories.Where(x =>
                                                x.Title.ToLower().IndexOf(searchItem) > -1 || x.By.ToLower().IndexOf(searchItem) > -1)
                                           .ToList();

                    }
                }
                return Ok(stories);

            }
            catch (Exception ex)
            {
                return BadRequest("unable to process the request");
                
            }
            
        }

        private async Task<HackerNewsStory> GetStoryAsync(int storyId)
        {
            return await _cache.GetOrCreateAsync<HackerNewsStory>(storyId,
                async cacheEntry =>
                {
                    HackerNewsStory story = new HackerNewsStory();

                    var response = await _hackerNewsService.GetStoryByIdAsync(storyId);
                    if (response.IsSuccessStatusCode)
                    {
                        var storyResponse = response.Content.ReadAsStringAsync().Result;
                        story = JsonConvert.DeserializeObject<HackerNewsStory>(storyResponse);
                    }

                    return story;
                });
        }

    }
}
