using UnityEngine;
using System.Collections;
using Roar.Components;

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

		public ArrayList List ()
		{
			return List (null);
		}

		public ArrayList List (Roar.Callback callback)
		{
			if (callback != null)
				callback (new Roar.CallbackInfo<object> (dataStore.friends.List ()));
			return dataStore.friends.List ();
		}

		public Hashtable GetFriend (string ikey)
		{
			return GetFriend (ikey, null);
		}

		public Hashtable GetFriend (string ikey, Roar.Callback callback)
		{
			if (callback != null)
				callback (new Roar.CallbackInfo<object> (dataStore.friends.Get (ikey)));
			return dataStore.friends.Get (ikey);
		}
	}
}