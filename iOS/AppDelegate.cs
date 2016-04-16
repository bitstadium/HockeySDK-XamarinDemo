using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Runtime.InteropServices;

#if HOCKEYAPP
using HockeyApp;
#elif INSIGHTS
using Xamarin;
#endif
using System.Threading.Tasks;

namespace HockeySDKXamarinDemo.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
#if HOCKEYAPP
		const string AppID = "Your-App-ID";

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			var manager = BITHockeyManager.SharedHockeyManager;
			manager.Configure(AppID);
			manager.DebugLogEnabled = true;
			manager.StartManager();

			global::Xamarin.Forms.Forms.Init();

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			LoadApplication(CreateApp());

			return base.FinishedLaunching(app, options);
		}
#elif INSIGHTS
		const string AppID = "Your-App-ID";

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Insights.Initialize(AppID);

			global::Xamarin.Forms.Forms.Init();

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			LoadApplication(CreateApp());

			return base.FinishedLaunching(app, options);
		}
#endif

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

