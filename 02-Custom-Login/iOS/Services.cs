using System;
namespace Services
{
	public class Auth0ClientIos : IAuth0Client
	{
		private readonly UIKit.UIViewController _uiViewController;
		private readonly Auth0.SDK.Auth0Client _platformAuth0;
		public Auth0ClientIos(UIKit.UIViewController uiViewController, string domain, string clientId)
		{
			_uiViewController = uiViewController;
			_platformAuth0 = new Auth0.SDK.Auth0Client(domain, clientId);
		}

		public async System.Threading.Tasks.Task<Auth0UserX> LoginAsync(string connection = "", bool withRefreshToken = false, string scope = "openid", string title = null)
		{
			var user = await _platformAuth0.LoginAsync(_uiViewController);
			return new Auth0UserX { Auth0AccessToken = user.Auth0AccessToken, Profile = user.Profile, IdToken = user.IdToken, RefreshToken = user.RefreshToken };
		}

		public void Logout()
		{
			_platformAuth0.Logout();
		}
	}
}
