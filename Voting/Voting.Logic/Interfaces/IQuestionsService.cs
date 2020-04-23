using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Logic.Models;

namespace Voting.Logic.Interfaces
{
  public interface IQuestionsService
  {
    Task<QuestionModel[]> GetQuestionsAsync(SearchFilter searchFilter);
    Task<QuestionModel> GetQuestionAsync(Guid id, Guid? userId = null);
    Task DeleteQuestionAsync(Guid id);
    Task<QuestionModel> SaveQuestionAsync(QuestionModel model);
    Task<Dictionary<string, string>> ValidateQuestionAsync(QuestionModel model);
  }
}
