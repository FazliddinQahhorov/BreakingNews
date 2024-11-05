using BreakingNews.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BreakingNews
{
    public class UzNewsApiService
    {
        private readonly HttpClient _httpClient;

        public UzNewsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://uznews.uz/");
        }

        public async Task<List<PostData>> GetSportsNewsAsync()
        {
            string endpoint = "_next/data/6dHLSj3UAx-n0inkR5aEA/uz/categories/sport.json?slug=sport";

            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode) return null;

            string jsonResponse = await response.Content.ReadAsStringAsync();

            var parsedJson = JObject.Parse(jsonResponse);
            List<PostData> posts = new List<PostData>();
            var chekCount = 0;

            // Parse the JSON response as JObject and navigate step-by-step
            for (int i = 0; i < 4; i++)
            {
                var postsData = parsedJson["pageProps"]["initialState"]["posts"]["posts"][i]["data"];

                // Access "result" and "posts" safe

                // Parse "posts" data into a list of Post objects

                foreach (var postGroup in postsData)
                {
                    chekCount++;
                    if (chekCount > 10) break;

                    posts.Add(new PostData
                    {
                        Image = postGroup["image"]?.ToString(),
                        Title = postGroup["title"]?.ToString(),
                        MetaDescription = postGroup["meta_description"]?.ToString(),
                        CreatedAt = postGroup["created_at"]?.ToString(),
                        ViewsCount = (int?)postGroup["views_count"] ?? 0,
                        Category = postGroup["category"]?["name"]?.ToString()
                    });

                }
            }

            return posts;
        }
    }
}
