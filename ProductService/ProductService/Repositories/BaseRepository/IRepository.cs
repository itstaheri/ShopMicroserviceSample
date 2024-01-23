using ProductService.Entities;
using System.Linq.Expressions;

namespace ProductService.Repositories.BaseRepository
{
    public interface IRepository <TEntity> where TEntity : BaseEntity
    {
        //-------Definition Public Functions Models-----------//
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);

        void Delete(object Id);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where); //lambda expression

        bool Any(object id);
        bool Any(Expression<Func<TEntity, bool>> where);

        TEntity GetById(object Id);
        IEnumerable<TEntity> GetAll();
        TEntity Get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);

        int SaveChanges();

        //---------------Async Functions----------------//
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
        Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        Task<bool> AnyAsync(object id);

        Task<int> SaveChangesAsync();
    }
}
