using System;
using System.Threading.Tasks;
using Voting.Logic.Models;

namespace Voting.Logic.Interfaces
{
  public interface IVotesService
  {
    Task<VoteModel> SaveVoteAsync(VoteModel model);
    Task<bool> CheckIsUserCanVoteAsync(Guid id, Guid userId);

    Task LoadVotesAsync(Guid questionId, AnswerModel[] answers, bool withVotes);
  }
}
