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
  public class AnswersService : IAnswersService
  {
    private readonly IDbAnswersRepository _answersRepository;
    private readonly ILogger<AnswersService> _logger;
    private readonly IMapper _mapper;

    public AnswersService(
      IDbAnswersRepository answersRepository,
      ILogger<AnswersService> logger,
      IMapper mapper)
    {
      _answersRepository = answersRepository;
      _logger = logger;
      _mapper = mapper;
    }

    public async Task SaveAnswersAsync(Guid questionId, AnswerModel[] answers)
    {
      try
      {
        var existedDbAnswers = await _answersRepository.GetAnswersByQuestionAsync(questionId);
        var forDeletionDbAnswers = existedDbAnswers.Where(a => !answers.Any(aa => a.Id.Equals(aa.Id))).ToList();

        foreach (var dbAnswer in forDeletionDbAnswers)
        {
          await _answersRepository.DeleteAnswerByIdAsync(dbAnswer.Id);
        }

        if (answers != null && answers.Any())
        {
          foreach (var answer in answers)
          {
            if (answer.Id.HasValue)
            {
              var dbAnswer = _mapper.Map<DbAnswer>(answer);
              await _answersRepository.UpdateAnswerAsync(dbAnswer);
            }
            else
            {
              answer.QuestionId = questionId;
              var dbAnswer = _mapper.Map<DbAnswer>(answer);
              await _answersRepository.AddAnswerAsync(dbAnswer);
            }
          }
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, ex);
      }
    }

    public async Task<AnswerModel[]> GetAnswersByQuestionAsync(Guid questionId)
    {
      try
      {
        return _mapper.Map<AnswerModel[]>(await _answersRepository.GetAnswersByQuestionAsync(questionId));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, ex);
        return null;
      }
    }
  }
}
