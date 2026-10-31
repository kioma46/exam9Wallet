using exam9Wallet.DAL.Models;

namespace exam9Wallet.BLL.Interfaces;

public interface ITransactionService : IService<Transaction>
{
    public Task<IEnumerable<Transaction>> GetAllSortedByDate(DateTime? from, DateTime? to);
}