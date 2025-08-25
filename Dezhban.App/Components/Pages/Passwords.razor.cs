using Dezhban.App.Components.UI.Passwords;
using Dezhban.ApplicationServices.Services.Users;
using Dezhban.Core.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Color = MudBlazor.Color;

namespace Dezhban.App.Components.Pages
{
    public partial class Passwords
    {
        [Inject] public IPasswordService _passwordService { get; set; } = default!;
        [Inject] public IDialogService _dialogService { get; set; } = default!;
        [Inject] private IJSRuntime JS { get; set; } = default!;

        private string _searchTerm = "";
        private List<PasswordModel> _passwords = new();
        private HashSet<Guid> _visiblePasswords = new(); // track visible ones

        private IEnumerable<PasswordModel> _filteredPasswords =>
            _passwords.Where(p =>
                string.IsNullOrWhiteSpace(_searchTerm)
                || p.Title.Contains(_searchTerm, StringComparison.OrdinalIgnoreCase)
                || p.Username.Contains(_searchTerm, StringComparison.OrdinalIgnoreCase));

        private string GetPasswordDisplay(PasswordModel model) =>
            _visiblePasswords.Contains(model.Id) ? model.Password : model.Password;

        private InputType GetPasswordInputType(PasswordModel model) =>
            _visiblePasswords.Contains(model.Id) ? InputType.Text : InputType.Password;

        private string GetPasswordIcon(PasswordModel model) =>
            _visiblePasswords.Contains(model.Id) ? Icons.Material.Filled.VisibilityOff : Icons.Material.Filled.Visibility;

        protected override async Task OnInitializedAsync()
        {

            // Load from service
            _passwords = await _passwordService.GetPasswordsAsync();
        }

        private void TogglePasswordVisibility(PasswordModel model)
        {
            if (_visiblePasswords.Contains(model.Id))
                _visiblePasswords.Remove(model.Id);
            else
                _visiblePasswords.Add(model.Id);
        }

        private async Task ConfirmDelete(PasswordModel model)
        {
            var parameters = new DialogParameters<DeletePasswordDialog>
        {
            { x=> x.ContentText, $"Are you sure you want to delete \"{model.Title}\"?" },
            { x=> x.ButtonText, "Delete" },
            { x=> x.Color, Color.Error }
        };

            var options = new DialogOptions { CloseOnEscapeKey = true };

            var dialog = await _dialogService.ShowAsync<DeletePasswordDialog>("Confirm Delete", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                DeletePassword(model);
            }
        }

        private void DeletePassword(PasswordModel model)
        {
            _passwords.Remove(model);
            // TODO: call repository/service to remove from DB
        }

        private async Task OpenUpdateDialog(PasswordModel existingPassword)
        {
            var parameters = new DialogParameters { ["Model"] = existingPassword };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = await _dialogService.ShowAsync<AddOrUpdatePasswordDialog>("Edit Password", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled && result.Data is PasswordModel updatedPassword)
            {
                // Replace in local list
                var index = _passwords.FindIndex(p => p.Id == updatedPassword.Id);
                if (index >= 0)
                    _passwords[index] = updatedPassword;

                // TODO: update DB
                StateHasChanged();
            }
        }

        private async Task OpenAddDialog()
        {
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            var dialog = await _dialogService.ShowAsync<AddOrUpdatePasswordDialog>("Add New Password", options);
            var result = await dialog.Result;

            if (!result.Canceled && result.Data is PasswordModel model)
            {
                //await _passwordService.AddPasswordAsync(model);
                _passwords.Add(model);
                StateHasChanged();
            }
        }

        private async Task CopyToClipboard(string text)
        {
            await JS.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
    }
}
