using exam9Wallet.DAL.Models;

namespace exam9Wallet.DAL.Interfaces;

public interface ITransactionRepository : IRepository<Transaction>
{
    public Task<IEnumerable<Transaction>> GetAllSortedByDate(DateTime? from, DateTime? to);
}