using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar.Components;
using Roar.DomainObjects;

namespace Roar.implementation.Components
{
	public class Friends : IFriends 
	{
		protected IDataStore dataStore;
		protected ILogger logger;

		public Friends (IDataStore dataStore, ILogger logger)
		{
			this.dataStore = dataStore;
			this.logger = logger;
		}

		public void Fetch (Roar.Callback< IDictionary<string,DomainObjects.Friend> > callback)
		{
			dataStore.friends.Fetch(callback);
		}

		public bool HasDataFromServer { get { return dataStore.friends.HasDataFromServer; } }

		public IList<Friend> List ()
		{
			return dataStore.friends.List();
		}


		public Friend GetFriend (string ikey)
		{
			return dataStore.friends.Get(ikey);
		}
	}
}