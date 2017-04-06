using System;

using Xamarin.Forms;
using RestSharp;

namespace FormsCustom
{
	public class App : Application
	{
        public static string CLIENT_ID = "{CLIENT_ID}";
        public static string DOMAIN = "{DOMAIN}";
        public App()
		{
			MainPage = new LoginPage();
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
	}
}
