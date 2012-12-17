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

        string oAuthToken = null;
		
		enum PostLogionAction
		{
			Nothing,
			LoginRoar,
			CreateRoar,
			BindRoar,
		}
        PostLogionAction postLoginAction;
		string requestedName;
		Roar.Callback<WebObjects.Facebook.BindOauthResponse> requestedBindCB;
		Roar.Callback<WebObjects.Facebook.CreateOauthResponse> createoAuthCB;
		Roar.Callback<WebObjects.Facebook.LoginOauthResponse> loginCB;
		
		public Facebook (IWebAPI.IFacebookActions facebookActions, IDataStore dataStore, ILogger logger)
		{
			this.facebook = facebookActions;
			this.dataStore = dataStore;
			this.logger = logger;
		}

		protected class FacebookCreateCallback : CBBase<WebObjects.Facebook.CreateOauthResponse>
		{
			protected Facebook facebook;

			public FacebookCreateCallback (Roar.Callback<WebObjects.Facebook.CreateOauthResponse> in_cb, Facebook in_facebook) : base(in_cb)
			{
				facebook = in_facebook;
			}
			
			public override void HandleError (Roar.RequestResult nn)
			{
				RoarManager.OnCreateUserFailed (nn.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.Facebook.CreateOauthResponse> info)
			{
				RoarManager.OnCreatedUser ();
				RoarManager.OnLoggedIn ();
				
			}
		}

        protected class FacebookBindCallback : CBBase<WebObjects.Facebook.BindOauthResponse>
		{
			protected Facebook facebook;

			public FacebookBindCallback (Roar.Callback<WebObjects.Facebook.BindOauthResponse> in_cb, Facebook in_facebook) : base(in_cb)
			{
				facebook = in_facebook;
			}
			
			public override void HandleError (Roar.RequestResult nn)
			{
                Debug.Log("Facebook binding failed "+nn.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.Facebook.BindOauthResponse> info)
			{
                Debug.Log("binding succeeded");
				
			}
		}


		// ---- Access Methods ----
		// ------------------------


		protected class LoginCallback : CBBase<WebObjects.Facebook.LoginOauthResponse>
		{
			protected Facebook facebook;

			public LoginCallback (Roar.Callback<WebObjects.Facebook.LoginOauthResponse> in_cb, Facebook in_facebook) : base(in_cb)
			{
				facebook = in_facebook;
			}

			public override void HandleError (Roar.RequestResult nn)
			{
				RoarManager.OnLogInFailed (nn.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.Facebook.LoginOauthResponse> info)
			{
				RoarManager.OnLoggedIn ();
				// @todo Perform auto loading of game and player data
				
			}
		}

        public void FacebookGraphRedirect(string redirectURL)
        {
            Debug.Log("redirecting because of a blank code");
			Application.OpenURL("https://graph.facebook.com/oauth/authorize?client_id="+DefaultRoar.Instance.facebookApplicationID+"&redirect_uri="+redirectURL);

        }

        public void FetchOAuthToken(string codeParameter)
        {
            DoFetchFacebookOAuthToken(codeParameter, null);

        }
		
		public void AttemptFacebookLoginChain()
		{
			if(oAuthToken == null || oAuthToken == "")
			{
				DefaultRoar.FacebookLoginOptions facebookLoginOptions = DefaultRoar.Instance.facebookLoginOptions;
            
				if(facebookLoginOptions != DefaultRoar.FacebookLoginOptions.ExternalOauthOnly && 
	            facebookLoginOptions != DefaultRoar.FacebookLoginOptions.InternalNonjavascriptLoginNOT_IMPLEMENTED)
		        {
		            RequestFacebookSignedRequest();
		        }
	            else	            
		        if(facebookLoginOptions != DefaultRoar.FacebookLoginOptions.ExternalOauthOnly && 
		            facebookLoginOptions != DefaultRoar.FacebookLoginOptions.InternalNonjavascriptLoginNOT_IMPLEMENTED &&
		            facebookLoginOptions != DefaultRoar.FacebookLoginOptions.SignedRequestOnly)
		        {
		
		            RequestFacebookGetCode();
		        }
				
			}
			
		}
		
		public void DoMainLogin(Roar.Callback<WebObjects.Facebook.LoginOauthResponse> callback)
		{
			if(oAuthToken == null || oAuthToken == "")
			{
				loginCB = callback;
				postLoginAction = PostLogionAction.LoginRoar;
				
				AttemptFacebookLoginChain();
					
			}
			else
			{
				DoLoginFacebookOAuth(callback);
				
			}
			
			
		}
		
		public void SignedRequestFailed()
		{
            Debug.Log("Signed request failed");
			if(DefaultRoar.Instance.facebookLoginOptions != DefaultRoar.FacebookLoginOptions.SignedRequestOnly)
            {
			    RequestFacebookGetCode();
            }
            else
            {
				Debug.Log ("Signed request login failed and I have no other login options to fall back on.");
            }
			
		}

		public void DoLoginFacebookOAuth (Roar.Callback<WebObjects.Facebook.LoginOauthResponse> cb)
		{
			if (oAuthToken == "") {
				logger.DebugLog ("[roar] -- Must specify oauth_token for facebook login");
				return;
			}

			Roar.WebObjects.Facebook.LoginOauthArguments args = new Roar.WebObjects.Facebook.LoginOauthArguments ();
			args.oauth_token = oAuthToken;
			
			
			facebook.login_oauth (args, new LoginFacebookOAuthCallback(cb, this));
		}

		public void DoLoginFacebookSignedReq (string signedReq, Roar.Callback<WebObjects.Facebook.LoginSignedResponse> cb)
		{
			if (signedReq == "") {
				logger.DebugLog ("[roar] -- Must specify signedReq for facebook login");
				return;
			}
			
			Roar.WebObjects.Facebook.LoginSignedArguments args = new Roar.WebObjects.Facebook.LoginSignedArguments ();
			
			args.signed_request = signedReq;

			facebook.login_signed (args, new LoginFacebookSignedCallback (cb, this));
		}

		public void DoFetchFacebookOAuthToken (string code, Roar.Callback<WebObjects.Facebook.FetchOauthTokenResponse> cb)
		{
			Roar.WebObjects.Facebook.FetchOauthTokenArguments args = new Roar.WebObjects.Facebook.FetchOauthTokenArguments();
			args.code = code;
			facebook.fetch_oauth_token(args, new FetchFacebookOAuthTokenCallback (cb, this));
		}


		class FetchFacebookOAuthTokenCallback : CBBase<WebObjects.Facebook.FetchOauthTokenResponse>
		{
			protected Facebook facebook;

			public FetchFacebookOAuthTokenCallback(Roar.Callback<Roar.WebObjects.Facebook.FetchOauthTokenResponse> in_cb, Facebook in_facebook)
				: base(in_cb)
			{
				facebook = in_facebook;
			}
	

			public override void HandleError(Roar.RequestResult nn)
			{
				Debug.Log("OAuth Fetch Failed " + nn.msg);
				
			}

			public override void HandleSuccess(CallbackInfo<WebObjects.Facebook.FetchOauthTokenResponse> info)
			{
				Debug.Log("oauth successful "+info.data.ToString()+info.msg);
				

                string oauthToken = info.data.oauth_token ;
                facebook.SetOAuthToken(oauthToken);
				facebook.DoPostLoginAction();
			}
		}


		class LoginFacebookOAuthCallback : CBBase<WebObjects.Facebook.LoginOauthResponse>
		{
			protected Facebook facebook;

			public LoginFacebookOAuthCallback (Roar.Callback<WebObjects.Facebook.LoginOauthResponse> in_cb, Facebook in_facebook) : base( in_cb )
			{
				facebook = in_facebook;
			}

			public override void HandleError (Roar.RequestResult nn)
			{
				RoarManager.OnLogInFailed (nn.msg);
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.Facebook.LoginOauthResponse> info)
			{
				RoarManager.OnLoggedIn ();
				// @todo Perform auto loading of game and player data
			}
		}
		
		class LoginFacebookSignedCallback : CBBase<WebObjects.Facebook.LoginSignedResponse>
		{
			protected Facebook facebook;

			public LoginFacebookSignedCallback (Roar.Callback<WebObjects.Facebook.LoginSignedResponse> in_cb, Facebook in_facebook) : base( in_cb )
			{
				facebook = in_facebook;
			}

			public override void HandleError (Roar.RequestResult nn)
			{
				RoarManager.OnLogInFailed (nn.msg);
                
			}

			public override void HandleSuccess (CallbackInfo<WebObjects.Facebook.LoginSignedResponse> info)
			{
				RoarManager.OnLoggedIn ();
				// @todo Perform auto loading of game and player data
				
			}
		}


        public void DoBindFacebook(Roar.Callback<WebObjects.Facebook.BindOauthResponse> cb)
        {
			
			if(oAuthToken == null || oAuthToken == "")
			{
				requestedBindCB = cb;
				postLoginAction = PostLogionAction.BindRoar;
				
				AttemptFacebookLoginChain();
					
			}
			else
			{
			
			
				Roar.WebObjects.Facebook.BindOauthArguments args = new Roar.WebObjects.Facebook.BindOauthArguments();
				args.oauth_token = oAuthToken;

				facebook.bind_oauth(args, new FacebookBindCallback (cb, this));
			}

        }

		public void DoCreateFacebook (string name,  Roar.Callback<WebObjects.Facebook.CreateOauthResponse> cb)
		{
			if(oAuthToken == null || oAuthToken == "")
			{
				createoAuthCB = cb;
				postLoginAction = PostLogionAction.CreateRoar;
				
				requestedName = name;
				
				AttemptFacebookLoginChain();
					
			}
			else
			{
				if (name == "" || oAuthToken == "" || oAuthToken == null) {
					logger.DebugLog ("[roar] -- Must specify username and signed req for creation");
					return;
				}
	
				Roar.WebObjects.Facebook.CreateOauthArguments args = new Roar.WebObjects.Facebook.CreateOauthArguments();
				
				args.name = name;
				args.oauth_token = oAuthToken;
	
				facebook.create_oauth(args, new FacebookCreateCallback (cb, this));
			}
		}
		
		public void DoPostLoginAction ( )
		{
            switch(postLoginAction)
			{
			case PostLogionAction.CreateRoar:
				DoCreateFacebook(requestedName, createoAuthCB);
				break;
				
			case PostLogionAction.LoginRoar:
				DoLoginFacebookOAuth(loginCB);
				
				break;
				
			case PostLogionAction.BindRoar:
				DoBindFacebook(requestedBindCB);
				break;
				
			case PostLogionAction.Nothing:
				
				break;
				
				
			}
			
		}

		//TODO: not sure this belongs in this class!
		/*
		public void CacheFromInventory (Roar.Callback cb=null)
		{
			if (! dataStore.inventory.HasDataFromServer)
				return;

			// Build sanitised ARRAY of ikeys from Inventory.list()
			var l = dataStore.inventory.List ();
			var ikeyList = new ArrayList ();
			for (int i=0; i<l.Count; i++)
				ikeyList.Add ((l [i] as Hashtable) ["ikey"]);

			var toCache = dataStore.cache.ItemsNotInCache (ikeyList) as ArrayList;

			// Build sanitised Hashtable of ikeys from Inventory
			// No need to call server as information is already present
			Hashtable cacheData = new Hashtable ();
			for (int i=0; i<toCache.Count; i++) {
				for (int k=0; k<l.Count; k++) {
					// If the Inventory ikey matches a value in the
					// list of items to cache, add it to our `cacheData` obj
					if ((l [k] as Hashtable) ["ikey"] == toCache [i])
						cacheData [toCache [i]] = l [k];
				}
			}

			// Enable update of cache if it hasn't been initialised yet
			dataStore.cache.HasDataFromServer = true;
			dataStore.cache.Set (cacheData);
		}
		 */
        public void SetOAuthToken(string oauth_token)
        {
            Debug.Log("Set oauth token "+oauth_token);
            if(oauth_token != "")
                oAuthToken = oauth_token;
            
        }
		
		


         /**
         * Function that is called to tell the hosting iframe to passback the signed request string if available.
         * Must be called before using the signed request string.
         *
         *
         **/
        public void RequestFacebookSignedRequest()
        {
            Application.ExternalCall("sendSignedRequest");
        }

        /**
        * Function that is called to ask the hosting browser to pass the get parameter code give by facebook
        * If no get parameter code is available will return blank and a graph authorization url will have to be requested.
        *
        *
        **/
	    public void RequestFacebookGetCode()
	    {
		    Application.ExternalCall("returnCodeIfAvailable");
	    }

	}

}
