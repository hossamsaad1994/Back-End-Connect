using connect_.Models;
using Microsoft.EntityFrameworkCore;

namespace connect_.Services
{
    public class RolesServices : IRolesServices  
    {
        private readonly ApplicationDbContext _context;

        public RolesServices (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role> Add(Role role)
        {
            await _context.Roles.AddAsync(role);
            _context.SaveChanges();
            return (role);
        }

        public Role Delete(Role role)
        {
            _context.Remove(role);
            _context.SaveChanges();
            return (role);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public Role Update(Role role)
        {
            _context.Update(role);
            _context.SaveChanges();
            return (role);
        }
    }
}
