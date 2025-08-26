using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dezhban.App.Components.UI.Passwords
{
    public partial class AdditionalDataDialog
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string ContentText { get; set; }

        private void Close() => MudDialog.Close();
    }
}
