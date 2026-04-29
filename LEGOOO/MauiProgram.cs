using Microsoft.Extensions.Logging;
using SQLite;
using System.IO;
using Microsoft.Maui.Storage;

namespace LEGOOO;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif
		// 🟢 DATABASE PATH
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "lego.db");
		

        // 🟢 REGISTER SERVICES
        builder.Services.AddSingleton(new AppDatabase(dbPath));
        builder.Services.AddSingleton<LegoColorRepository>();
		builder.Services.AddSingleton<MainPage>();
		return builder.Build();
	}
}
