using HandHeldbB.Page;
using Microsoft.Extensions.Logging;


namespace HandHeldbB
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

            Routing.RegisterRoute("OverViewPage", typeof(OverViewPage));
            Routing.RegisterRoute("WhseReceiptListPage", typeof(WhseReceiptListPage));
            Routing.RegisterRoute("WhseReceiptPage", typeof(WhseReceiptPage));
           
  

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
