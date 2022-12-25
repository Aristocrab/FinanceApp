using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinanceApp.Application.Common;
using FinanceApp.Application.Users.Commands.RegisterUser;
using FinanceApp.Application.Users.Queries.LoginUser;
using FinanceApp.Domain.Entities;
using FinanceApp.WebApi.Controllers.Shared;
using FinanceApp.WebApi.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FinanceApp.WebApi.Controllers;

[AllowAnonymous]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    private static string GetUserJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new("userId", user.Id.ToString())
        };

        var secretBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);
        var key = new SymmetricSecurityKey(secretBytes);
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            Constants.Issuer, 
            Constants.Audience, 
            claims,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials);

        var value = new JwtSecurityTokenHandler().WriteToken(token);

        return value;
    }
    
    [HttpPost("login")]
    public async Task<string> Login([FromBody] UserDto userDto)
    {
        var user = await _mediator.Send(new LoginUserCommand
        {
            Username = userDto.Username,
            Password = userDto.Password
        });

        return GetUserJwtToken(user);
    }
    
    [HttpPost("register")]
    public async Task<string> Register([FromBody] UserDto userDto)
    {
        var user = await _mediator.Send(new RegisterUserCommand
        {
            Username = userDto.Username,
            Password = userDto.Password
        });

        return GetUserJwtToken(user);
    }
}