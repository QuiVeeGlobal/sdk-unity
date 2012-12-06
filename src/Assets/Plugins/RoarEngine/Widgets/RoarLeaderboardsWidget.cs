using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar;
using Roar.DomainObjects;

public class RoarLeaderboardsWidget : RoarUIWidget
{
	public delegate void RoarLeaderboardsWidgetHandler();
	public static event RoarLeaderboardsWidgetHandler OnLeaderboardsFetchedStarted;
	public static event RoarLeaderboardsWidgetHandler OnLeaderboardsFetchedComplete;
	
	public delegate void RoarLeaderboardsWidgetSelectedHandler(string leaderboardKey);
	public static event RoarLeaderboardsWidgetSelectedHandler OnLeaderboardSelected;
	
	public enum WhenToFetch { OnEnable, Once, Occassionally, Manual };
	public WhenToFetch whenToFetch = WhenToFetch.OnEnable;
	public float howOftenToFetch = 60;

	public Rect leaderboardItemBounds;
	public float leaderboardItemSpacing;
	public string leaderboardEntryStyle = "LeaderboardEntryButton";
	
	private bool isFetching;
	private float whenLastFetched;
	private Roar.Components.ILeaderboards boards;	
	private IList<Leaderboard> leaderboards;
	private Leaderboard activeLeaderboard;
		
	protected override void Awake()
	{
		base.Awake();
		ScrollViewContentWidth = leaderboardItemBounds.width;
	}
	
	protected override void OnEnable()
	{
		base.OnEnable();
		
		boards = roar.Leaderboards;
		
		if (whenToFetch == WhenToFetch.OnEnable 
		|| (whenToFetch == WhenToFetch.Once && !boards.HasDataFromServer)
		|| (whenToFetch == WhenToFetch.Occassionally && (whenLastFetched == 0 || Time.realtimeSinceStartup - whenLastFetched >= howOftenToFetch))
		)
		{
			Fetch();
		}
	}
	
	public void Fetch()
	{
		isFetching = true;
		if (OnLeaderboardsFetchedStarted != null)
			OnLeaderboardsFetchedStarted();
		boards.Fetch(OnRoarFetchLeaderboardsComplete);
	}
	
	void OnRoarFetchLeaderboardsComplete(Roar.CallbackInfo<IDictionary<string,Roar.DomainObjects.Leaderboard> > info)
	{
		whenLastFetched = Time.realtimeSinceStartup;
		isFetching = false;
		leaderboards = boards.List();
		
		// set default labels
		foreach (Leaderboard leaderboard in leaderboards)
		{
			if (string.IsNullOrEmpty(leaderboard.label))
				leaderboard.label = string.Format("Leaderboard{0}", leaderboard.id);
		}
		
		ScrollViewContentHeight = leaderboards.Count * (leaderboardItemBounds.height + leaderboardItemSpacing);
		
		if (OnLeaderboardsFetchedComplete != null)
			OnLeaderboardsFetchedComplete();
	}
	
	protected override void DrawGUI(int windowId)
	{
		if (isFetching)
		{
			GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Fetching leaderboard data...", "StatusNormal");
			ScrollViewContentHeight = 0;
		}
		else
		{
			if (!boards.HasDataFromServer || leaderboards == null || leaderboards.Count == 0)
			{
				GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "No leaderboards to display", "StatusNormal");
				ScrollViewContentHeight = 0;
			}
			else
			{
				ScrollViewContentHeight = leaderboards.Count * (leaderboardItemBounds.height + leaderboardItemSpacing);
				Rect entry = leaderboardItemBounds;
				foreach (Leaderboard leaderboard in leaderboards)
				{
					if (GUI.Button(entry, leaderboard.label, leaderboardEntryStyle))
					{
						if (OnLeaderboardSelected != null)
							OnLeaderboardSelected(leaderboard.ikey);
					}
					entry.y += entry.height + leaderboardItemSpacing;
				}
			}
		}
	}
}
