using Dezhban.ApplicationServices;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace Dezhban.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddApplication(Path.Combine(FileSystem.AppDataDirectory, "PM_App.db"));
            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddMudServices();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            app.Services.EnsureDatabaseMigrated();

            return app;
        }
    }
}
