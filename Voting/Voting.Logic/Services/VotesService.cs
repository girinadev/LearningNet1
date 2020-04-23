using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Voiting.Repositories.Entities;
using Voiting.Repositories.Interfaces;
using Voting.Logic.Interfaces;
using Voting.Logic.Models;

namespace Voting.Logic.Services
{
  public class VotesService : IVotesService
  {
    private readonly IDbVotesRepository _votesRepository;
    private readonly IDbAnswersRepository _answersRepository;
    private readonly IDbQuestionsRepository _questionsRepository;
    private readonly ILogger<VotesService> _logger;
    private readonly IMapper _mapper;

    public VotesService(
       IDbQuestionsRepository questionsRepository,
      IDbVotesRepository votesRepository,
      IDbAnswersRepository answersRepository,
      ILogger<VotesService> logger,
      IMapper mapper)
    {
      _questionsRepository = questionsRepository;
      _votesRepository = votesRepository;
      _answersRepository = answersRepository;
      _logger = logger;
      _mapper = mapper;
    }

    public async Task<VoteModel> SaveVoteAsync(VoteModel model)
    {
      try
      {
        if (model.AnswerId == null)
        {
          var dbAnswer = _mapper.Map<DbAnswer>(new AnswerModel
          {
            QuestionId = model.QuestionId,
            Text = model.AnswerText
          });
          model.AnswerId = await _answersRepository.AddAnswerAsync(dbAnswer);
        }

        var dbVote = _mapper.Map<DbVote>(model);
        model.Id = await _votesRepository.AddVoteAsync(dbVote);

        return model;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        return null;
      }
    }

    public async Task<bool> CheckIsUserCanVoteAsync(Guid questionId, Guid userId)
    {
      try
      {
        var dbQuestion = await _questionsRepository.GetQuestionAsync(questionId);

        if (dbQuestion.Status != (int)QuestionStatuses.Public)
          return false;

        if (DateTime.Now.Date > dbQuestion.VotingEndDate?.Date)
          return false;

        var votes = await _votesRepository.GetVotesByQuestionIdAndUserIdAsync(questionId, userId);
        return !votes.Any();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        return false;
      }
    }

    public async Task LoadVotesAsync(Guid questionId, AnswerModel[] answers, bool withVotes)
    {
      try
      {
        var votes = await _votesRepository.GetVotesByQuestionIdAsync(questionId);
        var totalsByAnswers = votes.GroupBy(v => v.AnswerId).Select(r => new { AnswerId = r.Key, Total = r.Count() }).ToList();

        foreach (var answer in answers)
        {
          answer.Total = totalsByAnswers.FirstOrDefault(t => t.AnswerId.Equals(answer.Id))?.Total ?? 0;
          answer.Votes = withVotes ? _mapper.Map<VoteModel[]>(votes.Where(v => v.AnswerId.Equals(answer.Id))) : null;
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
      }
    }
  }
}
