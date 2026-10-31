using exam9Wallet.DAL.Data;
using exam9Wallet.DAL.Interfaces;
using exam9Wallet.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet.DAL.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly WalletMobileContext _context;
    
    public TransactionRepository(WalletMobileContext context)
    {
        _context = context;
    }
    
    public async Task Add(Transaction entity)
    {
        await _context.Transactions.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Transaction entity)
    {
        _context.Transactions.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Transaction entity)
    {
        _context.Transactions.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Transaction?> GetById(int id)
    {
        return await _context.Transactions.FindAsync(id);
    }

    public async Task<IEnumerable<Transaction>> GetAll()
    {
        return await _context.Transactions.ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetAllSortedByDate(DateTime? from, DateTime? to)
    {
        var query = _context.Transactions.AsQueryable();

        if (from.HasValue)
        {
            var localStart = from.Value.Date;
            var utcStart = TimeZoneInfo.ConvertTimeToUtc(localStart, TimeZoneInfo.Local);
            query = query.Where(t => t.Date >= utcStart);
        }

        if (to.HasValue)
        {
            var localEnd = to.Value.Date.AddDays(1).AddTicks(-1);
            var utcEnd = TimeZoneInfo.ConvertTimeToUtc(localEnd, TimeZoneInfo.Local);
            query = query.Where(t => t.Date <= utcEnd);
        }

        return await query.ToListAsync();
    }
}