using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using HackerNewsAPI.Service.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsAPI.XUnitTests
{
    public class HackerNewsServiceTest
    {
        

        [Fact]
        public async void ShouldReturnPosts()
        {
            //var handlerMock = new Mock<HttpMessageHandler>();
            //var cache = new Mock<IMemoryCache>();
            //var response = new HttpResponseMessage
            //{
            //    StatusCode = HttpStatusCode.OK,
            //    Content = new StringContent(@"[{ ""id"": 1, ""title"": ""Cool post!""}, { ""id"": 100, ""title"": ""Some title""}]"),
            //};

            //handlerMock
            //  .Protected()
            //  .Setup<Task<HttpResponseMessage>>(
            //    "SendAsync",
            //    ItExpr.IsAny<HttpRequestMessage>(),
            //    ItExpr.IsAny<CancellationToken>())
            //  .ReturnsAsync(response);
            //var httpClient = new HttpClient(handlerMock.Object);
            //var posts = new HackerNewsService(cache);

            //var retrievedPosts = await posts.GetPosts();

            //Assert.NotNull(retrievedPosts);
            //handlerMock.Protected().Verify(
            //  "SendAsync",
            //  Times.Exactly(1),
            //  ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
            //  ItExpr.IsAny<CancellationToken>());

           

        }
    }
}
