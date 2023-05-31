using connect_.Models;

namespace connect_.Services
{
    public interface IPostsServices
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> GetById(int id);
        Task<Post> Add(Post post);
        Post Update(Post post);
        Post Delete(Post post);
    }
}
