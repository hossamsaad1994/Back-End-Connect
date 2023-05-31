using connect_.Models;

namespace connect_.Services
{
    public interface IRolesServices
    {
        Task<IEnumerable<Role>> GetAll();
        Task<Role> GetById(int id);
        Task<Role> Add(Role role);
        Role Update(Role role);
        Role Delete(Role role);
    }
}
