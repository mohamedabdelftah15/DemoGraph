using DemoGraph.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoGraph.Repositories.Generics
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DemoGraphContext _context;
        readonly ILogger _log;
        public Repository(DemoGraphContext context,ILogger log)
        {
            _context = context;
            _log = log;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _context.Set<TEntity>();
            }
            catch (Exception ex)
            {
                _log.LogError($"Couldn't retrieve entities: {ex.Message}");
            }
            return null;
        }

        public TEntity GetById(int? id)
        {
            if (id == null)
            {
                _log.LogError($"id must not be null");
                return null;
            }
            try
            {
                return _context.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                _log.LogError($"This item could not be found: {ex.Message}");
            }
            return null;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                _log.LogError($"{nameof(AddAsync)} entity must not be null");
                return entity;
            }

            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                _log.LogError($"{nameof(entity)} could not be saved: {ex.Message}");
            }
            return null;
            
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                _log.LogError($"{nameof(AddRangeAsync)} entities must not be null");
                return false ;
            }

            try
            {
                await _context.Set<TEntity>().AddRangeAsync(entities);
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError($"{nameof(entities)} could not be saved: {ex.Message}");
            }
            return false;
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                _log.LogError($"{nameof(Update)} entity must not be null");
                return entity;
            }

            try
            {
                _context.Set<TEntity>().Update(entity);
                return entity;
            }
            catch (Exception ex)
            {
                _log.LogError($"{nameof(entity)} could not be updated: {ex.Message}");
            }
            return null;
        }

        public  bool UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                _log.LogError($"{nameof(UpdateRange)} entities must not be null");
                return false;
            }

            try
            {
                 _context.Set<TEntity>().UpdateRange(entities);
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError($"{nameof(entities)} could not be saved: {ex.Message}");
            }
            return false;
        }

        public bool Remove(TEntity entity)
        {
            if (entity == null)
            {
                _log.LogError($"{nameof(Remove)} entity must not be null");
                return false;
            }

            try
            {
                _context.Set<TEntity>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError($"{nameof(entity)} could not be removed: {ex.Message}");
            }
            return false;
        }

        public bool RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                _log.LogError($"{nameof(RemoveRange)} entity must not be null");
                return false;
            }

            try
            {
                _context.Set<TEntity>().RemoveRange(entities);
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError($"{nameof(entities)} could not be removed: {ex.Message}");
            }
            return true;
        }
    }
}
