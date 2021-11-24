using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portal.TM.Api.ViewModels;
using Portal.TM.Business.Entities;
using Portal.TM.Business.Interfaces;
using Portal.TM.Business.Notifications;
using Portal.TM.Business.Services;

namespace Portal.TM.Api.Controllers;

[Route("api/account")]
public class AuthController : MainBaseController
{

    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthController(
        UserManager<MyUser> userManager,
        INotificationHandler<DomainNotification> notifications,
        IDomainNotificationMediatorService mediator,
        IUserService userService,
        IMapper mapper) : base(notifications, mediator)
    {
        _userService = userService;
        _mapper = mapper;
    }


    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<ActionResult<MyUser>> Signin([FromBody] LoginUserViewModel loginUser)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return ModelStateErrorResponseError();
        }

        var user = await _userService.GetUser(loginUser.UserName, loginUser.Password);
        if (user != null)
        {
            var token = _userService.GetToken(user.UserName);
            return Ok(new { Token = token });
        }

        return ResponsePost(nameof(Signin), new {username = loginUser.UserName}, user);
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<ActionResult<MyUser>> Register([FromBody] RegisterUserViewModel registerUser)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return ModelStateErrorResponseError();
        }

        var user = await _userService.CreateUser(registerUser.FullName, registerUser.UserName, registerUser.Email, registerUser.Password);
        return ResponsePost(nameof(Register), new { username = registerUser.UserName }, user);
    }

    [HttpGet("")]
    public async Task<ActionResult<List<MyUser>>> GetAll([FromQuery] UserSearch search)
    {
        var lstProducts = await _userService.Query(search).ToListAsync();
        return ResponseGet(lstProducts);
    }
}