using exam9Wallet.DAL.Models;

namespace exam9Wallet.BLL.Interfaces;

public interface IServiceProviderService
{
    public Task Add(ServiceProvider entity);
    public Task Update(ServiceProvider entity);
    public Task Delete(ServiceProvider entity);
    public Task DeleteById(int id);
    public Task<ServiceProvider> GetById(int id);
    public Task<IEnumerable<ServiceProvider>> GetAll();
}