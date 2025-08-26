using Dezhban.Core.Entities;

namespace Dezhban.ApplicationServices.Services.Users
{
    public interface IPasswordService
    {
        Task AddPasswordAsync(PasswordModel model);
        Task<List<PasswordModel>> GetPasswordsAsync();
        Task UpdatePasswordAsync(PasswordModel model);
        Task DeletePasswordAsync(Guid id);
        Task<MemoryStream> GetPasswordsExcelBackupFileAsync();
    }
}
