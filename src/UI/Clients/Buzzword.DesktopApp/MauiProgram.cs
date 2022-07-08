using Microsoft.AspNetCore.Components.WebView.Maui;
using Buzzword.Pages.Data;
using Buzzword.Components;

namespace Buzzword.DesktopApp;

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

		builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddScoped<ThemeInterop>();
        builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
