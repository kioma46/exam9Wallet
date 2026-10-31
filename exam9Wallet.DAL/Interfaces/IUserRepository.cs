namespace exam9Wallet.DAL.Interfaces;

public interface IUserRepository
{
    Task<bool> UniqueCodeExists(int code);
}