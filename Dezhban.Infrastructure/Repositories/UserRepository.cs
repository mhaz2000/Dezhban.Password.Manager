using Dezhban.Core.Entities;
using Dezhban.Core.Repositories;
using Dezhban.Infrastructure.Data;

namespace Dezhban.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}