using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPlatform.Core.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public Repository(TicketContext context)
        {
            _context = context;
        }

        public virtual void Add(TEntity entity) => _context.Add(entity);

        public virtual void Remove(TEntity entity) => _context.Remove(entity);

        public virtual async Task<TEntity> GetById(params object[] id) => await _context.Set<TEntity>().FindAsync(id);

        public virtual IQueryable<TEntity> Query() => _context.Set<TEntity>();

        public virtual void Update(TEntity entity) => _context.Update(entity);
    }
}
