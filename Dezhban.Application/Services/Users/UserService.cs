using Dezhban.Core.Entities;
using Dezhban.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Dezhban.ApplicationServices.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task InitializedAsync(string password)
        {
            var passwordHasher = new PasswordHasher<User>();

            var user = new User()
            {
                IsInitialized = true
            };

            // Hash the password
            user.Password = passwordHasher.HashPassword(user, password);

            await _repository.AddAsync(user);
        }

        public async Task<bool> IsInitializedAsync()
        {
            var user = await _repository.GetAsync(_ => true);

            return user?.IsInitialized ?? false;
        }
    }
}
