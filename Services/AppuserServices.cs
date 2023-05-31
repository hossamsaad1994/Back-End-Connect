using Connect.Services;
using connect_.Models;
using Microsoft.EntityFrameworkCore;

namespace connect_.Services
{
    public class AppuserServices : IAppuserServices
    {

        private readonly ApplicationDbContext _context;

        public AppuserServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Appuser> Add(Appuser appuser)
        {
            await _context.AddAsync(appuser);
            _context.SaveChanges();
            return (appuser);
        }

        public Appuser Delete(Appuser appuser)
        {
            _context.Remove(appuser);
            _context.SaveChanges();

            return appuser;
        }

        public async Task<IEnumerable<Appuser>> GetAll()
        {
            return await _context.Appusers.ToListAsync();

        }


        public async Task<Appuser> GetById(int id)
        {
            return await _context.Appusers.FindAsync(id);
        }

        public Appuser Update(Appuser appuser)
        {
            _context.Update(appuser);
            _context.SaveChanges();

            return appuser;
        }
    }
}
