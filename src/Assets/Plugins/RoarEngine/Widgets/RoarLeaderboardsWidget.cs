using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar;
using Roar.DomainObjects;
using System.Linq;

public class RoarLeaderboardsWidget : RoarUIWidget
{
	public delegate void RoarLeaderboardsWidgetHandler();
	
	public delegate void RoarLeaderboardsWidgetSelectedHandler(string leaderboardKey, string leaderboardName);
	
	public enum WhenToFetch { OnEnable, Once, Occassionally, Manual };
	public WhenToFetch whenToFetch = WhenToFetch.OnEnable;
	public float howOftenToFetch = 60;

	public Rect leaderboardItemBounds = new Rect(0, 0, 450, 30);
	public float leaderboardItemSpacing = 4;
	public string leaderboardEntryStyle = "DefaultButton";
	
	private bool isFetching;
	private float whenLastFetched;
	private Roar.Components.ILeaderboards boards;	
	private IList<LeaderboardInfo> leaderboards;
	private LeaderboardData activeLeaderboard;

	public Rect rankingItemBounds = new Rect(0, 0, 450, 50);
	public float rankingItemSpacing = 4;
	public string rankingEntryPlayerRankStyle = "LeaderboardRankingPlayerRank";
	public string rankingEntryPlayerNameStyle = "LeaderboardRankingPlayerName";
	public string rankingEntryPlayerScoreStyle = "LeaderboardRankingPlayerScore";

	public string previousButtonLabel = "Previous page";
	public string previousButtonStyle = "DefaultPillboxStyle";
	public string nextButtonLabel = "Next page";
	public string nextButtonStyle = "DefaultPillboxStyle";

	public string customDataFormat = "{0}:{1}";
	public string rankFormat = "{0}";
	public string rankStyle = "LeaderboardRankingRank";
	public string valueFormat = "{0}";
	public string valueStyle = "LeaderboardRankingValue";

	public float valueWidth =100;
	public float rankColumnWidth = 40;
	public float interColumnSeparators =40;
	public float divideHeight = 20;
	public float sectionHeight = 50;
	public float topSeparation = 5;
	public float footerHeight = 60;
	public float buttonHeight = 30;

	public string leaderboardId = string.Empty;
	public string leaderboardName = "";
	public bool hasSelectedLeaderboard = false;
	public int page = 1;

	private bool isFetchingRankings;
	private IList<LeaderboardEntry> leaderboard;
		
	protected override void Awake()
	{
		base.Awake();
		ScrollViewContentWidth = leaderboardItemBounds.width;
	}
	
	void Reset()
	{
		bounds = new Rect(0,100,512,386);
		displayName = "Leaderboard";
		horizontalAlignment = AlignmentHorizontal.Left;
		verticalAlignment = AlignmentVertical.Center;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		
		boards = roar.Leaderboards;
		
		//Do we need to fetch the list of boards?
		if (whenToFetch == WhenToFetch.OnEnable 
		|| (whenToFetch == WhenToFetch.Once && !boards.HasBoardList)
		|| (whenToFetch == WhenToFetch.Occassionally && (whenLastFetched == 0 || Time.realtimeSinceStartup - whenLastFetched >= howOftenToFetch))
		)
		{
			FetchBoardList();
		}
	}
	
	public void FetchBoardList()
	{
		networkActionInProgress = true;

		isFetching = true;
		OnLeaderboardsFetchedStarted();
		boards.FetchBoardList(OnRoarFetchLeaderboardsComplete);
	}
	
	void OnRoarFetchLeaderboardsComplete(Roar.CallbackInfo<Roar.Components.ILeaderboards> cache)
	{
		networkActionInProgress = false;
		whenLastFetched = Time.realtimeSinceStartup;
		isFetching = false;
		leaderboards = boards.BoardList();
		
		// set default labels
		foreach (LeaderboardInfo leaderboard in leaderboards)
		{
			if (string.IsNullOrEmpty(leaderboard.label))
				leaderboard.label = string.Format("Leaderboard{0} - {1}", leaderboard.board_id, leaderboard.ikey);
		}
		
		ScrollViewContentHeight = leaderboards.Count * (leaderboardItemBounds.height + leaderboardItemSpacing);
		
		OnLeaderboardsFetchedComplete();
	}

	void OnLeaderboardsFetchedStarted()
	{}

	void OnLeaderboardsFetchedComplete()
	{
		FetchRankingsIfRequired();
	}

	void OnLeaderboardSelected(string leaderboardId, string name)
	{
		hasSelectedLeaderboard = true;
		this.leaderboardName = name;
		this.leaderboardId = leaderboardId;
		this.page = 1;
		FetchRankingsIfRequired();
	}

	void FetchRankingsIfRequired()
	{
		subheaderName =leaderboardName;

		if (!string.IsNullOrEmpty(leaderboardId))
		{
			leaderboard = boards.GetLeaderboard(leaderboardId,page);
		}
		
		if (leaderboard == null)
		{
			FetchRankings();
		}
	}

	public void FetchRankings()
	{
		if (string.IsNullOrEmpty(leaderboardId))
		{
			if (Debug.isDebugBuild)
				Debug.Log("leaderboardId not set!");
			return;
		}
		isFetchingRankings = true;
		networkActionInProgress = true;
		boards.FetchBoard( leaderboardId, page, OnRoarFetchLeaderboardComplete );
	}

	void OnRoarFetchLeaderboardComplete(Roar.CallbackInfo<Roar.Components.ILeaderboards> info)
	{
		//TODO: Handle errors!
		leaderboard = info.data.GetLeaderboard(leaderboardId, page);
		isFetchingRankings = false;
		networkActionInProgress = false;
	}
	
	protected override void DrawGUI(int windowId)
	{
		
		if(!hasSelectedLeaderboard)
		{
			if (isFetching)
			{
				GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Fetching leaderboard data...", "StatusNormal");
				ScrollViewContentHeight = contentBounds.height;
			}
			else
			{
				if (!boards.HasBoardList || leaderboards == null || leaderboards.Count == 0)
				{
					GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "No leaderboards to display", "StatusNormal");
					ScrollViewContentHeight = contentBounds.height;
				}
				else
				{
					ScrollViewContentHeight = Mathf.Max(contentBounds.height, leaderboards.Count * (leaderboardItemBounds.height + leaderboardItemSpacing));
					Rect entry = leaderboardItemBounds;
					foreach (LeaderboardInfo leaderboard in leaderboards)
					{
						if (GUI.Button(entry, leaderboard.label, leaderboardEntryStyle))
						{
							OnLeaderboardSelected(leaderboard.board_id, leaderboard.ikey);
						}
						entry.y += entry.height + leaderboardItemSpacing;
					}
				}
			}
		}
		else
		{
			
			if (isFetchingRankings)
			{
				GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Fetching leaderboard ranking data...", "StatusNormal");
				ScrollViewContentHeight = contentBounds.height;
			}
			else
			{
	
				GUI.Box(new Rect(0, 0, contentBounds.width, divideHeight), new GUIContent(""), "DefaultSeparationBar");
				Vector2 rankW = GUI.skin.FindStyle("DefaultSeparationBarText").CalcSize(new GUIContent("Rank"));
				GUI.Label(new Rect(interColumnSeparators - rankW.x/2, 0, valueWidth, divideHeight), "RANK", "DefaultSeparationBarText");
	
				GUI.Label(new Rect(interColumnSeparators*2 +rankW.x, 0, valueWidth, divideHeight), "NAME", "DefaultSeparationBarText");
	
				float heightSoFar = divideHeight;
	
				bool requires_refetch = false;
	
				if(leaderboard != null)
				foreach (LeaderboardEntry item in leaderboard)
				{
					Vector2 rank = GUI.skin.FindStyle("DefaultLightContentText").CalcSize(new GUIContent(item.rank.ToString()));
					Vector2 labSize = GUI.skin.FindStyle("DefaultLightContentText").CalcSize(new GUIContent(item.player_id));

					string prop_string = string.Join(
							"\n",
							item.properties.Select( p => ( string.Format( customDataFormat, p.ikey, p.value ) ) ).ToArray()
							);

					GUI.Box(new Rect(0, heightSoFar, contentBounds.width, sectionHeight), new GUIContent(""), "DefaultHorizontalSection");
					float ySoFar = heightSoFar + topSeparation;

					GUI.Box(new Rect(interColumnSeparators, ySoFar, rank.x, sectionHeight), item.rank.ToString(), "DefaultHeavyContentText");
					GUI.Box(new Rect(2*interColumnSeparators + rank.x, ySoFar, labSize.x, sectionHeight), prop_string, "DefaultHeavyContentText");

					GUI.Label (new Rect(3*interColumnSeparators + rank.x+labSize.x, heightSoFar, valueWidth, labSize.y),  item.value.ToString(), "DefaultHeavyContentText");

					if(GUI.Button(new Rect(4*interColumnSeparators + rank.x + labSize.x+valueWidth, ySoFar, valueWidth, buttonHeight), "View Prof", "DefaultButton"))
					{
						GameObject.Find("ProfileViewerWidget").SendMessage("ViewProfile", item.player_id, SendMessageOptions.RequireReceiver);
					}
					heightSoFar += sectionHeight + topSeparation;
				}

				if(leaderboard != null)
				{
					GUI.Box(new Rect(0, heightSoFar, contentBounds.width, footerHeight), new GUIContent(""), "DefaultFooterStyle");

					GUILayout.BeginArea(new Rect(0, heightSoFar, contentBounds.width, footerHeight));
					GUILayout.BeginVertical();
					GUILayout.FlexibleSpace();

					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();

					if( page==1 ) { GUI.enabled = false; }
					if( GUILayout.Button(previousButtonLabel, previousButtonStyle) )
					{
						page = page - 1;
						requires_refetch = true;
					}
					GUI.enabled = true;

					GUILayout.Space(interColumnSeparators);

					if( GUILayout.Button("back", previousButtonStyle) )
					{
						hasSelectedLeaderboard = false;
					}

					GUILayout.Space(interColumnSeparators);
					if( leaderboard.Count == 0 ) { GUI.enabled = false; }
					if( GUILayout.Button( nextButtonLabel, nextButtonStyle) )
					{
						page = page +1;
						requires_refetch = true;
					}
					GUI.enabled = true;
					GUILayout.Space(interColumnSeparators);
					GUILayout.EndHorizontal();
					GUILayout.FlexibleSpace();
					GUILayout.EndVertical();
					GUILayout.EndArea();
					heightSoFar += footerHeight;
				}

				if(alwaysShowVerticalScrollBar)
					if(heightSoFar < contentBounds.height)
						ScrollViewContentHeight = contentBounds.height;
					else
						ScrollViewContentHeight = heightSoFar;
				else
					ScrollViewContentHeight = heightSoFar;

				if(requires_refetch) FetchRankingsIfRequired();
			}
		}
		
		
	}
}
