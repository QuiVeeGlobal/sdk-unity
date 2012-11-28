using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar.Components;
using Roar.DomainObjects;

namespace Roar.implementation.Components
{
	public class Friends : IFriends 
	{
		protected DataStore dataStore;
		protected ILogger logger;

		public Friends (DataStore dataStore, ILogger logger)
		{
			this.dataStore = dataStore;
			this.logger = logger;
		}

		public void Fetch (Roar.Callback callback)
		{
			dataStore.friends.Fetch(callback);
		}

		public bool HasDataFromServer { get { return dataStore.friends.HasDataFromServer; } }

		public IList<Friend> List ()
		{
			return List (null);
		}

		public IList<Friend> List (Roar.Callback callback)
		{
			if (callback != null)
				callback (new Roar.CallbackInfo<object> (dataStore.friends.List ()));
			return dataStore.friends.List ();
		}

		public Friend GetFriend (string ikey)
		{
			return GetFriend (ikey, null);
		}

		public Friend GetFriend (string ikey, Roar.Callback callback)
		{
			if (callback != null)
				callback (new Roar.CallbackInfo<object> (dataStore.friends.Get (ikey)));
			return dataStore.friends.Get (ikey);
		}
	}
}