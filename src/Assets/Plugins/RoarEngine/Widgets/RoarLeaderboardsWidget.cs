using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar;

public class RoarLeaderboardsWidget : RoarUIWidget
{
	public enum WhenToFetch { OnEnable, Once, Occassionally, Manual };
	public WhenToFetch whenToFetch = WhenToFetch.OnEnable;
	public float howOftenToFetch = 60;

	public Rect leaderboardItemBounds;
	public float leaderboardItemSpacing;
	public string leaderboardEntryStyle = "LeaderboardEntryButton";
	
	private bool isFetching;
	private float whenLastFetched;
	private Roar.Components.ILeaderboards boards;	
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
		boards.Fetch(OnRoarFetchLeaderboardsComplete);
	}
	
	void OnRoarFetchLeaderboardsComplete(Roar.CallbackInfo info)
	{
		whenLastFetched = Time.realtimeSinceStartup;
		isFetching = false;
		ArrayList leaderboardsRaw = boards.List();
		
		Leaderboard leaderboard;
		int cnt = 0;
		foreach (Hashtable data in leaderboardsRaw)
		{
			leaderboard = new Leaderboard();
			leaderboard.id = data["board_id"] as string;
			leaderboard.key = data["ikey"] as string;
			leaderboard.label = data["label"] as string;
			if (leaderboard.label.Length == 0)
				leaderboard.label = string.Format("Leaderboard{0}", leaderboard.id);
			leaderboard.ranking = new Roar.implementation.Components.Ranking(leaderboard.id, roar.DataStore, roar.Logger);
			
			RoarTypesCache.AddLeaderboard(leaderboard);
			cnt++;
		}
		
		ScrollViewContentHeight = cnt * (leaderboardItemBounds.height + leaderboardItemSpacing);
	}
	
	protected override void DrawGUI(int windowId)
	{
		if (isFetching)
		{
			GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Fetching leaderboard data...", "StatusNormal");
		}
		else
		{
			Dictionary<string, Leaderboard> leaderboards = RoarTypesCache.Leaderboards;
			if (!boards.HasDataFromServer || leaderboards == null || leaderboards.Values.Count == 0)
			{
				GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "No leaderboards to display", "StatusNormal");
			}
			else
			{
				Rect entry = leaderboardItemBounds;
				foreach (Leaderboard leaderboard in leaderboards.Values)
				{
					if (GUI.Button(entry, leaderboard.label, leaderboardEntryStyle))
					{
					}
					entry.y += entry.height + leaderboardItemSpacing;
				}
			}
		}
	}
			
	#region Rankings
	/*
	public void FetchRankings(Leaderboard leaderboard, int page)
	{
		isFetching = true;
		activeLeaderboard = leaderboard;
		leaderboard.ranking.Page = page;
		leaderboard.ranking.Fetch(OnRoarFetchRankingsComplete);
	}
	
	void OnRoarFetchRankingsComplete(Roar.CallbackInfo info)
	{
		activeLeaderboard.whenLastFetched = Time.realtimeSinceStartup;
		isFetching = false;
		activeLeaderboard.rankingRaw = activeLeaderboard.ranking.List();
		foreach (Hashtable data in activeLeaderboard.rankingRaw)
		{
			foreach (DictionaryEntry datum in data)
			{
				//Debug.Log(string.Format("{0} => {1}", entry.Key, entry.Value));
				if ((string)datum.Key == "entries")
				{
					ArrayList entries = (ArrayList)datum.Value;
					activeLeaderboard.ranks = new List<Rank>(entries.Count);
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
						activeLeaderboard.ranks.Add(rank);
					}
				}
				else if ((string)datum.Key == "properties")
				{
					Hashtable properties = (Hashtable)datum.Value;
					if (properties.ContainsKey("page"))
						activeLeaderboard.page = System.Convert.ToInt32(properties["page"]);
					if (properties.ContainsKey("num_results"))
						activeLeaderboard.numResults = System.Convert.ToInt32(properties["num_results"]);
					if (properties.ContainsKey("offset"))
						activeLeaderboard.offset = System.Convert.ToInt32(properties["offset"]);
					if (properties.ContainsKey("low_is_high"))
						activeLeaderboard.lowIsHigh = System.Convert.ToBoolean(properties["low_is_high"]);
				}
			}
		}
	}
	
	void DrawRankingsGUI()
	{
	}
	*/
	#endregion
}
