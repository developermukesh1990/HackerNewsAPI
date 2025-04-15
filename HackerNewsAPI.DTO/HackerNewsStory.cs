using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsAPI.DTO
{
    public class HackerNewsStory
    {
        public string? By { get; set; }
        public int Descendants { get; set; }
        public int Id { get; set; }
        public int Score { get; set; }
        public string? Time { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Url { get; set; }
    }
}
