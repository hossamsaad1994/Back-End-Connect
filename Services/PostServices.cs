using connect_.Models;
using Microsoft.EntityFrameworkCore;

namespace connect_.Services
{
    public class PostServices : IPostsServices
    {
        private readonly ApplicationDbContext _context;

        public PostServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Post> Add(Post post)
        {
            await _context.Posts.AddAsync(post);
            _context.SaveChanges();
            return (post);
        }

        public Post Delete(Post post)
        {
            _context.Remove(post);
            _context.SaveChanges();
            return (post);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetById(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public Post Update(Post post)
        {
            _context.Update(post);
            _context.SaveChanges();
            return (post);
        }

    }
}
