using Dezhban.ApplicationServices.Services.Users;
using Dezhban.Core.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dezhban.App.Components.UI.Passwords
{
    public partial class AddOrUpdatePasswordDialog
    {
        [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public PasswordModel? Model { get; set; }   // incoming model for editing

        private MudForm _form = default!;
        private bool _show;
        private bool _isEdit => Model != null;
        private PasswordModel _model = new();

        protected override void OnInitialized()
        {
            // If editing, clone the incoming model so changes don't apply until Save
            _model = Model is null ? new PasswordModel() : new PasswordModel
            {
                Id = Model.Id,
                Title = Model.Title,
                Username = Model.Username,
                Password = Model.Password,
                AdditionalData = Model.AdditionalData
            };
        }

        private void Cancel() => MudDialog.Cancel();

        private async Task Save()
        {
            await _form.Validate();
            if (_form.IsValid)
                MudDialog.Close(DialogResult.Ok(_model));
        }
    }
}
