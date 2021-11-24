using Portal.TM.Business.Entities;
using Portal.TM.Business.Interfaces;
using Portal.TM.Data.Context;

namespace Portal.TM.Data.Repository;

public class UserRepository : RepositoryEntityBase<MyUser>, IUserRepository
{
    public UserRepository(MyIdentityDbContext context) : base(context)
    {
    }
}
