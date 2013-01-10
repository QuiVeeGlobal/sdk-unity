using System.Collections;
using System.Collections.Generic;
using Roar.Components;
using UnityEngine;

namespace Roar.implementation.Components
{
	public class Shop : IShop
	{
		protected IWebAPI.IShopActions shopActions;
		protected IDataStore dataStore;
		protected ILogger logger;

		public Shop (IWebAPI.IShopActions shopActions, IDataStore dataStore, ILogger logger)
		{
			this.shopActions = shopActions;
			this.dataStore = dataStore;
			this.logger = logger;
		}

		public void Fetch (Roar.Callback<IDictionary<string,DomainObjects.ShopEntry> > callback)
		{
			dataStore.shop.Fetch (callback);
		}

		public bool HasDataFromServer { get { return dataStore.shop.HasDataFromServer; } }

		public IList<DomainObjects.ShopEntry> List ()
		{
			return dataStore.shop.List();
		}


		// Returns the *object* associated with attribute `key`
		public DomainObjects.ShopEntry GetShopItem (string ikey)
		{
			return dataStore.shop.Get (ikey);
		}

	}

}
