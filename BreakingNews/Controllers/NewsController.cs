using Microsoft.AspNetCore.Mvc;

namespace BreakingNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly UzNewsApiService _uzNewsApiService;

        public NewsController(UzNewsApiService uzNewsApiService)
        {
            _uzNewsApiService = uzNewsApiService;
        }

        [HttpGet("get-sports-news")]
        public async Task<IActionResult> GetSportsNews()
        {
            var news = await _uzNewsApiService.GetSportsNewsAsync();
            return Ok(news);
        }
    }
}
