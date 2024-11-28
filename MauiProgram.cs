﻿using Microsoft.Extensions.Logging;

namespace CobaSQLite
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
            // TODO: Add statements for adding PersonRepository as a singleton
            string dbPath = FileAccessHelper.GetLocalFilePath("category.db3");
            builder.Services.AddSingleton<CategoryRepository>(s => ActivatorUtilities.CreateInstance<CategoryRepository>(s, dbPath));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
