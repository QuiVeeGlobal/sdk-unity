using System.Collections.Generic;
using Roar.Components;
using UnityEngine;

namespace Roar.implementation.Components
{
	public class Gifts : IGifts
	{
		protected DataStore dataStore;
		protected ILogger logger;

		public Gifts (DataStore dataStore, ILogger logger)
		{
			this.dataStore = dataStore;
			this.logger = logger;
		}

		public void Fetch (Roar.RequestCallback callback)
		{
			dataStore.gifts.Fetch (callback);
		}

		public bool HasDataFromServer { get { return dataStore.gifts.HasDataFromServer; } }

		public IList<Foo> List ()
		{
			return dataStore.gifts.List ();
		}


		// Returns the gift Hashtable associated with attribute `id`
		public Foo GetGift (string id)
		{
			return dataStore.gifts.Get (id);
		}
	}
}
