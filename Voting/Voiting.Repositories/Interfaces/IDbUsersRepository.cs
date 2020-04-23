using System;
using System.Threading.Tasks;
using Voiting.Repositories.Entities;

namespace Voiting.Repositories.Interfaces
{
  public interface IDbUsersRepository
  {
    Task<DbUser> GetUserByIdAsync(Guid id);

    Task<DbUser> GetUserByEmailAsync(string email);
    Task<Guid> SaveUserAsync(DbUser dbUser);
  }
}
