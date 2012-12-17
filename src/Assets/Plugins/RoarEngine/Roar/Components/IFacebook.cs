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
		void DoLoginFacebookOAuth(Roar.Callback<WebObjects.Facebook.LoginOauthResponse> cb);
		
		/**
		 * Login a player using Facebook Signed Request. Note this is for UnityPlayers
		 * that are running in a facebook iframe only (Canvas app)
		 *
		 * On success:
		 * - invokes callback with empty data parameter, success code and success message
		 * - fires a RoarManager#loggedInEvent
		 *
		 * On failure:
		 * - invokes callback with empty data parameter, error code and error message
		 * - fires a RoarManager#logInFailedEvent containing a failure message
		 *
		 * @param signed request taken from facebook post
		 * @param cb the callback function to be passed the result of doLogin.
		 **/
		void DoLoginFacebookSignedReq(string signedReq, Roar.Callback<WebObjects.Facebook.LoginSignedResponse> cb);

        /**
         * Binds an existing logged in account to a facebook account. You must have logged in to Roar
         * as well as to facebook before calling this function. The facebook account should not be tied
         * to another roar account before calling this method.
         * 
         * 
         *
         * On success:
         * 
         *
         * On failure:
         * - invokes callback with empty data parameter, error code and error message
         *
         * @param cb the callback function to be passed the result of doCreate.
         */
        void DoBindFacebook(Roar.Callback<Roar.WebObjects.Facebook.BindOauthResponse> cb);

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
		void DoCreateFacebook(string name, Roar.Callback<WebObjects.Facebook.CreateOauthResponse> cb);
		
		/**
		 * Will do whatever action is saved in postLoginAction (after a successful login). 
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
		void DoPostLoginAction();
		
		/**
		 * Will redirect the user to the facebook graph api (to retrieve a get code)
		 *
		 * On success:
		 *
		 * On failure:
		 *
		 */
		void FetchOAuthToken(string codeParameter);
		
		/**
		 * Called when the signed request oauth has failed.
		 *
		 * On success:
		 *
		 * On failure:
		 *
		 */
		void SignedRequestFailed();
		
        /**
		 * Will redirect the user to the facebook graph api (to retrieve a get code)
		 *
		 * On success:
		 *
		 * On failure:
		 *
		 */
        void FacebookGraphRedirect(string redirectURL);

        /**
		 * The main login class for facebook. Attempts to login based on the facebook login options.
		 *
		 * On success:
		 *
		 * On failure:
		 *
		 */
        void DoMainLogin(Roar.Callback<WebObjects.Facebook.LoginOauthResponse> callback);

        /**
		 * Request a signed request from the hosting javascript
		 *
		 * On success:
		 *
		 * On failure:
		 *
		 */
        void RequestFacebookSignedRequest();

        /**
		 * Request a get code from the hosting javascript
		 *
		 * On success:
		 *
		 * On failure:
		 *
		 */
        void RequestFacebookGetCode();
		
		/**
		 * Sets the OAuth token directly if it has been obtained from elsewhere. Useful if you dont want 
		 * to use Roars facebook login system.
		 *
		 *
		 */
		void SetOAuthToken(string oauth_token);
        
	}
}
