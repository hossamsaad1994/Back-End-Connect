using connect_.Models;

namespace Connect.Services
{
    public interface IAppuserServices
    {
        Task<IEnumerable<Appuser>> GetAll();
        Task<Appuser> GetById(int id);
        Task<Appuser> Add(Appuser user);
        Appuser Update(Appuser user);
        Appuser Delete(Appuser user);
    }
}
