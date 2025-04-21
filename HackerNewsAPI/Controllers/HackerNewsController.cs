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
        /// Getting top best stories
        /// </summary>
        /// <returns> returns best stories in response</returns>
       
        [HttpGet("TopNews")]
        public async Task<IActionResult> TopNews()
        {
            try
            {
                var response = await _hackerNewsService.BestStoriesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving top news stories", error = ex.Message });

            }
            
        }

    }
}
