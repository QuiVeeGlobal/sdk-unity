using DC = Roar.implementation.DataConversion;
using System.Collections;
using Roar.DomainObjects;

namespace Roar.implementation
{
	public class DataStore
	{
		public DataStore (IRequestSender api, ILogger logger)
		{
			properties = new DataModel<Foo> ("properties", "user/view", "attribute", null, new DC.XmlToFoo(), api, logger);
			inventory = new DataModel<DomainObjects.Item> ("inventory", "items/list", "item", null, new DC.XmlToInventoryItemHashtable (), api, logger);
			shop = new DataModel<DomainObjects.ShopEntry>("shop", "shop/list", "shopitem", null, new DC.XmlToShopEntry (), api, logger);
			actions = new DataModel<Foo> ("tasks", "tasks/list", "task", null, new DC.XmlToFoo (), api, logger);
			gifts = new DataModel<Foo> ("gifts", "mail/what_can_i_send", "mailable", null, new DC.XmlToFoo (), api, logger);
			achievements = new DataModel<Foo> ("achievements", "user/achievements", "achievement", null, new DC.XmlToFoo (), api, logger);
			leaderboards = new DataModel<DomainObjects.Leaderboard>("leaderboards", "leaderboards/list", "board", null, new DC.XmlToLeaderboard(), api, logger);
			ranking = new DataModel<Foo> ("ranking", "leaderboards/view", "ranking", null, new DC.XmlToFoo (), api, logger);
			friends = new DataModel<DomainObjects.Friend> ("friends", "friends/list", "friend", null, new DC.XmlToFriend (), api, logger);
			cache = new ItemCache ("cache", "items/view", "item", null, new DC.XmlToFoo(), api, logger);
			appStore = new DataModel<Foo> ("appstore", "appstore/shop_list", "shopitem", null, new DC.XmlToFoo (), api, logger);
		}

		public void Clear (bool x)
		{
			properties.Clear (x);
			inventory.Clear (x);
			shop.Clear (x);
			actions.Clear (x);
			gifts.Clear (x);
			achievements.Clear (x);
			leaderboards.Clear (x);
			ranking.Clear (x);
			friends.Clear (x);
			cache.Clear (x);
			appStore.Clear (x);
		}

		public DataModel<Foo> properties;
		public DataModel<DomainObjects.Item> inventory;
		//TODO: Change this to be the WebObject
		public DataModel<DomainObjects.ShopEntry> shop;
		public DataModel<Foo> actions;
		public DataModel<Foo> gifts;
		public DataModel<Foo> achievements;
		//TODO: Change this to be the WebObject
		public DataModel<DomainObjects.Leaderboard> leaderboards;
		public DataModel<Foo> ranking;
		//TODO: Change this to be the WebObject
		public DataModel<DomainObjects.Friend> friends;
		public DataModel<Foo> appStore;
		public ItemCache cache;
	}

}
