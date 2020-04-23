using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voiting.Repositories.Entities;
using Voiting.Repositories.Interfaces;

namespace Voiting.Repositories.Repositories
{
  public class DbQuestionsRepository : DbBaseRepository, IDbQuestionsRepository
  {
    public DbQuestionsRepository(DbConnectionSettings config) : base(config) { }

    public Task DeleteQuestionAsync(Guid id)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", id);

      return QuerySingle<long>(@"delete 
	                                from [dbo].[Questions]
	                                where [Id] = @id", parameters);
    }

    public Task<DbQuestion> GetQuestionAsync(Guid id)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", id);

      return QuerySingle<DbQuestion>(@"select * 
                                      from[dbo].[Questions]
                                      where[Id] = @id", parameters);
    }

    public Task<IEnumerable<DbQuestion>> GetQuestionsAsync(int? status, Guid? userId = null, DateTime? votingEndDate = null, bool? isVoted = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@userId", userId);

      string whereQuery = "";

      if (isVoted != null)
      {
        var inNotIn = isVoted.Value ? "in" : "not in";
        whereQuery = @"[dbo].[Questions].[Id] " + inNotIn +
                      @"(
                          select [dbo].[Answers].[QuestionId] 
                          from [dbo].[Answers] 
                          inner join [dbo].[Votes] on [dbo].[Votes].[AnswerId] = [dbo].[Answers].[Id] and [dbo].[Votes].[UserId] = isnull(@userId, [dbo].[Votes].[UserId])
                      )";
      }
      else
      {
        whereQuery = @"[dbo].[Questions].[UserId] = isnull(@userId, [dbo].[Questions].[UserId])";
      }

      switch (status)
      {
        case 2:
          parameters.Add("@status", 1);
          parameters.Add("@votingEndDate", DateTime.Now.Date);
          whereQuery += @"and [dbo].[Questions].[Status] = @status and [dbo].[Questions].[VotingEndDate] >= @votingEndDate";
          break;
        case 3:
          parameters.Add("@status", 0);
          parameters.Add("@votingEndDate", DateTime.Now.Date);
          whereQuery += @"and ([dbo].[Questions].[Status] = @status or [dbo].[Questions].[VotingEndDate] < @votingEndDate)";
          break;
        default:
          parameters.Add("@status", status);
          parameters.Add("@votingEndDate", votingEndDate);
          whereQuery += @"and [dbo].[Questions].[Status] = isnull(@status, [dbo].[Questions].[Status])
                         and ([dbo].[Questions].[VotingEndDate] <= isnull(@votingEndDate, [dbo].[Questions].[VotingEndDate]) or [dbo].[Questions].[VotingEndDate] is null)";
          break; 
      }




      return Query<DbQuestion>(@"select [dbo].[Questions].*
                                  from [dbo].[Questions]
                                  where "
                                  + whereQuery +
                                  @" order by [dbo].[Questions].[CreatedDate] desc", parameters);
    }

    public Task<Guid> AddQuestionAsync(DbQuestion value)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", value.Id);
      parameters.Add("@text", value.Text);
      parameters.Add("@userId", value.UserId);
      parameters.Add("@status", value.Status);
      parameters.Add("@type", value.Type);
      parameters.Add("@maxVoteCount", value.MaxVoteCount);
      parameters.Add("@maxAnswersCount", value.MaxAnswersCount);
      parameters.Add("@votingEndDate", value.VotingEndDate);

      return QuerySingle<Guid>(@"insert into [dbo].[Questions] 
	                                (
	                                  [Id],
                                    [Text],
	                                  [UserId],
	                                  [Status],
	                                  [Type],
	                                  [MaxVoteCount],
	                                  [MaxAnswersCount],
	                                  [VotingEndDate],
                                    [CreatedDate]
	                                 )
	                                 output inserted.Id
	                                 values
	                                 (
	                                   @id,
	                                   @text,
	                                   @userId,
	                                   @status,
	                                   @type,
	                                   @maxVoteCount,
	                                   @maxAnswersCount,
	                                   @votingEndDate,
	                                   getutcdate()
	                                 )", parameters);
    }

    public Task UpdateQuestionAsync(DbQuestion value)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@id", value.Id);
      parameters.Add("@text", value.Text);
      parameters.Add("@status", value.Status);
      parameters.Add("@type", value.Type);
      parameters.Add("@maxVoteCount", value.MaxVoteCount);
      parameters.Add("@maxAnswersCount", value.MaxAnswersCount);
      parameters.Add("@votingEndDate", value.VotingEndDate);

      return QuerySingle<long>(@"update [dbo].[Questions]
	                                set
	                                  [Text] = @text,
	                                  [Status] = @status,
	                                  [Type] = @type,
	                                  [MaxVoteCount] = @maxVoteCount,
	                                  [MaxAnswersCount] = @maxAnswersCount,
	                                  [VotingEndDate] = @votingEndDate,
	                                  [UpdatedDate] = getutcdate()
                                    where [Id] = @id", parameters);
    }
  }
}
