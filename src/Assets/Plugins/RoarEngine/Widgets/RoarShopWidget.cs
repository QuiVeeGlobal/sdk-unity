using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar;

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
	
	void OnRoarFetchShopComplete(Roar.CallbackInfo info)
	{
		whenLastFetched = Time.realtimeSinceStartup;
		isFetching = false;
		
		ArrayList list = shop.List();
		int cnt = 0;
		foreach (Hashtable item in list)
		{
			//Debug.Log("======================================");
			//foreach (DictionaryEntry kv in item)
			//	Debug.Log(string.Format("{0} => {1}", kv.Key, kv.Value));
			
			// base info
			ShopItem shopItem = new ShopItem();
			shopItem.key = item["shop_ikey"] as string;
			shopItem.label = item["label"] as string;
			shopItem.description = item["description"] as string;
			
			RoarTypesCache.AddShopItem(shopItem);
			cnt++;
			
			// item costs
			foreach (Hashtable costs in item["costs"] as ArrayList)
			{
				//foreach (DictionaryEntry kv in costs)
				//	Debug.Log(string.Format("{0} => {1}", kv.Key, kv.Value));
				ItemCost cost = new ItemCost();
				cost.key = costs["ikey"] as string;
				cost.type = costs["type"] as string;
				cost.amount = System.Convert.ToInt32(costs["value"] as string);
				cost.ok = System.Convert.ToBoolean(costs["bool"] as string);
				shopItem.costs.Add(cost);
			}
			
			// item modifiers
			foreach (Hashtable modifiers in item["modifiers"] as ArrayList)
			{
				//Debug.Log("=========== MODIFIERS ===========================");
				//foreach (DictionaryEntry kv in modifiers)
				//	Debug.Log(string.Format("{0} => {1}", kv.Key, kv.Value));
				
				ItemModifier modifier = new ItemModifier();
				modifier.key = modifiers["ikey"] as string;
				modifier.name = modifiers["name"] as string;
				if (modifiers.ContainsKey("type"))
					modifier.type = modifiers["type"] as string;
				else
					modifier.type = "item";
				if (modifiers.ContainsKey("value"))
					modifier.amount = System.Convert.ToInt32(modifiers["value"] as string);
				else
					modifier.amount = 1;
				shopItem.modifiers.Add(modifier);
			}
		}
		
		ScrollViewContentWidth = shopItemBounds.width;
		ScrollViewContentHeight = cnt * (shopItemBounds.height + shopItemSpacing);
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
			foreach (ShopItem item in RoarTypesCache.ShopItems.Values)
			{
				GUI.Label(itemRect, item.label, shopItemLabelStyle);
				GUI.Label(itemRect, item.description, shopItemDescriptionStyle);
				GUI.Label(itemRect, string.Format("{0} {1}", item.costs[0].amount.ToString(), RoarTypesCache.UserStatByKey(item.costs[0].key).Title), shopItemCostStyle);
				GUI.BeginGroup(itemRect);
				if (GUI.Button(buyButtonBounds, "Buy", shopItemBuyButtonStyle))
				{
					if (Debug.isDebugBuild)
					{
						Debug.Log(string.Format("buy request: {0} for {1} {2}", item.key, item.costs[0].amount, item.costs[0].key));
					}
					if (OnItemBuyRequest != null)
					{
						OnItemBuyRequest(item.key, item.costs[0].key);
					}
				}
				GUI.EndGroup();
				
				itemRect.y += itemRect.height + shopItemSpacing;
			}
		}
	}
}
