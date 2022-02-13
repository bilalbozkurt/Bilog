using bilog.Services.BlogPostService;
using Microsoft.AspNetCore.Mvc;

namespace bilog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;

        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            return Ok(await _blogPostService.GetAllPosts());
        }
    }
}