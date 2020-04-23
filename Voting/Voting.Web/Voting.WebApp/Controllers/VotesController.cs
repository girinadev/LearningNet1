using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Voting.Logic.Interfaces;
using Voting.Logic.Models;

namespace Voting.WebApp.Controllers
{
  [Route("api/votes")]
  [ApiController]
  public class VotesController : ControllerBase
  {
    private readonly IVotesService _votesService;

    public VotesController(IVotesService votesService)
    {
      _votesService = votesService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VoteModel model)
    {
      if (!model.AnswerId.HasValue && string.IsNullOrEmpty(model.AnswerText))
        return BadRequest("Answer id or text must be specified");

      model.User = new UserModel
      {
        Id = Guid.Parse(HttpContext.User.Identity.Name)
      };
      var result = await _votesService.SaveVoteAsync(model);
      return Ok(result);
    }
  }
}
