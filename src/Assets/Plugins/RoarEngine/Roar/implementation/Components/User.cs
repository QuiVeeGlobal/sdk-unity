using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Roar.Components;

namespace Roar.implementation.Components
{
	public class User : IUser
	{
		protected DataStore dataStore;
		ZWebAPI.UserActions userActions;
		ILogger logger;

		public User (ZWebAPI.UserActions userActions, DataStore dataStore, ILogger logger)
		{
			this.userActions = userActions;
			this.dataStore = dataStore;
			this.logger = logger;

			// -- Event Watchers
			// Flush models on logout
			RoarManager.loggedOutEvent += () => {
				dataStore.Clear (true); };

			// Watch for initial inventory ready event, then watch for any
			// subsequent `change` events
			RoarManager.inventoryReadyEvent += () => CacheFromInventory ();
		}


		// ---- Access Methods ----
		// ------------------------

		public void DoLogin (string name, string hash, Roar.Callback<WebObjects.User.LoginResponse> cb)
		{
			if (name == "" || hash == "") {
				logger.DebugLog ("[roar] -- Must specify username and password for login");
				return;
			}

			WebObjects.User.LoginArguments args = new WebObjects.User.LoginArguments();
			args.name = name;
			args.hash = hash;
			userActions.login (args, new LoginCallback (cb, this));
		}

		protected class LoginCallback : CBBase<WebObjects.User.LoginResponse>
		{
			protected User user;

			public LoginCallback (Roar.Callback<WebObjects.User.LoginResponse> in_cb, User in_user) : base(in_cb)
			{
				user = in_user;
			}

			public override void HandleError (RequestResult info)
			{
				RoarManager.OnLogInFailed (info.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.User.LoginResponse> info)
			{
				RoarManager.OnLoggedIn ();
				// @todo Perform auto loading of game and player data
			}
		}

		public void DoLoginFacebookOAuth (string oauth_token, Roar.Callback<WebObjects.User.Login_facebook_oauthResponse> cb)
		{
			if (oauth_token == "") {
				logger.DebugLog ("[roar] -- Must specify oauth_token for facebook login");
				return;
			}

			WebObjects.User.Login_facebook_oauthArguments args = new Roar.WebObjects.User.Login_facebook_oauthArguments();
			args.oauth_token = oauth_token;

			userActions.login_facebook_oauth (args, new LoginFacebookOAuthCallback (cb, this));
		}
		class LoginFacebookOAuthCallback : CBBase<WebObjects.User.Login_facebook_oauthResponse>
		{
			protected User user;

			public LoginFacebookOAuthCallback (Roar.Callback<WebObjects.User.Login_facebook_oauthResponse> in_cb, User in_user) : base( in_cb )
			{
				user = in_user;
			}

			public override void HandleError (RequestResult info)
			{
				RoarManager.OnLogInFailed (info.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.User.Login_facebook_oauthResponse> info)
			{
				RoarManager.OnLoggedIn ();
				// @todo Perform auto loading of game and player data
			}
		}


		public void DoLogout (Roar.Callback<WebObjects.User.LogoutResponse> cb)
		{
			WebObjects.User.LogoutArguments args = new Roar.WebObjects.User.LogoutArguments();
			userActions.logout (args, new LogoutCallback (cb, this));
		}

		protected class LogoutCallback : CBBase<WebObjects.User.LogoutResponse>
		{
			protected User user;

			public LogoutCallback (Roar.Callback<WebObjects.User.LogoutResponse> in_cb, User in_user) : base(in_cb)
			{
				user = in_user;
				cb = in_cb;
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.User.LogoutResponse> info)
			{
				RoarManager.OnLoggedOut ();
			}

		};


		public void DoCreate (string name, string hash, Roar.Callback<WebObjects.User.CreateResponse> cb)
		{
			if (name == "" || hash == "") {
				logger.DebugLog ("[roar] -- Must specify username and password for login");
				return;
			}
			
			WebObjects.User.CreateArguments args = new Roar.WebObjects.User.CreateArguments();
			args.name = name;
			args.hash = hash;

			userActions.create (args, new CreateCallback (cb, this));
		}
		protected class CreateCallback : CBBase<WebObjects.User.CreateResponse>
		{
			protected User user;

			public CreateCallback (Roar.Callback<WebObjects.User.CreateResponse> in_cb, User in_user) : base(in_cb)
			{
				user = in_user;
			}

			public override void HandleError (RequestResult info)
			{
				RoarManager.OnCreateUserFailed (info.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.User.CreateResponse> info)
			{
				RoarManager.OnCreatedUser ();
				RoarManager.OnLoggedIn ();
			}
		}

		//TODO: not sure this belongs in this class!
		public void CacheFromInventory (Roar.RequestCallback cb=null)
		{
			if (! dataStore.inventory.HasDataFromServer)
				return;

			// Build sanitised ARRAY of ikeys from Inventory.list()
			IList<DomainObjects.Item> l = dataStore.inventory.List ();
			List<string> ikeyList = new List<string> ();
			for (int i=0; i<l.Count; i++)
				ikeyList.Add ( l[i].ikey );

			IList<string> toCache = dataStore.cache.ItemsNotInCache (ikeyList);

			// Build sanitised Hashtable of ikeys from Inventory
			// No need to call server as information is already present
			Dictionary<string,Foo> cacheData = new Dictionary<string,Foo> ();
			for (int i=0; i<toCache.Count; i++) {
				for (int k=0; k<l.Count; k++) {
					// If the Inventory ikey matches a value in the
					// list of items to cache, add it to our `cacheData` obj
					if ( l[k].ikey == toCache [i])
					{
						//TODO: Fix this
						//cacheData [toCache [i]] = l [k];
						cacheData [toCache[i]] = new Foo();
					}
				}
			}

			// Enable update of cache if it hasn't been initialised yet
			dataStore.cache.HasDataFromServer = true;
			dataStore.cache.Set (cacheData);
		}

	}

}
