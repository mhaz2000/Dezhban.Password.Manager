using Dezhban.ApplicationServices.Services.Users;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Dezhban.App.Components.Pages
{
    public partial class Home
    {
        [Inject] public IUserService _userService { get; set; } = default!;
        [Inject] public NavigationManager _navigationManager { get; set; } = default!;

        private bool _userExists;
        private bool _isLoading = true;
        private string _password = "";
        private string _errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            _userExists = await _userService.IsInitializedAsync();
            _isLoading = false;
        }

        private async Task CreateUser()
        {
            if (_password.Length < 8)
            {
                _errorMessage = "Password must contain at least 8 character.";
                return;
            }

            await _userService.InitializedAsync(_password);
            _errorMessage = "";
            _userExists = true;
            _password = "";
            StateHasChanged();
        }

        private async Task Login()
        {
            var loginResult = await _userService.LoginAsync(_password);
            if(loginResult)
            {
                _navigationManager.NavigateTo("/passwords");
            }
            else
            {
                _errorMessage = "Login Failed.";
            }
        }
    }
}
