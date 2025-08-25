using Dezhban.Core.Entities;
using Dezhban.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dezhban.ApplicationServices.Services.Users
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _repository;

        public PasswordService(IPasswordRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<PasswordModel>> GetPasswordsAsync()
        {
            return await _repository.GetMany(_ => true).ToListAsync();
        }
    }
}
