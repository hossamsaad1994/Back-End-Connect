using connect_.Models;

namespace connect_.Services
{
    public interface ICommentsSevices
    {
        Task<IEnumerable<Comments>> GetAll();
        Task<Comments> GetById(int id);
        Task<Comments> Add(Comments comment);
        Comments Update(Comments comment);
        Comments Delete(Comments comment);
    }
}
