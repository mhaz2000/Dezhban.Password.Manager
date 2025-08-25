using Dezhban.Core.Entities;

namespace Dezhban.ApplicationServices.Services.Users
{
    public interface IPasswordService
    {
        Task<List<PasswordModel>> GetPasswordsAsync();
    }
}
