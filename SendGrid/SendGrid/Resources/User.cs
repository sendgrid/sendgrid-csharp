using Newtonsoft.Json.Linq;
using SendGrid.Model;
using SendGrid.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace SendGrid.Resources
{
	public class User
	{
		private string _endpoint;
		private Client _client;

		/// <summary>
		/// Constructs the SendGrid Users object.
		/// See https://sendgrid.com/docs/API_Reference/Web_API_v3/user.html
		/// </summary>
		/// <param name="client">SendGrid Web API v3 client</param>
		/// <param name="endpoint">Resource endpoint, do not prepend slash</param>
		public User(Client client, string endpoint = "v3/user/profile")
		{
			_endpoint = endpoint;
			_client = client;
		}

		/// <summary>
		/// Get your user profile
		/// </summary>
		/// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/user.html</returns>
		public async Task<UserProfile> GetProfileAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var response = await _client.Get(_endpoint, cancellationToken);
			response.EnsureSuccess();

			var responseContent = await response.Content.ReadAsStringAsync();
			var profile = JObject.Parse(responseContent).ToObject<UserProfile>();
			return profile;
		}

		/// <summary>
		/// Update your user profile
		/// </summary>
		/// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/user.html</returns>
		public async Task UpdateProfileAsync(UserProfile profile, CancellationToken cancellationToken = default(CancellationToken))
		{
			var data = JObject.FromObject(profile);
			var response = await _client.Patch(_endpoint, data, cancellationToken);
			response.EnsureSuccess();
		}

		/// <summary>
		/// Get your user account
		/// </summary>
		/// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/user.html</returns>
		public async Task<Account> GetAccountAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var response = await _client.Get("v3/user/account", cancellationToken);
			response.EnsureSuccess();

			var responseContent = await response.Content.ReadAsStringAsync();
			var account = JObject.Parse(responseContent).ToObject<Account>();
			return account;
		}
	}
}
