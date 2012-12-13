using System.Collections;
using System.Collections.Generic;
using Roar.Components;
using UnityEngine;

namespace Roar.implementation.Components
{
	public class Leaderboards : ILeaderboards
	{
		protected IDataStore dataStore;
		protected ILogger logger;

		public Leaderboards (IDataStore dataStore, ILogger logger)
		{
			this.dataStore = dataStore;
			this.logger = logger;
		}

		public void FetchBoardList( Roar.Callback< ILeaderboardCache > callback )
		{
			dataStore.leaderboards.FetchBoardList(callback);
		}

		public bool HasBoardList { get { return dataStore.leaderboards.HasBoardList; } }

		public IList<DomainObjects.LeaderboardInfo> BoardList ()
		{
			return dataStore.leaderboards.BoardList ();
		}
		
		public IList<Roar.DomainObjects.LeaderboardEntry> GetLeaderboard( string board_id )
		{
			return dataStore.leaderboards.GetLeaderboard(board_id);
		}

	}
}
