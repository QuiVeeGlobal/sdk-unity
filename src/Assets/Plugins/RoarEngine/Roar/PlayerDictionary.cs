using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar.DomainObjects;

public class PlayerDictionary {

	Dictionary<string, IList<LeaderboardExtraProperties>>  playerIDleaderboardDict; 
	Dictionary<string, Roar.DomainObjects.Friend> playerIDFriendDict;
	Dictionary<string, string> playerIDFriendInviteDict;
	
	public void Init()
	{
		playerIDleaderboardDict = new Dictionary<string, IList<LeaderboardExtraProperties>>();
		playerIDFriendDict = new Dictionary<string, Friend>();
		playerIDFriendInviteDict = new Dictionary<string, string>();
		
		RoarManager.leaderboardsChangeEvent += LoadChangedLeaderboard;
		RoarManager.roarServerFriendRequestEvent+= delegate(Roar.Events.FriendRequestEvent friendReq) {
			if(playerIDFriendInviteDict.ContainsKey(friendReq.from_player_id))
			{
				playerIDFriendInviteDict.Add(friendReq.from_player_id, friendReq.name);
			}
		};
	}
	
	void LoadChangedLeaderboard()
	{
		IList<LeaderboardData> boardList = DefaultRoar.Instance.DataStore.ranking.List();
		
		foreach(LeaderboardData board in boardList)
		{
			foreach(LeaderboardEntry e in board.entries)
			{
				if(!playerIDleaderboardDict.ContainsKey(e.player_id))
				{
					playerIDleaderboardDict.Add(e.player_id, e.properties);
				}
			}
		}
	}
	
	void UpdateFriendsDictionary()
	{
		IList<Roar.DomainObjects.Friend> friendList = DefaultRoar.Instance.Friends.List(); //lists friends and friend invites.
		
		playerIDFriendDict.Clear();
		
		foreach(Friend f in friendList)
		{
			playerIDFriendDict.Add(f.player_id, f);
		}
	}
}
