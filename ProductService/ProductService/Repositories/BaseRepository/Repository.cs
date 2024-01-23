using Microsoft.EntityFrameworkCore;
using ProductService.Entities;
using System.Linq.Expressions;

namespace ProductService.Repositories.BaseRepository
{

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private DbSet<TEntity> _entities;
        protected Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        public bool Any(object id)
        {
            return _entities.Find(id) != null ? true : false;
        }

        public bool Any(Expression<Func<TEntity, bool>> where)
        {
            return _entities.Where(where) != null ? true : false;
        }

        public async Task<bool> AnyAsync(object id)
        {
            return await _entities.FindAsync(id) != null ? true : false;
        }


        public void Delete(object Id)
        {
            var entity = GetById(Id);
            if (entity == null) throw new Exception("");
            _entities.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new Exception("");
            _entities.Remove(entity);

        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = _entities.Where(where).AsEnumerable();
            _context.RemoveRange(objects);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return _entities.Where(where).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _entities.Where(where).FirstOrDefaultAsync();
        }

        public TEntity GetById(object Id)
        {
            return _entities.Find(Id);
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _entities.Where(where).AsEnumerable();
        }

        public async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _entities.Where(where).ToListAsync();
        }

        public void Insert(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
