using exam9Wallet.BLL.Interfaces;
using exam9Wallet.DAL.Interfaces;
using exam9Wallet.DAL.Models;

namespace exam9Wallet.BLL.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Add(Transaction entity)
    {
        await _repository.Add(entity);
    }

    public async Task Update(Transaction entity)
    {
        await _repository.Update(entity);
    }

    public async Task Delete(Transaction entity)
    {
        await  _repository.Delete(entity);
    }

    public async Task DeleteById(int id)
    {
        await _repository.DeleteById(id);
    }

    public async Task<Transaction?> GetById(int id)
    {
        Transaction? t = await _repository.GetById(id);
        if (t != null)
            return t;
        throw new KeyNotFoundException();
    }

    public async Task<IEnumerable<Transaction>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<IEnumerable<Transaction>> GetAllSortedByDate(DateTime? from, DateTime? to)
    {
        return await _repository.GetAllSortedByDate(from, to);
    }
}