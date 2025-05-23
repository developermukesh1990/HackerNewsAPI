﻿using HackerNewsAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsAPI.Service.Interfaces
{
    public interface IHackerNewsService
    {
        public Task<List<HackerNewsStory>> BestStoriesAsync();

        public Task<HttpResponseMessage> GetStoryByIdAsync(int id);

    }
}
