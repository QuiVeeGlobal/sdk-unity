using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar;
using Roar.DomainObjects;

public class RoarShopWidget : RoarUIWidget
{
	public delegate void RoarShopWidgetBuyHandler(string itemShopKey, string itemCostKey);
	public static event RoarShopWidgetBuyHandler OnItemBuyRequest;
	
	public enum WhenToFetch { OnEnable, Once, Occassionally, Manual };
	public WhenToFetch whenToFetch = WhenToFetch.Occassionally;
	public float howOftenToFetch = 60;
	
	public string shopItemLabelStyle = "ShopItemLabel";
	public string shopItemDescriptionStyle = "ShopItemDescription";
	public string shopItemCostStyle = "ShopItemCost";
	public string shopItemBuyButtonStyle = "ShopItemBuyButton";
	public Rect shopItemBounds;
	public Rect buyButtonBounds;
	public float shopItemSpacing;
	
	private bool isFetching;
	private float whenLastFetched;
	private Roar.Components.IShop shop;
	private IList<ShopEntry> shopEntries;
	
	protected override void OnEnable ()
	{
		if (IsLoggedIn)
		{
			base.OnEnable();
			shop = DefaultRoar.Instance.Shop;
			if (shop != null)
			{
				if (whenToFetch == WhenToFetch.OnEnable 
				|| (whenToFetch == WhenToFetch.Once && !shop.HasDataFromServer)
				|| (whenToFetch == WhenToFetch.Occassionally && (whenLastFetched == 0 || Time.realtimeSinceStartup - whenLastFetched >= howOftenToFetch))
				)
				{
					Fetch();
				}
			}
			else if (Debug.isDebugBuild)
			{
				Debug.LogWarning("Shop data is null; unable to render shop widget");
			}
		}
		else
		{
			enabled = false;
		}
	}

	public void Fetch()
	{
		isFetching = true;
		shop.Fetch(OnRoarFetchShopComplete);
	}
	
	void OnRoarFetchShopComplete(Roar.CallbackInfo< IDictionary<string,Roar.DomainObjects.ShopEntry> > info)
	{
		whenLastFetched = Time.realtimeSinceStartup;
		isFetching = false;
		shopEntries = shop.List();
		
		if( info.code!=IWebAPI.OK)
		{
			Debug.Log ( string.Format("Error loading shop:{0}:{1}", info.code, info.msg) );
			return;
		}
		
		int cnt = 0;
		
		Debug.Log("======================================");
		foreach ( KeyValuePair<string,Roar.DomainObjects.ShopEntry> item in info.data)
		{
			Debug.Log( string.Format("{0}:{1}:{2}", cnt, item.Key, item.Value.label) );
			if( item.Value.costs.Count == 1)
			{
				Roar.DomainObjects.Costs.Stat stat_cost = item.Value.costs[0] as Roar.DomainObjects.Costs.Stat;
				if( stat_cost != null )
				{
					Debug.Log ( string.Format ("Costs {0} {1} ({2} {3})", stat_cost.value, stat_cost.ikey, stat_cost.ok, stat_cost.reason ) );
				}
			}
		}
		Debug.Log("======================================");
		
		ScrollViewContentWidth = shopItemBounds.width;
		ScrollViewContentHeight = shopEntries.Count * (shopItemBounds.height + shopItemSpacing);
	}
	
	protected override void DrawGUI(int windowId)
	{
		if (shop == null || !IsLoggedIn) return;
		if (isFetching)
		{
			GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Fetching shop data...", "StatusNormal");
		}
		else
		{			
			Rect itemRect = shopItemBounds;
			foreach (ShopEntry item in shopEntries)
			{
				GUI.Label(itemRect, item.label, shopItemLabelStyle);
				GUI.Label(itemRect, item.description, shopItemDescriptionStyle);
				//GUI.Label(itemRect, string.Format("{0} {1}", item.costs[0].amount.ToString(), RoarTypesCache.UserStatByKey(item.costs[0].key).Title), shopItemCostStyle);
				GUI.BeginGroup(itemRect);
				
				//For now only check the costs
				bool can_buy = true;
				foreach( Roar.DomainObjects.Cost cost in item.costs)
				{
					if( ! cost.ok ) { can_buy = false; break; }
				}
				GUI.enabled = can_buy;
				
				if (GUI.Button(buyButtonBounds, "Buy", shopItemBuyButtonStyle))
				{
					//if (Debug.isDebugBuild)
					//{
					//	Debug.Log(string.Format("buy request: {0} for {1} {2}", item.ikey, item.costs[0].amount, item.costs[0].key));
					//}
					//if (OnItemBuyRequest != null)
					//{
					//	OnItemBuyRequest(item.ikey, item.costs[0].ikey);
					//}
				}
				GUI.enabled = true;
				GUI.EndGroup();
				
				itemRect.y += itemRect.height + shopItemSpacing;
			}
		}
	}
}
