using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

using HockeyApp.Android;
using HockeyApp.Android.Utils;

using System.Threading.Tasks;

namespace HockeySDKXamarinDemo.Droid
{
	[Activity(Label = "TestHockeyApp.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		public const string AppID = "Your-App-ID";

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			HockeyLog.LogLevel = 1;

			CrashManager.Register(this, AppID);
			UpdateManager.Register(this, AppID);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(CreateApp());
		}

		protected override void OnResume()
		{
			base.OnResume();

			//Tracking.StartUsage(this);
		}

		protected override void OnPause()
		{
			//Tracking.StopUsage(this);

			base.OnPause();
		}

		private App CreateApp()
		{
			var app = new App();

			var ThrowNativeJavaExceptionButton = new Xamarin.Forms.Button {
				Text = "Throw Native Java Exception"
			};
			ThrowNativeJavaExceptionButton.Clicked += ThrowNativeJavaException;

			app.AddChild(ThrowNativeJavaExceptionButton);

			return app;
		}

		public override bool OnPrepareOptionsMenu(IMenu menu) {
			try {
				// I am always getting menu.HasVisibleItems = false in my app
				if (menu != null && menu.HasVisibleItems) {
					// Exception is happening when the following code is executed
					var result = base.OnPrepareOptionsMenu(menu);
					return result;
				}
			}
			catch
			{
			}
			return true;
		}

		private void ThrowNativeJavaException(object sender, EventArgs e)
		{
			NativeJava.NativeJavaException.ThrowException("This is a native java exception.");
		}
	}
}

