using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Voiting.Repositories.Entities;
using Voiting.Repositories.Interfaces;
using Voting.Logic.Interfaces;
using Voting.Logic.Models;

namespace Voting.Logic.Services
{
  public class AccountService : IAccountService
  {
    private readonly IDbUsersRepository _usersRepository;
    private readonly ILogger<AccountService> _logger;
    private readonly IMapper _mapper;

    public AccountService(
      IDbUsersRepository usersRepository,
      ILogger<AccountService> logger,
      IMapper mapper)
    {
      _usersRepository = usersRepository;
      _logger = logger;
      _mapper = mapper;
    }

    public async Task<UserModel> LoginUserAsync(LoginUserModel model)
    {
      var user = await _usersRepository.GetUserByEmailAsync(model.Email)
        ?? throw new Exception("User does't exist.");

      var passwordHash = CreateHash(model.Password);
      if (!user.Password.Equals(passwordHash))
        throw new Exception("User email or password is invalid.");

      return _mapper.Map<UserModel>(user);
    }

    public async Task<Guid> RegisterUserAsync(RegisterUserModel model)
    {
      if (string.IsNullOrEmpty(model.Password?.Trim()) || string.IsNullOrEmpty(model.ConfirmPassword?.Trim()))
        throw new Exception("Password is required.");

      if (!model.Password.Equals(model.ConfirmPassword))
        throw new Exception("Password and confirmation not equals.");

      if (await _usersRepository.GetUserByEmailAsync(model.Email) != null)
        throw new Exception("User already exist.");

      try
      {

        var dbUser = new DbUser
        {
          Id = Guid.NewGuid(),
          Email = model.Email,
          FirstName = model.FirstName,
          LastName = model.LastName,
          Password = CreateHash(model.Password)
        };

        return await _usersRepository.SaveUserAsync(dbUser);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, ex);
        return Guid.Empty;
      }
    }
    private static string CreateHash(string password)
    {
      using (var mySHA256 = SHA256.Create())
      {        
        byte[] hashValue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Encoding.UTF8.GetString(hashValue);
      }
    }
  }
}
