using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Voiting.Repositories
{
  public abstract class DbBaseRepository
  {
    private readonly DbConnectionSettings _config;

    protected DbBaseRepository(DbConnectionSettings config)
    {
      _config = config;
    }

    protected async Task<IEnumerable<T>> Query<T>(string text, object parameter)
    {
      using (var db = new SqlConnection(this._config.ConnectionString))
      {
        await db.OpenAsync();
        return await db.QueryAsync<T>(text, parameter, commandType: CommandType.Text, commandTimeout: _config.CommandTimeout);
      }
    }

    protected async Task<T> QuerySingle<T>(string text, object parameter)
    {
      using (var db = new SqlConnection(this._config.ConnectionString))
      {
        await db.OpenAsync();
        return await db.QuerySingleOrDefaultAsync<T>(text, parameter, commandType: CommandType.Text, commandTimeout: _config.CommandTimeout);
      }
    }
  }
}
