using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoarLeaderboardsModule : RoarModule
{
	public string defaultLeaderboard;
	
	private RoarLeaderboardsWidget leaderboardsWidget;
	private RoarRankingsWidget rankingsWidget;
	
	protected override void Awake ()
	{
		base.Awake ();
		leaderboardsWidget = GetComponentInChildren<RoarLeaderboardsWidget>();
		rankingsWidget = GetComponentInChildren<RoarRankingsWidget>();
	}
	
	void OnEnable()
	{
		if (leaderboardsWidget)
			leaderboardsWidget.enabled = true;
		if (rankingsWidget)
		{
			if (!string.IsNullOrEmpty(defaultLeaderboard))
				rankingsWidget.leaderboardKey = defaultLeaderboard;
			rankingsWidget.enabled = true;
		}
	}
	
	void OnDisable()
	{
		if (leaderboardsWidget)
			leaderboardsWidget.enabled = false;
		if (rankingsWidget)
			rankingsWidget.enabled = false;
	}
	
	protected override void DrawGUI()
	{}
	
#if UNITY_EDITOR
	/*
	void Update()
	{
		if (CurrentMode == Mode.Rankings && Input.GetKeyUp(KeyCode.Escape))
		{
			CurrentMode = Mode.Leaderboards;
		}
	}
	*/
#endif

	#region Utility
	/*
	public override void ResetToDefaultConfiguration()
	{
		base.ResetToDefaultConfiguration();
		horizontalContentAlignment = AlignmentHorizontal.Center;
		horizontalContentOffset = 0;
		verticalContentAlignment = AlignmentVertical.Top;
		verticalContentOffset = 148;
		backgroundType = RoarModule.BackgroundType.ExtentedImage;
		backgroundImageStyle = "RoundedBackground";
		backgroundColor = new Color32(199,199,199,192);
		extendedBackgroundWidth = 564;
		extendedBackgroundHeight = 532;
		whenToFetch = WhenToFetch.OnEnable;
		scrollViewHeight = 448;
		scrollBarPadding = 4;
		leaderboardEntryWidth = 512;
		leaderboardEntryHeight = 64;
		leaderboardEntrySpacing = 2;
		leaderboardEntryStyle = "LeaderboardEntryButton";
		leaderboardTitleRect = new Rect(0,0,512,24);
		leaderboardTitleOnRankingStyle = "LeaderboardTitleOnRanking";
		leaderboardTitlePadding = 8;
		leaderboardEntryWidth = 512;
		leaderboardEntryHeight = 64;
		rankingEntrySpacing = 2;
		rankingEntryPlayerRankStyle = "LeaderboardRankingPlayerRank";
		rankingEntryPlayerNameStyle = "LeaderboardRankingPlayerName";
		rankingEntryPlayerScoreStyle = "LeaderboardRankingPlayerScore";
	}
	*/
	#endregion	
}
