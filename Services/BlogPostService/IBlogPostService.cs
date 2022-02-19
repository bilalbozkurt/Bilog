using bilog.Models;

namespace bilog.Services.BlogPostService
{
    public interface IBlogPostService
    {
        Task<ServiceResponse<BlogPost>> GetSinglePostById(int id);
        Task<ServiceResponse<BlogPost>> GetSinglePostByLink(string link);
        Task<ServiceResponse<List<BlogPost>>> GetAllPosts();
        Task<ServiceResponse<List<BlogPost>>> SearchPosts(string searchInput);
    }
}