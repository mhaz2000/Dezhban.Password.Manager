namespace Dezhban.ApplicationServices.Services.Users
{
    public interface IUserService
    {
        Task<bool> IsInitializedAsync();
        Task InitializedAsync(string password);
        Task<bool> LoginAsync(string password);
    }
}
