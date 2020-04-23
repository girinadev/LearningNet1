using System;
using System.Threading.Tasks;
using Voting.Logic.Models;

namespace Voting.Logic.Interfaces
{
  public interface IAccountService
  {
    Task<Guid> RegisterUserAsync(RegisterUserModel model);
    Task<UserModel> LoginUserAsync(LoginUserModel model);
  }
}
