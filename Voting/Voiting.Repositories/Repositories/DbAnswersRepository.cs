using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voiting.Repositories.Entities;
using Voiting.Repositories.Interfaces;

namespace Voiting.Repositories.Repositories
{
  public class DbAnswersRepository : DbBaseRepository, IDbAnswersRepository
  {
    public DbAnswersRepository(DbConnectionSettings config) : base(config) { }

    public Task<Guid> AddAnswerAsync(DbAnswer value)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", value.Id);
      parameters.Add("@text", value.Text);
      parameters.Add("@questionId", value.QuestionId);

      return QuerySingle<Guid>(@"insert into [dbo].[Answers] 
                                  (
                                    [Id],
                                    [Text],
                                    [QuestionId],
                                    [CreatedDate]
                                   )
                                   output inserted.Id
                                   values
                                   (
                                     @id,
                                     @text,
                                     @questionId,
                                     getutcdate()
                                   )", parameters);
    }

    public Task DeleteAnswerByIdAsync(Guid id)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", id);

      return Query<DbAnswer>(@"delete from [dbo].[Answers] 
	                              where [Id] = @id", parameters);
    }

    public Task<IEnumerable<DbAnswer>> GetAnswersByQuestionAsync(Guid id)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@questionId", id);

      return Query<DbAnswer>(@"select * from [dbo].[Answers] 
	                              where [QuestionId] = @questionId", parameters);
    }

    public Task UpdateAnswerAsync(DbAnswer value)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", value.Id);
      parameters.Add("@text", value.Text);

      return QuerySingle<long>("update [dbo].[Answers] set [Text] = @text, [UpdatedDate] = getutcdate() where[Id] = @id", parameters);
    }
  }
}
