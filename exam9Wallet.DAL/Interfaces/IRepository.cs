namespace exam9Wallet.DAL.Interfaces;

public interface IRepository<T> where T : class
{
    public Task Add(T entity);
    public Task Update(T entity);
    public Task Delete(T entity);
    public Task DeleteById(int id);
    public Task<T?> GetById(int id);
    public Task<IEnumerable<T>> GetAll();
}