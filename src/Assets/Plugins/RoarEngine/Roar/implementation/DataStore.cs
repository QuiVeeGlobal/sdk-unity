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

public class ItemsListToItem : IDomToCache<Roar.WebObjects.Items.ListResponse,Roar.DomainObjects.InventoryItem>
{
	public Dictionary<string, Roar.DomainObjects.InventoryItem> convert( Roar.WebObjects.Items.ListResponse d)
	{
		Dictionary<string,Roar.DomainObjects.InventoryItem> retval = new Dictionary<string, Roar.DomainObjects.InventoryItem>();
		//TODO: Implement this
		return retval;
	}
}

public class ShopListToShopEntry : IDomToCache<Roar.WebObjects.Shop.ListResponse,Roar.DomainObjects.ShopEntry>
{
	public Dictionary<string, Roar.DomainObjects.ShopEntry> convert( Roar.WebObjects.Shop.ListResponse d)
	{
		Dictionary<string,Roar.DomainObjects.ShopEntry> retval = new Dictionary<string, Roar.DomainObjects.ShopEntry>();
		//TODO: Implement this
		return retval;
	}
}

public class LeaderboardListToLeaderboard : IDomToCache<Roar.WebObjects.Leaderboards.ListResponse,Roar.DomainObjects.Leaderboard>
{
	public Dictionary<string, Roar.DomainObjects.Leaderboard> convert( Roar.WebObjects.Leaderboards.ListResponse d)
	{
		Dictionary<string,Roar.DomainObjects.Leaderboard> retval = new Dictionary<string, Roar.DomainObjects.Leaderboard>();
		//TODO: Implement this
		return retval;
	}
}

public class FriendsListToFriend : IDomToCache<Roar.WebObjects.Friends.ListResponse,Roar.DomainObjects.Friend>
{
	public Dictionary<string, Roar.DomainObjects.Friend> convert( Roar.WebObjects.Friends.ListResponse d)
	{
		Dictionary<string,Roar.DomainObjects.Friend> retval = new Dictionary<string, Roar.DomainObjects.Friend>();
		//TODO: Implement this
		return retval;
	}
}

public class ItemsViewToItemPrototype: IDomToCache<Roar.WebObjects.Items.ViewResponse,Roar.DomainObjects.ItemPrototype>
{
	public Dictionary<string, Roar.DomainObjects.ItemPrototype> convert( Roar.WebObjects.Items.ViewResponse d)
	{
		Dictionary<string,Roar.DomainObjects.ItemPrototype> retval = new Dictionary<string, Roar.DomainObjects.ItemPrototype>();
		//TODO: Implement this
		return retval;
	}
}

public class FooToFoo : IDomToCache<Foo,Foo>
{
	public Dictionary<string, Foo> convert( Foo d)
	{
		throw new System.NotImplementedException();
	}
}


public interface IDomGetter<DT>
{
	void get( ZWebAPI.Callback<DT> cb );
	

}

	
	
public class GenericGetter<T,A> : IDomGetter<T> where A:new()
{
	public delegate void GetFn( A args, ZWebAPI.Callback<T> cb );
	
	GetFn getfn;
	
	public GenericGetter( GetFn getfn ) 
	{
		this.getfn = getfn;
	}
	
	
	public void get( ZWebAPI.Callback<T> cb )
	{
		A args = new A();
		getfn(args,cb);
	}
}

public class UserViewGetter : GenericGetter<Roar.WebObjects.User.ViewResponse, Roar.WebObjects.User.ViewArguments>
{
	public UserViewGetter( ZWebAPI api ) : base( api.user.view ) {}
}

public class ItemsListGetter : GenericGetter<Roar.WebObjects.Items.ListResponse, Roar.WebObjects.Items.ListArguments>
{
	public ItemsListGetter( ZWebAPI api ) :base( api.items.list ) {}
}

public class ShopListGetter : GenericGetter<Roar.WebObjects.Shop.ListResponse, Roar.WebObjects.Shop.ListArguments>
{
	public ShopListGetter( ZWebAPI api ) :base( api.shop.list ) {}
}

public class LeaderboardListGetter : GenericGetter<Roar.WebObjects.Leaderboards.ListResponse, Roar.WebObjects.Leaderboards.ListArguments>
{
	public LeaderboardListGetter( ZWebAPI api ) : base( api.leaderboards.list ) {}
}

public class FriendsListGetter : GenericGetter<Roar.WebObjects.Friends.ListResponse, Roar.WebObjects.Friends.ListArguments>
{
	public FriendsListGetter( ZWebAPI api ) : base( api.friends.list ) {}
}

public class ItemsViewGetter : GenericGetter<Roar.WebObjects.Items.ViewResponse, Roar.WebObjects.Items.ViewArguments>
{
	public ItemsViewGetter( ZWebAPI api ) : base( api.items.view ) {}
}

public class FooGetter : IDomGetter<Foo>
{
	public FooGetter( ZWebAPI api ) 
	{
		this.api = api;
	}
	
	protected ZWebAPI api;
	public void get( ZWebAPI.Callback<Foo> cb )
	{
		throw new System.NotImplementedException();
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
			
			properties = new DataModel<Property,Roar.WebObjects.User.ViewResponse> ("properties", new UserViewGetter(zwebapi), new UserViewToProperty(), logger);
			inventory = new DataModel<DomainObjects.InventoryItem,Roar.WebObjects.Items.ListResponse> ("inventory", new ItemsListGetter(zwebapi), new ItemsListToItem(), logger);
			shop = new DataModel<DomainObjects.ShopEntry,WebObjects.Shop.ListResponse>("shop", new ShopListGetter(zwebapi), new ShopListToShopEntry(), logger);
			actions = new DataModel<Foo,Foo> ("tasks", new FooGetter(zwebapi), new FooToFoo(), logger);
			gifts = new DataModel<Foo,Foo> ("gifts", new FooGetter(zwebapi), new FooToFoo(), logger);
			achievements = new DataModel<Foo,Foo> ("achievements", new FooGetter(zwebapi), new FooToFoo(), logger);
			leaderboards = new DataModel<DomainObjects.Leaderboard,WebObjects.Leaderboards.ListResponse>( "leaderboards", new LeaderboardListGetter(zwebapi), new LeaderboardListToLeaderboard(), logger);
			ranking = new DataModel<Foo,Foo> ("ranking", new FooGetter(zwebapi), new FooToFoo(), logger);
			friends = new DataModel<DomainObjects.Friend,WebObjects.Friends.ListResponse> ("friends",  new FriendsListGetter(zwebapi), new FriendsListToFriend(), logger);
			cache = new ItemCache ("cache", new ItemsViewGetter(zwebapi), new ItemsViewToItemPrototype(), logger);
			appStore = new DataModel<Foo,Foo> ("appstore", new FooGetter(zwebapi), new FooToFoo(), logger);
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
		
		

		public DataModel<Property,Roar.WebObjects.User.ViewResponse> properties;
		public DataModel<DomainObjects.InventoryItem,Roar.WebObjects.Items.ListResponse> inventory;
		public DataModel<DomainObjects.ShopEntry,Roar.WebObjects.Shop.ListResponse> shop;
		public DataModel<Foo,Foo> actions;
		public DataModel<Foo,Foo> gifts;
		public DataModel<Foo,Foo> achievements;
		public DataModel<DomainObjects.Leaderboard,WebObjects.Leaderboards.ListResponse> leaderboards;
		public DataModel<Foo,Foo> ranking;
		public DataModel<DomainObjects.Friend,WebObjects.Friends.ListResponse> friends;
		public DataModel<Foo,Foo> appStore;
		public ItemCache cache;
	}

}
