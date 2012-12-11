using System.Collections;

namespace Roar.Components
{
	/**
	 * \brief Methods for creating, authenticating and logging out a User.
	 *
	 * @todo The naming of the members of this class seem a little odd.
	 **/
	public interface IFacebook
	{
		/**
		 * Login a player using Facebook OAuth.
		 *
		 * On success:
		 * - invokes callback with empty data parameter, success code and success message
		 * - fires a RoarManager#loggedInEvent
		 *
		 * On failure:
		 * - invokes callback with empty data parameter, error code and error message
		 * - fires a RoarManager#logInFailedEvent containing a failure message
		 *
		 * @param oauth_token the OAuth token.
		 * @param cb the callback function to be passed the result of doLogin.
		 **/
		void LoginOAuth(string oauth_token, Roar.Callback<WebObjects.Facebook.LoginOauthResponse> cb);


		/**
		 * Creates a new user with the given username and facebook authToken, and logs
		 * that player in. This will only work if you have a signed request verifying the current user.
		 * You will automatically get this from Facebook as a POST parameter if you are running an iframe app within Facebook.
		 *
		 * On success:
		 * - fires a RoarManager#createdUserEvent
		 * - automatically calls doLogin()
		 *
		 * On failure:
		 * - invokes callback with empty data parameter, error code and error message
		 * - fires a RoarManager#createUserFailedEvent containing a failure message
		 *
		 * @param name the players username
		 * @param the players facebook signed auth
		 * @param cb the callback function to be passed the result of doCreate.
		 */
		void CreateOAuth(string name, string oAuthToken, Roar.Callback<WebObjects.Facebook.CreateOauthResponse> cb);
	}
}
