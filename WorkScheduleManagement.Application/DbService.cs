using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities;
using WorkScheduleManagement.Data.Entities.Requests;

namespace WorkScheduleManagement.Application
{
    public class DbService : IDbRepository
    {
        private readonly AppDbContext _context;

        public DbService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get<T>() where T : class, IEntity
        {
            return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression) where T : class, IEntity
        {
            return _context.Set<T>().Where(expression).AsQueryable();
        }
        
        public async Task<T> Add<T>(T entity) where T : class, IEntity
        {
            var addedEntity = await _context.Set<T>().AddAsync(entity);
            return addedEntity.Entity;
        }

        public async Task<T> Update<T>(T entity) where T : class, IEntity
        {
            var updatedEntity = await Task.Run(() => _context.Set<T>().Update(entity));
            return updatedEntity.Entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}