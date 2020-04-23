using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Voiting.Repositories.Interfaces;
using Voting.Logic.Interfaces;
using Voting.Logic.Models;
using AutoMapper;
using Voiting.Repositories.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Voting.Logic.Services
{
  public class QuestionsService : IQuestionsService
  {
    private readonly IVotesService _votesService;
    private readonly IDbQuestionsRepository _questionsRepository;
    private readonly IAnswersService _answersService;
    private readonly IDbUsersRepository _userRepository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IMapper _mapper;

    public QuestionsService(
      IDbQuestionsRepository questionsRepository,
      IAnswersService answersService,
      IDbUsersRepository userRepository,
      IVotesService votesService,
      ILogger<QuestionsService> logger,
      IMapper mapper)
    {
      _votesService = votesService;
      _questionsRepository = questionsRepository;
      _answersService = answersService;
      _userRepository = userRepository;
      _logger = logger;
      _mapper = mapper;
    }

    public async Task<QuestionModel> GetQuestionAsync(Guid id, Guid? userId = null)
    {
      try
      {
        var dbQuestion = await _questionsRepository.GetQuestionAsync(id);
        var question = _mapper.Map<QuestionModel>(dbQuestion);
        question.Answers = await _answersService.GetAnswersByQuestionAsync(id);
        question.User = _mapper.Map<UserModel>(await _userRepository.GetUserByIdAsync(dbQuestion.UserId));
        if (userId.HasValue)
        {
          question.User.CanVote = await _votesService.CheckIsUserCanVoteAsync(id, userId.Value);

          if (!question.User.CanVote)
          {
            await _votesService.LoadVotesAsync(id, question.Answers, question.User.Id.Equals(userId.Value));
          }
        }

        return question;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        return null;
      }
    }

    public async Task<QuestionModel[]> GetQuestionsAsync(SearchFilter searchFilter)
    {
      try
      {
        var dbQuestions = await _questionsRepository.GetQuestionsAsync((int?)searchFilter.Status, searchFilter.UserId, searchFilter.VotingEndDate, searchFilter.IsVoted);
        var questions = _mapper.Map<QuestionModel[]>(dbQuestions);

        foreach (var question in questions)
        {
          question.User = _mapper.Map<UserModel>(await _userRepository.GetUserByIdAsync(dbQuestions.First(q => q.Id.Equals(question.Id)).UserId));
        }

        return questions;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        return null;
      }
    }

    public async Task<Dictionary<string, string>> ValidateQuestionAsync(QuestionModel model)
    {
      var errors = new Dictionary<string, string>();

      if (model.Id != null)
      {
        var dbQuestion = await _questionsRepository.GetQuestionAsync(model.Id.Value);

        if (dbQuestion.Status != (int)QuestionStatuses.Private)
          errors.Add(nameof(model.Status), "Can't modify question. It is not 'Private'");
      }

      if (string.IsNullOrEmpty(model.Text))
        errors.Add(nameof(model.Text), "Question test is required.");

      if (model.Answers.Any(a => string.IsNullOrEmpty(a.Text)))
        errors.Add(nameof(model.Answers), "Answers test is required.");
            

      if (model.Status == QuestionStatuses.Public)
      {
        if (model.MaxVoteCount <= 0)
          errors.Add(nameof(model.MaxVoteCount), "Max votes count must be greate than 0.");

        if (model.Answers?.Count() < 2)
          errors.Add(nameof(model.Status), "Question must contains at least two answers.");

        if (model.MaxVoteCount > model.Answers?.Count())
          errors.Add(nameof(model.MaxVoteCount), "Max votes count must be less or equals answers count.");

        if (model.VotingEndDate?.Date < DateTime.Now.Date)
          errors.Add(nameof(model.VotingEndDate), "Invalid voting end date.");
              
        if (model.Type == QuestionTypes.Expandable)
        {
          if (model.MaxAnswersCount <= 0)
            errors.Add(nameof(model.MaxAnswersCount), "Question with type 'Expandable' must contains max answers count value.");

          if (model.MaxAnswersCount > model.MaxVoteCount)
            errors.Add(nameof(model.MaxAnswersCount), "Max own answers count must less or equals than max votes count.");
        }
        else
        {
          if (model.MaxAnswersCount > 0)
            errors.Add(nameof(model.MaxAnswersCount), "Question with type 'NotExpandable' can't contains max answers count value.");
        }
      }

      return errors;
    }

    public async Task<QuestionModel> SaveQuestionAsync(QuestionModel model)
    {
      try
      {
        if (model.Id != null)
        {
          var dbQuestion = await _questionsRepository.GetQuestionAsync(model.Id.Value);

          dbQuestion = _mapper.Map<DbQuestion>(model);

          await _questionsRepository.UpdateQuestionAsync(dbQuestion);
        }
        else
        {
          var dbQuestion = _mapper.Map<DbQuestion>(model);

          model.Id = await _questionsRepository.AddQuestionAsync(dbQuestion);
        }

        await _answersService.SaveAnswersAsync(model.Id.Value, model.Answers);

        return await GetQuestionAsync(model.Id.Value);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        return null;
      }
    }

    public Task DeleteQuestionAsync(Guid id)
    {
      try
      {
        return _questionsRepository.DeleteQuestionAsync(id);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        return null;
      }
    }
  }
}
