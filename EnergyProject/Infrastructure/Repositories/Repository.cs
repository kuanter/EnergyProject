using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task<T>? GetById(string id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}