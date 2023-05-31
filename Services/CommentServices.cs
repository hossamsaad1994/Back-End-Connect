using connect_.Models;
using Microsoft.EntityFrameworkCore;

namespace connect_.Services
{
    public class CommentServices : ICommentsSevices
    {
        private readonly ApplicationDbContext _context;

        public CommentServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comments> Add(Comments comment)
        {
            await _context.AddAsync(comment);
            _context.SaveChanges();
            return (comment);
        }

        public Comments Delete(Comments comment)
        {
            _context.Remove(comment);
            _context.SaveChanges();
            return (comment);
        }

        public async Task<IEnumerable<Comments>> GetAll()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comments> GetById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public Comments Update(Comments comment)
        {
            _context.Update(comment);
            _context.SaveChanges();
            return (comment);
        }
    }
}
