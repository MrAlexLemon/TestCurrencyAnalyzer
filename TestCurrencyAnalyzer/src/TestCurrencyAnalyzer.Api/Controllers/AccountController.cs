using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Commands.Currency;
using TestCurrencyAnalyzer.Application.Commands.Identity;
using TestCurrencyAnalyzer.Application.Dtos;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Currency;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;
using TestCurrencyAnalyzer.Application.Queries.Currency;
using TestCurrencyAnalyzer.Domain.Entities.Identity.User;

namespace TestCurrencyAnalyzer.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

       

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInRequest request)
        {
            return Ok(await _mediator.Send(new SignInCommand(request.Email, request.Password)));
        }

        [HttpGet("user")]
        [Authorize]
        public ActionResult GetCurrentUser()
        {
            return Ok(new UserDto(User.Identity?.Name, User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty));
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity?.Name;
            await _mediator.Send(new LogoutCommand(userName));
            return Ok();
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var userName = User.Identity?.Name;
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            return Ok(await _mediator.Send(new RefreshTokenCommand(userName, role, accessToken, request.RefreshToken)));
        }

        [HttpPost("currencyInfo/request")]
        [Authorize]
        public async Task<IActionResult> CurrencyInfoFromApi([FromBody] CreateCurrencyExchangeRequest request)
        {
            var userName = User.Identity?.Name;
            var command = new CreateCurrencyExchangeCommand(userName, request.InputСurrency, request.OutputСurrency, request.Amount);
            return Ok(await _mediator.Send(command));
        }


        [HttpGet("exchangeCurrencyInfo")]
        [Authorize]
        public async Task<IActionResult> ExchangeCurrencyInfo([FromQuery] CurrencyParametersRequest request)
        {
            var userName = User.Identity?.Name;
            var query = new GetCurrencyExchangeInfoQuery(userName, request.PageNumber, request.PageSize);
            return Ok(await _mediator.Send(request));
        }
    }
}
