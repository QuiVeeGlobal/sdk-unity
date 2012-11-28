using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar;
using Roar.DomainObjects;

public class RoarRankingsWidget : RoarUIWidget
{
	public enum WhenToFetch { OnEnable, Once, Occassionally, Manual };
	public WhenToFetch whenToFetch = WhenToFetch.OnEnable;
	public float howOftenToFetch = 60;
	
	public Rect rankingItemBounds;
	public float rankingItemSpacing;
	public string rankingEntryPlayerRankStyle = "LeaderboardRankingPlayerRank";
	public string rankingEntryPlayerNameStyle = "LeaderboardRankingPlayerName";
	public string rankingEntryPlayerScoreStyle = "LeaderboardRankingPlayerScore";

	//public string rankingNavigatePageValueStyle = "LabelPageValue";
	//public string rankingNavigateLeftButtonStyle = "ButtonNavigatePageLeft";
	//public string rankingNavigateRightButtonStyle = "ButtonNavigatePageRight";
	
	public string leaderboardKey = string.Empty;
	public int page = 1;
	
	private Roar.Components.ILeaderboards boards;	
	private bool isFetching;
	private Leaderboard leaderboard;
	
	protected override void Awake()
	{
		base.Awake();
		ScrollViewContentWidth = rankingItemBounds.width;
	}
	
	protected override void OnEnable()
	{
		base.OnEnable();
		RoarLeaderboardsWidget.OnLeaderboardsFetchedStarted += OnLeaderboardsFetchedStarted;
		RoarLeaderboardsWidget.OnLeaderboardsFetchedComplete += OnLeaderboardsFetchedComplete;
		RoarLeaderboardsWidget.OnLeaderboardSelected += OnLeaderboardSelected;
		
		boards = roar.Leaderboards;
		
		FetchIfPossible();
	}
	
	protected override void OnDisable()
	{
		RoarLeaderboardsWidget.OnLeaderboardsFetchedStarted -= OnLeaderboardsFetchedStarted;
		RoarLeaderboardsWidget.OnLeaderboardsFetchedComplete -= OnLeaderboardsFetchedComplete;
		RoarLeaderboardsWidget.OnLeaderboardSelected -= OnLeaderboardSelected;
	}
	
	void OnLeaderboardsFetchedStarted()
	{}
	
	void OnLeaderboardsFetchedComplete()
	{
		FetchIfPossible();
	}
	
	void OnLeaderboardSelected(string leaderboardKey)
	{
		this.leaderboardKey = leaderboardKey;
		this.page = 1;
		FetchIfPossible();
	}
	
	void FetchIfPossible()
	{
		if (!string.IsNullOrEmpty(leaderboardKey))
		{
			leaderboard = boards.GetLeaderboard(leaderboardKey);
		}
		
		if (leaderboard != null)
		{
			if (whenToFetch == WhenToFetch.OnEnable 
			|| (whenToFetch == WhenToFetch.Once && !boards.HasDataFromServer)
//			|| (whenToFetch == WhenToFetch.Occassionally && (leaderboard.whenLastFetched == 0 || Time.realtimeSinceStartup - leaderboard.whenLastFetched >= howOftenToFetch))
			)
			{
				Fetch();
			}
		}
	}
	
	public void Fetch()
	{
		if (!string.IsNullOrEmpty(leaderboardKey))
		{
			leaderboard = boards.GetLeaderboard(leaderboardKey);
			/*
			if (leaderboard != null)
			{
				isFetching = true;
				leaderboard.ranking.Page = page;
				leaderboard.ranking.Fetch(OnRoarFetchRankingsComplete);
			}
			*/
		}
	}
	
	public void Fetch(int page)
	{
		this.page = page;
		Fetch();
	}
	
	public void Fetch(string leaderboardKey, int page)
	{
		this.page = page;
		this.leaderboardKey = leaderboardKey;
		Fetch();
	}
	
	/*
	void OnRoarFetchRankingsComplete(Roar.CallbackInfo info)
	{
		leaderboard.whenLastFetched = Time.realtimeSinceStartup;
		isFetching = false;
		leaderboard.rankingRaw = leaderboard.ranking.List();
		Title = leaderboard.label;
		int cnt = 0;
		foreach (Hashtable data in leaderboard.rankingRaw)
		{
			foreach (DictionaryEntry datum in data)
			{
				//Debug.Log(string.Format("{0} => {1}", entry.Key, entry.Value));
				if ((string)datum.Key == "entries")
				{
					ArrayList entries = (ArrayList)datum.Value;
					leaderboard.ranks = new List<Rank>(entries.Count);
					Rank rank;
					foreach (Hashtable entry in entries)
					{
						rank = new Rank();
						foreach (DictionaryEntry kv in entry)
						{
							//Debug.Log(string.Format("{0} => {1}", kv.Key, kv.Value));
							string k = kv.Key as string;
							if (k == "rank")
							{
								rank.rank = System.Convert.ToInt32(kv.Value);
							}
							else if (k == "player_id")
							{
								rank.playerId = kv.Value as string;
							}
							else if (k == "player_name")
							{
								rank.playerName = kv.Value as string;
							}
							else if (k == "value")
							{
								rank.value = kv.Value as string;
							}
						}
						leaderboard.ranks.Add(rank);
						cnt++;
					}
				}
				else if ((string)datum.Key == "properties")
				{
					Hashtable properties = (Hashtable)datum.Value;
					if (properties.ContainsKey("page"))
						leaderboard.page = System.Convert.ToInt32(properties["page"]);
					if (properties.ContainsKey("num_results"))
						leaderboard.numResults = System.Convert.ToInt32(properties["num_results"]);
					if (properties.ContainsKey("offset"))
						leaderboard.offset = System.Convert.ToInt32(properties["offset"]);
					if (properties.ContainsKey("low_is_high"))
						leaderboard.lowIsHigh = System.Convert.ToBoolean(properties["low_is_high"]);
				}
			}
		}
		
		ScrollViewContentHeight = cnt * (rankingItemBounds.height + rankingItemSpacing);
	}
	*/
	
	protected override void DrawGUI(int windowId)
	{
		if (isFetching)
		{
			GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Fetching leaderboard ranking data...", "StatusNormal");
			ScrollViewContentHeight = 0;
		}
		else
		{
			if (leaderboard == null || leaderboard.entries == null || leaderboard.entries.Count == 0)
			{
				GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "No ranking data.", "StatusNormal");
				ScrollViewContentHeight = 0;
			}
			else
			{
				ScrollViewContentHeight = leaderboard.entries.Count * (rankingItemBounds.height + rankingItemSpacing);
				Rect entryRect = rankingItemBounds;
				foreach (LeaderboardEntry leaderboardEntry in leaderboard.entries)
				{
					GUI.Label(entryRect, leaderboardEntry.rank.ToString(), rankingEntryPlayerRankStyle);
					//GUI.Label(entry, leaderboardEntry.playerName, rankingEntryPlayerNameStyle);
					GUI.Label(entryRect, leaderboardEntry.value.ToString(), rankingEntryPlayerScoreStyle);
					
					entryRect.y += entryRect.height + rankingItemSpacing;
				}
				//useScrollView = utilizeScrollView && ((entry.y + entry.height) > contentBounds.height);
			}
		}
	}
	
	/*
	void GUIPageNavigator(Rect rect)
	{
		GUIStyle navigateButtonStyle;
		float w = rect.width;
		//float h = rect.height;

		GUI.BeginGroup(rect);
		rect.x = 0;
		rect.y = 0;
		
		navigateButtonStyle = skin.FindStyle(rankingNavigateLeftButtonStyle);
		rect.width = navigateButtonStyle.fixedWidth;
		rect.height = navigateButtonStyle.fixedHeight;
		if (activeLeaderboard.HasPrevious)
		{
			if (GUI.Button(rect, string.Empty, rankingNavigateLeftButtonStyle))
			{
				FetchRankings(activeLeaderboard, activeLeaderboard.page + 1);
			}
		}
		
		rect.width = w;
		if (activeLeaderboard.HasPrevious || activeLeaderboard.HasNext)
			GUI.Label(rect, activeLeaderboard.page.ToString(), rankingNavigatePageValueStyle);

		navigateButtonStyle = skin.FindStyle(rankingNavigateRightButtonStyle);
		rect.width = navigateButtonStyle.fixedWidth;
		rect.height = navigateButtonStyle.fixedHeight;
		rect.x = w - rect.width;
		if (activeLeaderboard.HasNext)
		{
			if (GUI.Button(rect, string.Empty, rankingNavigateRightButtonStyle))
			{
				FetchRankings(activeLeaderboard, activeLeaderboard.page - 1);
			}
		}
		
		GUI.EndGroup();
	}
	*/
}
