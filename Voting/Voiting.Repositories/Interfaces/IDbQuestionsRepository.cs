using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voiting.Repositories.Entities;

namespace Voiting.Repositories.Interfaces
{
  public interface IDbQuestionsRepository
  {
    Task<DbQuestion> GetQuestionAsync(Guid id);
    Task<IEnumerable<DbQuestion>> GetQuestionsAsync(int? status = null, Guid? userId = null, DateTime? votingEndDate = null, bool? isVoted = null);
    Task<Guid> AddQuestionAsync(DbQuestion value);

    Task UpdateQuestionAsync(DbQuestion value);
    Task DeleteQuestionAsync(Guid id);
  }
}
