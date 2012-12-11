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

		public void LoginOAuth (string oauth_token, Roar.Callback<WebObjects.Facebook.LoginOauthResponse> cb)
		{
			if (oauth_token == "") {
				logger.DebugLog ("[roar] -- Must specify oauth_token for facebook login");
				return;
			}
			
			Roar.WebObjects.Facebook.LoginOauthArguments args = new Roar.WebObjects.Facebook.LoginOauthArguments();
			args.oauth_token = oauth_token;

			facebook.login_oauth (args, new LoginOAuthCallback (cb));
		}

		class LoginOAuthCallback : CBBase<WebObjects.Facebook.LoginOauthResponse>
		{
			public LoginOAuthCallback (Roar.Callback<WebObjects.Facebook.LoginOauthResponse> in_cb) : base( in_cb )
			{
			}

			public override void HandleError ( Roar.RequestResult  info)
			{
				RoarManager.OnLogInFailed (info.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.Facebook.LoginOauthResponse> info)
			{
				RoarManager.OnLoggedIn ();
				// @todo Perform auto loading of game and player data
			}
		}


		public void CreateOAuth (string name, string oAuthToken, Roar.Callback<WebObjects.Facebook.CreateOauthResponse> cb)
		{
			if (name == "" || oAuthToken == "") {
				logger.DebugLog ("[roar] -- Must specify username and oauthToken for creation");
				return;
			}
			
			Roar.WebObjects.Facebook.CreateOauthArguments args = new Roar.WebObjects.Facebook.CreateOauthArguments();
			args.name = name;
			args.oauth_token = oAuthToken;

			facebook.create_oauth (args, new CreateOAuthCallback (cb));
		}


		protected class CreateOAuthCallback : CBBase<WebObjects.Facebook.CreateOauthResponse>
		{

			public CreateOAuthCallback (Roar.Callback<WebObjects.Facebook.CreateOauthResponse> in_cb) : base(in_cb)
			{
			}

			public override void HandleError ( Roar.RequestResult info)
			{
				RoarManager.OnCreateUserFailed (info.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.Facebook.CreateOauthResponse> info)
			{
				RoarManager.OnCreatedUser ();
				RoarManager.OnLoggedIn ();
			}
		}

	}

}
