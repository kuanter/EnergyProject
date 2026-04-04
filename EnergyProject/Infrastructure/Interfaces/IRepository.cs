namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T>? GetById(string id);
        public Task<List<T>> GetAll();

        public Task Create(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);
    }
}