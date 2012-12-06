using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoarFriendsListWidget : RoarUIWidget
{
	public enum WhenToFetch { OnEnable, Once, Occassionally, Manual };
	public WhenToFetch whenToFetch = WhenToFetch.Occassionally;
	public float howOftenToFetch = 60;
	
	private bool isFetching;
	private float whenLastFetched;
	private Roar.Components.IFriends friends;
	
	protected override void OnEnable ()
	{
		base.OnEnable ();
		friends = DefaultRoar.Instance.Friends;
		if (friends != null)
		{
			if (whenToFetch == WhenToFetch.OnEnable 
			|| (whenToFetch == WhenToFetch.Once && !friends.HasDataFromServer)
			|| (whenToFetch == WhenToFetch.Occassionally && (whenLastFetched == 0 || Time.realtimeSinceStartup - whenLastFetched >= howOftenToFetch))
			)
			{
				Fetch();
			}
		}
		else if (Debug.isDebugBuild)
		{
			Debug.LogWarning("Friends data is null; unable to render friends list widget");
		}
	}
	
	public void Fetch()
	{
		isFetching = true;
		friends.Fetch(OnRoarFetchFriendsComplete);
	}
	
	void OnRoarFetchFriendsComplete(Roar.CallbackInfo< IDictionary<string,Roar.DomainObjects.Friend> > info)
	{
		whenLastFetched = Time.realtimeSinceStartup;
		isFetching = false;
	}
	
	protected override void DrawGUI(int windowId)
	{
		if (!IsLoggedIn) return; // attempt draw nothing if not logged in
		if (isFetching)
		{
			GUI.Label(new Rect(Screen.width/2f - 256,Screen.height/2f - 32,512,64), "Fetching friends...", "StatusNormal");
		}
	}
}
