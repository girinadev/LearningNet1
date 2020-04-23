using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voiting.Repositories.Entities;
using Voiting.Repositories.Interfaces;

namespace Voiting.Repositories.Repositories
{
  public class DbVotesRepository : DbBaseRepository, IDbVotesRepository
  {
    public DbVotesRepository(DbConnectionSettings config) : base(config) { }

    public Task<Guid> AddVoteAsync(DbVote value)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", value.Id);
      parameters.Add("@answerId", value.AnswerId);
      parameters.Add("@userId", value.UserId);

      return QuerySingle<Guid>(@"insert into [dbo].[Votes] 
	                                (
	                                  [Id],
                                    [AnswerId],
	                                  [UserId],
                                    [CreatedDate]
	                                 )
	                                 output inserted.Id
	                                 values
	                                 (
	                                   @id,
	                                   @answerId,
	                                   @userId,
	                                   getutcdate()
	                                 )", parameters);
    }

    public Task<IEnumerable<DbVote>> GetVotesByQuestionIdAsync(Guid questionId)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@questionId", questionId);

      return Query<DbVote>(@"select 
		                        [dbo].[Votes].*,
		                        [dbo].[Answers].[QuestionId],
                            [dbo].[Users].[FirstName] as 'UserFirstName',
                            [dbo].[Users].[LastName] as 'UserLastName'
	                          from [dbo].[Votes] 
		                        inner join [dbo].[Answers] on [dbo].[Votes].[AnswerId] = [dbo].[Answers].[Id]
                            inner join [dbo].[Users] on [dbo].[Votes].[UserId] = [dbo].[Users].[Id]
	                          where [dbo].[Answers].[QuestionId] = @questionId", parameters);
    }

    public Task<IEnumerable<DbVote>> GetVotesByQuestionIdAndUserIdAsync(Guid questionId, Guid userId)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@questionId", questionId);
      parameters.Add("@userId", userId);

      return Query<DbVote>(@"select 
		                          [dbo].[Votes].*,
		                          [dbo].[Answers].[QuestionId]
	                          from [dbo].[Votes] 
		                          inner join [dbo].[Answers] on [dbo].[Votes].[AnswerId] = [dbo].[Answers].[Id]
	                          where [dbo].[Answers].[QuestionId] = @questionId and [dbo].[Votes].[UserId] = @userId", parameters);
    }
  }
}
