using DC = Roar.implementation.DataConversion;
using System.Collections;
using System.Collections.Generic;
using Roar.DomainObjects;


public interface IDomToCache<DT,CT>
{
	Dictionary<string,CT> convert(DT d);
}

public class UserViewToProperty : IDomToCache<Roar.WebObjects.User.ViewResponse, Property>
{
	public Dictionary<string,Property> convert(Roar.WebObjects.User.ViewResponse d)
	{
		Dictionary<string,Property> retval = new Dictionary<string, Property>();
		//TODO: Implement this
		return retval;
	}
}

public interface IDomGetter<DT>
{
	void get( ZWebAPI.Callback<DT> cb );
}

public class UserViewGetter : IDomGetter<Roar.WebObjects.User.ViewResponse>
{
	public UserViewGetter( ZWebAPI api ) 
	{
		this.api = api;
	}
	
	protected ZWebAPI api;
	public void get( ZWebAPI.Callback<Roar.WebObjects.User.ViewResponse> cb )
	{
		Roar.WebObjects.User.ViewArguments args = new Roar.WebObjects.User.ViewArguments();
		api.user.view( args, cb );
	}
}

public class Property
{
//TODO: Fix up the types in here!
	public string value;
	public string type;
	public string max;
	public string min;
	public string regen_every;
	public string label;
	
}


namespace Roar.implementation
{
	public class DataStore
	{
		public DataStore (ZWebAPI zwebapi, IRequestSender api, ILogger logger)
		{
			//This should convert from the response type to key-value pairs
			
			properties = new DataModelZ<Property,Roar.WebObjects.User.ViewResponse> ("properties", new UserViewGetter(zwebapi), new UserViewToProperty(), logger);
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

		public DataModelZ<Property,Roar.WebObjects.User.ViewResponse> properties;
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
