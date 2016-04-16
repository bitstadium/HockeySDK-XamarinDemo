using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace HockeySDKXamarinDemo
{
	public class App : Application
	{
		public App()
		{
			// The root page of your application
			MainPage = new TestPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		public void AddChild(View child)
		{
			(MainPage as TestPage).AddChild(child);
		}

		public void AddChildren(params View [] children)
		{
			(MainPage as TestPage).AddChildren(children);
		}
			
		public void AddChildren(IEnumerable<View> children)
		{
			(MainPage as TestPage).AddChildren(children);
		}
	}
}

