using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//TODO: Should this be called RoarActionsWidget?

public class RoarLoginWidget : RoarUIWidget
{
	public delegate void RoarLoginWidgetHandler();
	public static event RoarLoginWidgetHandler OnFullyLoggedIn; // logged in & stats fetched
	protected Roar.Components.ITasks tasks;
	protected bool isFetching=false;
	
	public bool enableOnAwake = true;
	public bool fetchPropertiesOnLogin = true;
	public bool saveUsername = true;
	public bool savePassword = false;
	
	private static string KEY_USERNAME = "roar-username";
	private static string KEY_PASSWORD = "roar-password";
	
	private string status = "Supply a username and password to log in or to register a new account.";
	private bool isError;
	private string username = string.Empty;
	private string password = string.Empty;
	public int loginBoxSpacing = 10;
	public int leftOffset = 50;
	public int topOffset = 50;
	public int passwordBoxSpacing = 10;
	public float labelWidth = 90;
	public float statusWidth = 400;
	public float fieldHeight = 30;
	public float fieldWidth = 140;
	public float buttonWidth = 200;
	public float buttonHeight = 40;
	public float buttonSpacing = 10;
	private bool networkActionInProgress;
	public string buttonStyle="LoginButton";
	public string boxStyle="LoginField";
	public string loginLabelStyle = "LoginLabel";
	public string statusStyle = "LoginStatus";
	
	public bool allowFacebookLogin = true;
	
	public string facebookApplicationID = "";
	bool lastRequestLogin; //true if the last request was a login facebook request, else it was a create facebook request
	
	
	protected override void Awake ()
	{
		base.Awake ();
		if(enableOnAwake)
			enabled = true;
	}
	
	protected override void OnEnable ()
	{
		
		if (saveUsername)
		{
			username = PlayerPrefs.GetString(KEY_USERNAME, string.Empty);
		}
		if (savePassword)
		{
			password = PlayerPrefs.GetString(KEY_PASSWORD, string.Empty);
		}
		
		roar.Facebook.RequestFacebookStatePara();
	}
	
	protected override void DrawGUI(int windowId)
	{
		
		if(networkActionInProgress)
		{
			GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Logging In", "StatusNormal");
			ScrollViewContentHeight = contentBounds.height;
		}
		else
		{
			Rect currentRect = new Rect(leftOffset, topOffset, statusWidth, fieldHeight);
			GUI.Label(currentRect, status, statusStyle);
			currentRect.y += 100;
			currentRect.width = labelWidth;
			GUI.Label(currentRect, "Username", loginLabelStyle);
			currentRect.x += labelWidth;
			currentRect.width = fieldWidth;
			
			username = GUI.TextField(currentRect, username, boxStyle);
			currentRect.x = leftOffset;
			
			currentRect.y += fieldHeight + loginBoxSpacing;
			
			currentRect.width = labelWidth;
			GUI.Label(currentRect, "Password", loginLabelStyle);
			currentRect.x += labelWidth;
			currentRect.width = fieldWidth;
			
			password = GUI.PasswordField(currentRect, password, '*', boxStyle);
			currentRect.x = leftOffset;
			currentRect.y+= fieldHeight + passwordBoxSpacing;
			
			currentRect.width = buttonWidth;
			currentRect.height = buttonHeight;
			
			GUI.enabled = username.Length > 0 && password.Length > 0 && !networkActionInProgress;
			
			if (GUI.Button(currentRect, "Log In", buttonStyle) || ( Event.current.keyCode == KeyCode.Return))
			{
	
				status = "Logging in...";
				networkActionInProgress = true;			
				if (saveUsername)
				{
					PlayerPrefs.SetString(KEY_USERNAME, username);
				}
				if (savePassword)
				{
					PlayerPrefs.SetString(KEY_PASSWORD, password);
				}
				if (Debug.isDebugBuild)
					Debug.Log(string.Format("[Debug] Logging in as [{0}] with password [{1}].", username, password));
				
				roar.User.Login(username, password, OnRoarLoginComplete);
			}
			
			currentRect.y+= buttonSpacing + buttonHeight;
			
			if (GUI.Button(currentRect, "Create Account", buttonStyle))
			{
	
				status = "Creating new player account...";
				networkActionInProgress = true;
				roar.User.Create(username, password, OnRoarAccountCreateComplete);
			}
			
			currentRect.y+= buttonSpacing + buttonHeight;
			GUI.enabled = true;
			
			if (GUI.Button(currentRect, "Login Facebook", buttonStyle))
			{
				lastRequestLogin = true;
				status = "Logging in through facebook...";
				networkActionInProgress = true;
				roar.Facebook.DoWebplayerLogin(OnRoarFacebookLoginComplete);
			}
			
			currentRect.y+= buttonSpacing + buttonHeight;
			
			if(username.Length == 0)
				GUI.enabled = false;
			if (GUI.Button(currentRect, "Create Facebook", buttonStyle))
			{
				lastRequestLogin = false;
				status = "Logging in to Facebook...";
				networkActionInProgress = true;
				roar.Facebook.DoWebplayerCreate(username, OnRoarFacebooCreateComplete);
			}
			
		
		}
		GUI.enabled = true;
		
	}
	
	public void Fetch()
	{
		isFetching = true;
		tasks.Fetch(OnRoarFetchTasksComplete);
	}
	
	void OnRoarFetchTasksComplete( Roar.CallbackInfo< IDictionary<string,Roar.DomainObjects.Task> > data )
	{
		isFetching = false;
	}
	
	void OnRoarFacebookFetchOauthTokenComplete(Roar.CallbackInfo<Roar.WebObjects.Facebook.FetchOauthTokenResponse> info)
	{
		if(info.code == IWebAPI.OK)
		{
			Debug.Log("oauth fetched successfully");
			networkActionInProgress = false;
		}
		else
		{
			networkActionInProgress = false;
			status = "Error, "+info.msg;
			
		}
		
	}
	
	void OnRoarFacebookLoginComplete(Roar.CallbackInfo<Roar.WebObjects.Facebook.LoginOauthResponse> info)
	{
		if (Debug.isDebugBuild)
		{
			Debug.Log(string.Format("OnRoarLoginComplete ({0}): {1}", info.code, info.msg));
			if( info.data!= null)
			{
			Debug.Log( string.Format("OnRoarLoginComplete got auth_token {0}", info.data.auth_token ) );
			Debug.Log( string.Format("OnRoarLoginComplete got player_id {0}",  info.data.player_id ) );
			}
			else
			{
				Debug.Log("OnRoarLogingComplete got null data");
			}
		}
		switch (info.code)
		{
		case IWebAPI.OK: // (success)
			isError = false;
			this.enabled = false;
			// fetch the player's properties after successful login
			if (fetchPropertiesOnLogin)
			{
				DefaultRoar.Instance.Properties.Fetch(OnRoarPropertiesFetched);
			}
			else
			{
				
				networkActionInProgress = false;
			}
			
			break;
		case 3: // Invalid name or password
		default:
			isError = true;
			status = info.msg;
			networkActionInProgress = false;
			break;
		}
		
	}
	
	void OnRoarFacebooCreateComplete(Roar.CallbackInfo<Roar.WebObjects.Facebook.CreateOauthResponse> info)
	{
		
		switch (info.code)
		{
		case IWebAPI.OK: // (success)
			isError = false;
			this.enabled = false;
			// fetch the player's properties after successful login
			if (fetchPropertiesOnLogin)
			{
				DefaultRoar.Instance.Properties.Fetch(OnRoarPropertiesFetched);
			}
			else
			{
				
				networkActionInProgress = false;
			}
			
			break;
		case 3: // Invalid name or password
		default:
			isError = true;
			status = info.msg;
			networkActionInProgress = false;
			break;
		}
		
	}
		
	void OnRoarLoginComplete(Roar.CallbackInfo<Roar.WebObjects.User.LoginResponse> info)
	{
		if (Debug.isDebugBuild)
		{
			Debug.Log(string.Format("OnRoarLoginComplete ({0}): {1}", info.code, info.msg));
			if( info.data!= null)
			{
			Debug.Log( string.Format("OnRoarLoginComplete got auth_token {0}", info.data.auth_token ) );
			Debug.Log( string.Format("OnRoarLoginComplete got player_id {0}",  info.data.player_id ) );
			}
			else
			{
				Debug.Log("OnRoarLogingComplete got null data");
			}
		}
		switch (info.code)
		{
		case IWebAPI.OK: // (success)
			isError = false;
			this.enabled = false;
			// fetch the player's properties after successful login
			if (fetchPropertiesOnLogin)
			{
				DefaultRoar.Instance.Properties.Fetch(OnRoarPropertiesFetched);
			}
			else
			{
				
				networkActionInProgress = false;
			}
			
			break;
		case 3: // Invalid name or password
		default:
			isError = true;
			status = info.msg;
			networkActionInProgress = false;
			break;
		}
	}

	void OnRoarAccountCreateComplete(Roar.CallbackInfo<Roar.WebObjects.User.CreateResponse> info)
	{
		if (Debug.isDebugBuild)
			Debug.Log(string.Format("OnRoarAccountCreateComplete ({0}): {1}", info.code, info.msg));
		switch (info.code)
		{
		case IWebAPI.OK: // (success)
			status = "Account successfully created. You can now log in.";
			break;
		case 3:
		default:
			isError = true;
			status = info.msg;
			break;
		}
		networkActionInProgress = false;
	}
	
	void OnRoarPropertiesFetched(Roar.CallbackInfo< IDictionary<string,Roar.DomainObjects.PlayerAttribute> > info)
	{
		networkActionInProgress = false;
		if (OnFullyLoggedIn != null) OnFullyLoggedIn();
	}
	
	#region JAVASCRIPT CALLBACK
	/**
	* Function that is called from javascript and is handed the facebook code parameter. Call graph.authorize with this.
	*
	*
	* @param code is the get parameter picked up from facebook 'GET'. Can be null.
	**/
	void CatchCodeGetPara(string paras)
	{
		Debug.Log("got code get para"+paras);
		if(paras.Split(' ')[0] == "")
		{
			//Invoke redirect with authorization.
			//fire event that says we are redirecting to login/authorize.
			if(lastRequestLogin)
				roar.Facebook.FacebookGraphRedirect(facebookApplicationID, "LoginFacebookOnLoad", paras.Split(' ')[1]);
			else
				roar.Facebook.FacebookGraphRedirect(facebookApplicationID, "CreateFacebookOnLoad", paras.Split(' ')[1]);
			
			return;
		}

		Debug.Log("got string para");
		Debug.Log("string is "+paras);
		string codeParameter = paras.Split(' ')[0];

		roar.Facebook.FetchOAuthToken(codeParameter, OnRoarFacebookFetchOauthTokenComplete);

	}

	/**
	* Function that is called from javascript and is handed the facebook state parameter
	*
	*
	* @param code is the get parameter picked up from facebook 'GET'. Can be null.
	**/
	void CatchStatePara(string paras)
	{
		Debug.Log("caught state para"+paras);
		if(paras == "")
		{
			return;
		}
		else
		{
			if(paras == "LoginFacebookOnLoad")
			{
				status = "Logging in through facebook...";
				networkActionInProgress = true;
				roar.Facebook.DoWebplayerLogin(OnRoarFacebookLoginComplete);
			}
			
			if(paras == "CreateFacebookOnLoad")
			{
				if(username != null || username != "")
				{
					status = "Logging in to Facebook...";
					networkActionInProgress = true;
					roar.Facebook.DoWebplayerCreate(username, OnRoarFacebooCreateComplete);
				}
				else
					status = "Please specify a valid username when creating an account";
			}
			
		}

	}

	/**
	 * Function that is called from javascript and is passed the signedRequest string
	 *
	 *
	 * @param signedRequest is the actual signed request picked up from facebook 'POST'
	 **/
	void CatchFacebookRequest(string oAuth)
	{
		if (oAuth == "")
		{
			
			roar.Facebook.SignedRequestFailed();
			//fire signed request event failed. go for the graph api method.

		}
		else
		{
			roar.Facebook.SetOAuthToken(oAuth);
			
			roar.Facebook.DoPostLoginAction();
		}
	}

	#endregion	

}
