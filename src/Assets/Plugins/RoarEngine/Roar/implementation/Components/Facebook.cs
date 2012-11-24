using System.Collections;
using UnityEngine;

using Roar.Components;

namespace Roar.implementation.Components
{
	public class Facebook : IFacebook
	{
		protected IDataStore dataStore;
		IWebAPI.IFacebookActions facebook;
		ILogger logger;

		public Facebook (IWebAPI.IFacebookActions facebookActions, IDataStore dataStore, ILogger logger)
		{
			this.facebook = facebookActions;
			this.dataStore = dataStore;
			this.logger = logger;
		}

		// ---- Access Methods ----
		// ------------------------

		public void LoginOAuth (string oauth_token, Roar.Callback<WebObjects.Facebook.Login_oauthResponse> cb)
		{
			if (oauth_token == "") {
				logger.DebugLog ("[roar] -- Must specify oauth_token for facebook login");
				return;
			}
			
			Roar.WebObjects.Facebook.Login_oauthArguments args = new Roar.WebObjects.Facebook.Login_oauthArguments();
			args.oauth_token = oauth_token;

			facebook.login_oauth (args, new LoginOAuthCallback (cb));
		}

		class LoginOAuthCallback : CBBase<WebObjects.Facebook.Login_oauthResponse>
		{
			public LoginOAuthCallback (Roar.Callback<WebObjects.Facebook.Login_oauthResponse> in_cb) : base( in_cb )
			{
			}

			public override void HandleError ( Roar.RequestResult  info)
			{
				RoarManager.OnLogInFailed (info.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.Facebook.Login_oauthResponse> info)
			{
				RoarManager.OnLoggedIn ();
				// @todo Perform auto loading of game and player data
			}
		}


		public void CreateOAuth (string name, string oAuthToken, Roar.Callback<WebObjects.Facebook.Create_oauthResponse> cb)
		{
			if (name == "" || oAuthToken == "") {
				logger.DebugLog ("[roar] -- Must specify username and oauthToken for creation");
				return;
			}
			
			Roar.WebObjects.Facebook.Create_oauthArguments args = new Roar.WebObjects.Facebook.Create_oauthArguments();
			args.name = name;
			args.oauth_token = oAuthToken;

			facebook.create_oauth (args, new CreateOAuthCallback (cb));
		}


		protected class CreateOAuthCallback : CBBase<WebObjects.Facebook.Create_oauthResponse>
		{

			public CreateOAuthCallback (Roar.Callback<WebObjects.Facebook.Create_oauthResponse> in_cb) : base(in_cb)
			{
			}

			public override void HandleError ( Roar.RequestResult info)
			{
				RoarManager.OnCreateUserFailed (info.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.Facebook.Create_oauthResponse> info)
			{
				RoarManager.OnCreatedUser ();
				RoarManager.OnLoggedIn ();
			}
		}

	}

}
