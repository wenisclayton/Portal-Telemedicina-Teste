using AspNetCore.IQueryable.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Portal.TM.Api.Configuration;
using Portal.TM.Business.Entities;
using Portal.TM.Business.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Portal.TM.Business.Notifications;

namespace Portal.TM.Business.Services;
public class UserService : BaseService, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<MyUser> _userManager;
    private readonly SignInManager<MyUser> _signInManager;
    private readonly IDomainNotificationMediatorService _domainNotification;

    public UserService(
        IDomainNotificationMediatorService domainNotification,
        IUserRepository userRepository,
        UserManager<MyUser> userManager,
        SignInManager<MyUser> signInManager) : base(domainNotification)
    {
        _domainNotification = domainNotification;
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IQueryable<MyUser> Query(ICustomQueryable search)
    {
        return _userRepository.Queryable().Apply(search);
    }

    public async Task<MyUser?> GetUser(string loginUserName, string loginUserPassword)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUserName, loginUserPassword, false, true);
        if (result.Succeeded)
        {
            var user = (await _userRepository.SearchAsync(user => user.UserName == loginUserName)).FirstOrDefault();
            if (user == null) return null;

            await _signInManager.SignInAsync(user, false);
            return user;
        }

        _domainNotification.Notify(new DomainNotification("User", "User Not Found"));

        return null;
    }

    public string GetToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var responseToken = tokenHandler.WriteToken(token);
        return responseToken;
    }

    public async Task<MyUser?> CreateUser(string fullName, string userName, string email, string password)
    {
        var newUser = new MyUser
        {
            FullName = fullName,
            UserName = userName,
            Email = email,
            EmailConfirmed = true,
            DateCreation = DateTime.Now
        };
        var result = await _userManager.CreateAsync(newUser, password);
        if (result.Succeeded)
            return newUser;

        foreach (var identityError in result.Errors)
        {
            _domainNotification.Notify(new DomainNotification("CreateUser", $"Code:{identityError.Code} - Description: {identityError.Description}"));
        }
        
        
        return null;
    }
}

public interface IUserService
{
    public IQueryable<MyUser> Query(ICustomQueryable search);

    Task<MyUser?> GetUser(string loginUserUserName, string loginUserPassword);

    string GetToken(string username);
    Task<MyUser?> CreateUser(string fullName, string userName, string email, string password);
}
