using Microsoft.Extensions.Logging;
using MobileApp.PageModels;
using MobileApp.Pages;

namespace MobileApp
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ListPageModel>();
            builder.Services.AddSingleton<ListPage>();

            builder.Services.AddSingleton<EditPage>();
            builder.Services.AddSingleton<EditPageModel>();

            builder.Services.AddSingleton<AddPage>();
            builder.Services.AddSingleton<AddPageModel>();

            builder.Logging.AddDebug();
            return builder.Build();
        }
    }
}
