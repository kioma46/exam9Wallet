namespace exam9Wallet.BLL.Interfaces;

public interface IService<T> where T : class
{
    public Task Add(T entity);
    public Task Update(T entity);
    public Task Delete(T entity);
    public Task DeleteById(int id);
    public Task<T?> GetById(int id);
    public Task<IEnumerable<T>> GetAll();

}