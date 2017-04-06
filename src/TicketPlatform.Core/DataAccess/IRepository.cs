using System.Linq;
using System.Threading.Tasks;

namespace TicketPlatform.Core.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query();

        Task<TEntity> GetById(params object[] id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
