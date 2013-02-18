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
	private string username = string.Empty;
	private string password = string.Empty;
	public int loginBoxSpacing = 10;
	public float labelHeight= 30;
	public float statusWidth = 400;
	public float fieldHeight = 30;
	public float fieldWidth = 140;
	public float buttonWidth = 200;
	public float buttonHeight = 40;
	public float footerSpacing = 50;
	
	public string buttonStyle="DefaultButton";
	public string boxStyle="DefaultTextArea";
	public string loginLabelStyle = "DefaultHeavyContentText";
	public string statusStyle = "LoginStatus";
	
	
	
	
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
		
		
	}
	
		
	protected override void DrawGUI(int windowId)
	{
		{
			float totalSpaceAvailableY = contentBounds.height; //the total space available between header and footer.
			
			totalSpaceAvailableY = totalSpaceAvailableY - 2*fieldHeight - 2* labelHeight - loginBoxSpacing - footerSpacing;
			
			
			Rect currentRect = new Rect(contentBounds.width/2 - fieldWidth/2, totalSpaceAvailableY/2, fieldWidth, labelHeight);
//			GUI.Label(currentRect, status, statusStyle);
//			currentRect.y += 100;
			
			if(networkActionInProgress)
				GUI.enabled = false;
			GUI.Label(currentRect, "Username", loginLabelStyle);
			currentRect.y += labelHeight;
			currentRect.width = fieldWidth;
			currentRect.height = fieldHeight;
			//currentRect.x = contentBounds.width/2 - fieldWidth/2;
			
			username = GUI.TextField(currentRect, username, boxStyle);
			currentRect.y+=fieldHeight;
			
			currentRect.y += loginBoxSpacing;
			
			currentRect.width = fieldWidth;
			currentRect.height = labelHeight;
			
			GUI.Label(currentRect, "Password", loginLabelStyle);
			currentRect.y += labelHeight;
			
			currentRect.width = fieldWidth;
			currentRect.height = fieldHeight;
			
			
			password = GUI.PasswordField(currentRect, password, '*', boxStyle);
			
			currentRect.y += fieldHeight;
			currentRect.y += labelHeight;
			
			currentRect.x = 0;
			currentRect.width = contentBounds.width;
			
			GUI.Label(currentRect, status, statusStyle);
			
			
			currentRect.y = contentBounds.height - footerSpacing;
			
			GUI.Box(new Rect(0, contentBounds.height - footerSpacing, contentBounds.width, contentBounds.height), new GUIContent(""), "DefaultFooterStyle");
			
			currentRect.width = buttonWidth;
			currentRect.height = buttonHeight;
			currentRect.x = contentBounds.width - buttonWidth - 50; //change when design comes.
			currentRect.y = contentBounds.height - buttonHeight/2 - footerSpacing/2;
			
			GUI.enabled = username.Length > 0 && password.Length > 0 && !networkActionInProgress;
			
			if ((GUI.Button(currentRect, "Log In", buttonStyle) || ( Event.current.keyCode == KeyCode.Return)) && !networkActionInProgress)
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
				currentRect.x -= buttonWidth + 50;
			if (GUI.Button(currentRect, "Create", buttonStyle) && !networkActionInProgress)
			{
	
				status = "Creating new player account...";
				networkActionInProgress = true;
				roar.User.Create(username, password, OnRoarAccountCreateComplete);
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

}
