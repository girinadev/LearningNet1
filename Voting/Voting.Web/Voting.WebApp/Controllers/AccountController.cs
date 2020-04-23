using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voting.Logic.Interfaces;
using Voting.Logic.Models;

namespace VotingApp.Api.Controllers
{
  [Route("api/account")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
      _accountService = accountService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody]RegisterUserModel model)
    {
      try
      {
        var result = await _accountService.RegisterUserAsync(model);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserModel model)
    {
      try
      {
        var user = await _accountService.LoginUserAsync(model);

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Id.ToString()) };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return Ok(user);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [Authorize]
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return Ok();
    }
  }
}