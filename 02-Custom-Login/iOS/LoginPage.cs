using System;

using Xamarin.Forms;
using RestSharp;
using Newtonsoft.Json;
using Auth0.SDK;
using UIKit;

namespace FormsCustom
{
	public class LoginPage : ContentPage
	{
		public LoginPage()
		{
			var email = new Entry
			{
				Placeholder = "Email"
			};

			var password = new Entry
			{
				Placeholder = "Password",
				IsPassword = true
			};

			var signupButton = new Button
			{
				Text = "Sign Up"
			};

			var facebookLoginButton = new Button
			{
				Text = "Login with Facebook"
			};

			signupButton.Clicked += (object sender, EventArgs e) =>
			{
				Login(email.Text, password.Text);
			};

			facebookLoginButton.Clicked += (object sender, EventArgs e) =>
			{
				SocialLogin();
				
			};

			Content = new StackLayout
			{
				Padding = 30,
				Spacing = 10,
				Children = {
					new Label { Text = "Custom Login with Xamarin Forms" },
					new Label { Text = "Login with Username and Password" },
					email,
					password,
					signupButton,
					new Label { Text = "Login with Social Media Account" },
					facebookLoginButton,
				}
			};
		}

		public void Login(string username, string password)
		{
			// We are using the RestSharp library which provides many useful
			// methods and helpers when dealing with REST.
			// We first create the request and add the necessary parameters
			var client = new RestClient("https://reviewgen.auth0.com");
			var request = new RestRequest("oauth/ro", Method.POST);
			request.AddParameter("client_id", "kHC5pGuvKFtXElLXuLOYP2CDysJnWAWN");
			request.AddParameter("username", username);
			request.AddParameter("password", password);
			request.AddParameter("connection", "Username-Password-Authentication");
			request.AddParameter("grant_type", "password");
			request.AddParameter("scope", "openid");

			// We execute the request and capture the response
			// in a variable called `response`
			IRestResponse response = client.Execute(request);

			// Using the Newtonsoft.Json library we deserialaize the string into an object,
			// we have created a LoginToken class that will capture the keys we need
			LoginToken token = JsonConvert.DeserializeObject<LoginToken>(response.Content);

			// We check to see if we received an `id_token` and if we did make a secondary call
			// to get the user data. If we did not receive an `id_token` we can safely assume
			// that the authentication failed so we display an error message telling the user
			// to try again.
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

		public void SocialLogin()
		{
			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null)
			{
				vc = vc.PresentedViewController;
			}
			var auth0 = new Auth0Client("reviewgen.auth0.com", "kHC5pGuvKFtXElLXuLOYP2CDysJnWAWN");
			auth0.LoginAsync(vc, "facebook");
		}

		public void GetUserData(string token)
		{
			var client = new RestClient("https://reviewgen.auth0.com");
			var request = new RestRequest("tokeninfo", Method.GET);
			request.AddParameter("id_token", token);


			IRestResponse response = client.Execute(request);

			User user = JsonConvert.DeserializeObject<User>(response.Content);

			// Once the call executes, we capture the user data in the
			// `Application.Current` namespace which is globally available in Xamarin
			Application.Current.Properties["email"] = user.email;
			Application.Current.Properties["picture"] = user.picture;

			// Finally, we navigate the user the the Orders page
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

