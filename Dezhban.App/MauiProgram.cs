using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
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
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default); // Add this line


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
