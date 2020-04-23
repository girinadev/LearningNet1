using System;
using System.Threading.Tasks;
using Voting.Logic.Models;

namespace Voting.Logic.Interfaces
{
  public interface IAnswersService
  {
    Task<AnswerModel[]> GetAnswersByQuestionAsync(Guid questionId);
    Task SaveAnswersAsync(Guid questionId, AnswerModel[] answers);
  }
}
