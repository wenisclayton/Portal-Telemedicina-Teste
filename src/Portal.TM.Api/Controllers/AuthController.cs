using Microsoft.AspNetCore.Identity;
using Portal.TM.Api.ViewModels;
using Portal.TM.Business.Entities;
using Portal.TM.Business.Interfaces;
using Portal.TM.Business.Notifications;

namespace Portal.TM.Api.Controllers;

[Route("api/account")]
public class AuthController : MainBaseController
{
    private readonly SignInManager<MyUser> _signInManager;
    private readonly UserManager<MyUser> _userManager;

    public AuthController(
        INotificationHandler<DomainNotification> notifications,
        IDomainNotificationMediatorService mediator,
        SignInManager<MyUser> signInManager, 
        UserManager<MyUser> userManager) : base(notifications, mediator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("signin")]
    public async Task<ActionResult<MyUser>> Register([FromBody] RegisterUserViewModel registerUser)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return ModelStateErrorResponseError();
        }

        var newUser = new MyUser
        {
            FullName = registerUser.FullName,
            UserName = registerUser.UserName,
            Email = registerUser.Email,
            EmailConfirmed = true,
            DateCreation = DateTime.Now
        };
        var result = await _userManager.CreateAsync(newUser, registerUser.Password);


        return ResponsePost(nameof(Register), new { username = registerUser.UserName }, newUser);
    }
}