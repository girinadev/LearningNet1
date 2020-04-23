using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voiting.Repositories.Entities;

namespace Voiting.Repositories.Interfaces
{
  public interface IDbAnswersRepository
  {
    Task<Guid> AddAnswerAsync(DbAnswer value);
    Task UpdateAnswerAsync(DbAnswer value);
    Task<IEnumerable<DbAnswer>> GetAnswersByQuestionAsync(Guid id);
    Task DeleteAnswerByIdAsync(Guid id);
  }
}
