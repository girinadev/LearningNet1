using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voiting.Repositories.Entities;

namespace Voiting.Repositories.Interfaces
{
  public interface IDbVotesRepository
  {
    Task<Guid> AddVoteAsync(DbVote dbVote);
    Task<IEnumerable<DbVote>> GetVotesByQuestionIdAsync(Guid id);
    Task<IEnumerable<DbVote>> GetVotesByQuestionIdAndUserIdAsync(Guid id, Guid userId);
  }
}
