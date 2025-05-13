using Foundation;
using Microsoft.Maui;
using ObjCRuntime;
using UIKit;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Platform;

namespace AppliLeCrocodile
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
