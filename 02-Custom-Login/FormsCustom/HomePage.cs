using System;

using Xamarin.Forms;
using Auth0.SDK;

namespace FormsCustom
{
	public class HomePage : ContentPage
	{
		public HomePage()
		{
			User user = new User
			{
				Email = Application.Current.Properties["email"] as string,
				Avatar = Application.Current.Properties["picture"] as string
			};

			var logoutButton = new Button
			{
				Text = "Logout"
			};

			logoutButton.Clicked += (object sender, EventArgs e) =>
			{
				Logout();
			};

			Content = new StackLayout
			{
				Padding = 30,
				Spacing = 10,
				Children = {
					new Image { Source = ImageSource.FromUri(new Uri(user.Avatar))},
					new Label { Text = "Hello " + user.Email },
					logoutButton

				}
			};
		}

		public void Logout()
		{
			var auth0 = new Auth0Client("YOUR-AUTH0-DOMAIN", "YOUR-AUTH0-CLIENT-ID");
			auth0.Logout();
			Navigation.PushModalAsync(new LoginPage());
		}

		public class User
		{
			public string Email { get; set; }
			public string Avatar { get; set; }
		}
	}
}

