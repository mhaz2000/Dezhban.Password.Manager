using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dezhban.App.Components.UI.Passwords
{
    public partial class DeletePasswordDialog
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string ContentText { get; set; }

        [Parameter]
        public string ButtonText { get; set; }

        [Parameter]
        public MudBlazor.Color Color { get; set; }

        private void Submit() => MudDialog.Close(DialogResult.Ok(true));

        private void Cancel() => MudDialog.Cancel();
    }
}
