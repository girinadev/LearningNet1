using Dapper;
using System;
using System.Threading.Tasks;
using Voiting.Repositories.Entities;
using Voiting.Repositories.Interfaces;

namespace Voiting.Repositories.Repositories
{
  public class DbUsersRepository : DbBaseRepository, IDbUsersRepository
  {
    public DbUsersRepository(DbConnectionSettings config) : base(config) { }

    public Task<DbUser> GetUserByEmailAsync(string email)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@email", email);

      return QuerySingle<DbUser>(@"select * 
                                    from[dbo].[Users]
                                    where[Email] = @email", parameters);
    }

    public Task<DbUser> GetUserByIdAsync(Guid id)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", id);

      return QuerySingle<DbUser>(@"select * 
                                    from[dbo].[Users]
                                    where[Id] = @id", parameters);
    }

    public Task<Guid> SaveUserAsync(DbUser dbUser)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", dbUser.Id);
      parameters.Add("@email", dbUser.Email);
      parameters.Add("@firstName", dbUser.FirstName);
      parameters.Add("@lastName", dbUser.LastName);
      parameters.Add("@password", dbUser.Password);

      return QuerySingle<Guid>(@"insert into [dbo].[Users] 
                                  (
                                    [Id],
                                    [Email],
                                    [FirstName],
                                    [LastName],
                                    [Password],
                                    [CreatedDate]
                                   )
                                   output inserted.Id
                                   values
                                   (
                                     @id,
                                     @email,
                                     @firstName,
                                     @lastName,
                                     @password,
                                     getutcdate()
                                   )", parameters);
    }
  }
}
