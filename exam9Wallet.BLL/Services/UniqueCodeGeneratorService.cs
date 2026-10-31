using exam9Wallet.DAL.Interfaces;

namespace exam9Wallet.BLL.Services;

public class UniqueCodeGeneratorService
{
    private readonly IUserRepository _userRepository;
    public UniqueCodeGeneratorService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<int> GenerateUniqueCodeAsync()
    {
        Random rnd = new Random();
        while (true)
        {
            int code = rnd.Next(100000, 1000000);
            if (!await _userRepository.UniqueCodeExists(code))
                return code;
        }
    }
}