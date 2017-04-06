using System;

using Xamarin.Forms;
using RestSharp;
using Newtonsoft.Json;
using Auth0.SDK;
#if __IOS__
using UIKit;
#endif
#if __ANDROID__
using Android.Content;
#endif


namespace FormsCustom
{
	public class LoginPage : ContentPage
	{
        Auth0Client auth0 = new Auth0Client(App.DOMAIN, App.CLIENT_ID);

		public LoginPage()
		{
			var email = new Entry { Placeholder = "Email" };
			var password = new Entry { Placeholder = "Password", IsPassword = true };

			var loginWithAPIButton = new Button { Text = "Sign In with API" };
			var loginWithSDKButton = new Button { Text = "Sign In with SDK" };

			loginWithAPIButton.Clicked += (object sender, EventArgs e) =>
			{
				LoginWithAPI(email.Text, password.Text);
			};

			loginWithSDKButton.Clicked += (object sender, EventArgs e) =>
			{
				LoginWithSDK(email.Text, password.Text);
			};

			var facebookLoginButton = new Button{ Text = "Login with Facebook" };

			facebookLoginButton.Clicked += (object sender, EventArgs e) =>
			{
				LoginWithFacebook();
			};

			Content = new StackLayout
			{
				Padding = 30,
				Spacing = 10,
				Children = {
					new Label { Text = "Traditional Login" },
					email,
					password,
					loginWithAPIButton,
					loginWithSDKButton,
					new Label { Text = "Social Login" },
					facebookLoginButton
				}
			};
		}

		public async void LoginWithSDK(string username, string password)
		{
			var user = await auth0.LoginAsync("Username-Password-Authentication", username, password);

			Application.Current.Properties["id_token"] = user.IdToken;
			Application.Current.Properties["access_token"] = user.Auth0AccessToken;

			Console.WriteLine(user.Profile);

			Application.Current.Properties["name"] = user.Profile["name"].ToString();
			Application.Current.Properties["email"] = user.Profile["email"].ToString();
			Application.Current.Properties["picture"] = user.Profile["picture"].ToString();

			await Navigation.PushModalAsync(new HomePage());
		}

		public void LoginWithAPI(string username, string password)
		{
			var client = new RestClient(App.DOMAIN);
			var request = new RestRequest("oauth/ro", Method.POST);
			request.AddParameter("client_id", App.CLIENT_ID);
			request.AddParameter("username", username);
			request.AddParameter("password", password);
			request.AddParameter("connection", "Username-Password-Authentication");
			request.AddParameter("grant_type", "password");
			request.AddParameter("scope", "openid");

			IRestResponse response = client.Execute(request);

			LoginToken token = JsonConvert.DeserializeObject<LoginToken>(response.Content);

			if (token.id_token != null)
			{
				Application.Current.Properties["id_token"] = token.id_token;
				Application.Current.Properties["access_token"] = token.access_token;
				GetUserData(token.id_token);
			}
			else {
				DisplayAlert("Oh No!", "It's seems that you have entered an incorrect email or password. Please try again.", "OK");
			};
		}

		public async void LoginWithFacebook()
		{
			#if __IOS__
				var window = UIApplication.SharedApplication.KeyWindow;
				var ui = window.RootViewController;
				while (ui.PresentedViewController != null)
				{
					ui = ui.PresentedViewController;
				}
			#endif
			#if __ANDROID__
				Context ui = Xamarin.Forms.Forms.Context;
			#endif
			var user = await auth0.LoginAsync(ui, "facebook");

			Application.Current.Properties["id_token"] = user.IdToken;
			Application.Current.Properties["access_token"] = user.Auth0AccessToken;

			Application.Current.Properties["name"] = user.Profile["name"].ToString();
			Application.Current.Properties["email"] = user.Profile["email"].ToString();
			Application.Current.Properties["picture"] = user.Profile["picture"].ToString();

			await Navigation.PushModalAsync(new HomePage());
		}


		public void GetUserData(string token)
		{
			var client = new RestClient(App.DOMAIN);
			var request = new RestRequest("tokeninfo", Method.GET);
			request.AddParameter("id_token", token);


			IRestResponse response = client.Execute(request);

			User user = JsonConvert.DeserializeObject<User>(response.Content);

			// Once the call executes, we capture the user data in the
			// `Application.Current` namespace which is globally available in Xamarin
			Application.Current.Properties["email"] = user.email;
			Application.Current.Properties["picture"] = user.picture;

			// Finally, we navigate the user the the Home page
			Navigation.PushModalAsync(new HomePage());
		}

		public class LoginToken
		{
			public string id_token { get; set; }
			public string access_token { get; set; }
			public string token_type { get; set; }
		}

		public class User
		{
			public string name { get; set; }
			public string picture { get; set; }
			public string email { get; set; }
		}
	}

}

