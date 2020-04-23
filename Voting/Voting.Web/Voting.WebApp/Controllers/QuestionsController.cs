using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voting.Logic.Interfaces;
using Voting.Logic.Models;

namespace VotingApp.Web.Controllers
{
  [Route("api/questions")]
  [ApiController]
  public class QuestionsController : ControllerBase
  {
    private readonly IQuestionsService _questionsService;

    public QuestionsController(IQuestionsService questionsService)
    {
      _questionsService = questionsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(DateTime? votingEndDate = null, bool? isVoted = null)
    {
      var searchFilter = new SearchFilter
      {
        Status = QuestionStatuses.Public,
        VotingEndDate = votingEndDate,
        IsVoted = isVoted,
        UserId = isVoted.HasValue && HttpContext.User.Identity.IsAuthenticated
        ? Guid.Parse(HttpContext.User.Identity.Name) 
        : new Guid?()
      };

      var result = await _questionsService.GetQuestionsAsync(searchFilter);
      return Ok(result);
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetByUser(QuestionStatuses? status = null, DateTime? votingEndDate = null, bool? isVoted = null)
    {
      var searchFilter = new SearchFilter
      {
        Status = status,
        VotingEndDate = votingEndDate,
        IsVoted = isVoted,
        UserId = Guid.Parse(HttpContext.User.Identity.Name)
      };

      var result = await _questionsService.GetQuestionsAsync(searchFilter);
      return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
      var result = await _questionsService.GetQuestionAsync(id, Guid.Parse(HttpContext.User.Identity.Name));

      if (result == null)
        NotFound();

      return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] QuestionModel model)
    {
      var errors = await _questionsService.ValidateQuestionAsync(model);
      if (errors.Any())
        return BadRequest(errors);

      model.User = new UserModel
      {
        Id = Guid.Parse(HttpContext.User.Identity.Name)
      };
      var result = await _questionsService.SaveQuestionAsync(model);
      return Ok(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] QuestionModel model)
    {
      var errors = await _questionsService.ValidateQuestionAsync(model);
      if (errors.Any())
        return BadRequest(errors);

      model.User = new UserModel
      {
        Id = Guid.Parse(HttpContext.User.Identity.Name)
      };
      var result = await _questionsService.SaveQuestionAsync(model);
      return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      await _questionsService.DeleteQuestionAsync(id);
      return NoContent();
    }
  }
}
