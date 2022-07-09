using Microsoft.AspNetCore.Components.WebView.Maui;
using Buzzword.Pages.Data;
using Buzzword.Components;
using Buzzword.Application.Interfaces;
using Buzzword.Application.WebDomainServices;
using Buzzword.HttpPolly;
using Buzzword.DesktopApp.Services;

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
		builder.Services.AddHttpClient();
        builder.Services.AddSingleton<IHttpPollyConnection, HttpPollyConnection>();
        builder.Services.AddSingleton<HttpPollyClient>();
        builder.Services.AddSingleton<IUserService, UserService>();

		return builder.Build();
	}
}
