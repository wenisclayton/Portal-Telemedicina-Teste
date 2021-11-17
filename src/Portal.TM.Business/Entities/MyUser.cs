using Microsoft.AspNetCore.Identity;
#pragma warning disable CS8618

namespace Portal.TM.Business.Entities;
public class MyUser : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public DateTime DateCreation { get; set; }
}
