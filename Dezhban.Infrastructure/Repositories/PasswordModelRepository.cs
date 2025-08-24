using Dezhban.Core.Entities;
using Dezhban.Core.Repositories;
using Dezhban.Infrastructure.Data;

namespace Dezhban.Infrastructure.Repositories;

public class PasswordRepository : Repository<PasswordModel>, IPasswordRepository
{
    public PasswordRepository(AppDbContext context) : base(context)
    {
    }
}
