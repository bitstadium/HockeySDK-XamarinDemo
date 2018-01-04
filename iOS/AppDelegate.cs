using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Runtime.InteropServices;

using HockeyApp.iOS;

using System.Threading.Tasks;

namespace HockeySDKXamarinDemo.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		const string AppID = "Your-App-ID";

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			var manager = BITHockeyManager.SharedHockeyManager;
			manager.Configure(AppID);
			manager.DebugLogEnabled = true;
			manager.LogLevel = BITLogLevel.Verbose;
			manager.CrashManager.CrashManagerStatus = BITCrashManagerStatus.AutoSend;
			manager.DisableMetricsManager = true;
			manager.StartManager();
			manager.Authenticator.AuthenticateInstallation(); // This line is obsolete in crash only builds

			global::Xamarin.Forms.Forms.Init();

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			LoadApplication(CreateApp());

			return base.FinishedLaunching(app, options);
		}

		private App CreateApp()
		{
			var app = new App();

			var ThrowNativeObjCExceptionButton = new Xamarin.Forms.Button {
				Text = "Throw native ObjC Exception"
			};
			ThrowNativeObjCExceptionButton.Clicked += ThrowNativeObjCException;

			app.AddChild(ThrowNativeObjCExceptionButton);

			return app;
		}
	
		[DllImport("libc")]
		private static extern int raise(int sig);

		private static void ThrowNativeObjCException(object sender, EventArgs e)
		{
			raise(6); // 6 == SIGABRT
		}
	}
}

