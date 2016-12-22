using System;
namespace Services
{
	public interface Interfaces
	{
	}
	public interface IPlatformUiContext { }

	public interface IAuth0Client
	{
		System.Threading.Tasks.Task<Auth0UserX> LoginAsync(string connection = "", bool withRefreshToken = false, string scope = "openid", string title = null);
		void Logout();
	}

	//seems pointless to interface since a test can readily manipulate a flat DTO like this
	public class Auth0UserX
	{
		public string Auth0AccessToken { get; set; }
		public string IdToken { get; set; }
		public Newtonsoft.Json.Linq.JObject Profile { get; set; }
		public string RefreshToken { get; set; }
	}
}
