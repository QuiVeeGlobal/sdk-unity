using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar;
using Roar.DomainObjects;

public class RoarFacebookShopWidget : RoarUIWidget
{
	public delegate void RoarShopWidgetBuyHandler(Roar.DomainObjects.ShopEntry shop_entry);
	public static event RoarShopWidgetBuyHandler OnItemBuyRequest;
	
	public enum WhenToFetch { OnEnable, Once, Occassionally, Manual };
	public WhenToFetch whenToFetch = WhenToFetch.Occassionally;
	public float howOftenToFetch = 60;
	
	public string shopItemLabelStyle = "ShopItemLabel";
	public string shopItemDescriptionStyle = "ShopItemDescription";
	public string shopItemCostStyle = "ShopItemCost";
	public string shopItemBuyButtonStyle = "ShopItemBuyButton";
	public Rect shopItemBounds = new Rect(0, 0, 450, 90);
	public Rect buyButtonBounds= new Rect(200, 25, 100, 40);
	public float shopItemSpacing = 12;
	
	private bool isFetching;
	private bool isBuying = false;
	private float whenLastFetched;
	private Roar.Components.IFacebook facebook;
	private IList<FacebookShopEntry> shopEntries;
	
	private IList<string> errorMessages = new List<string>();
	
	protected override void OnEnable ()
	{
		if (IsLoggedIn)
		{
			base.OnEnable();
			facebook = DefaultRoar.Instance.Facebook;
			if (facebook != null)
			{
				if (whenToFetch == WhenToFetch.OnEnable 
				|| (whenToFetch == WhenToFetch.Once && !facebook.IsLoggedInViaFacebook())
				|| (whenToFetch == WhenToFetch.Occassionally && (whenLastFetched == 0 || Time.realtimeSinceStartup - whenLastFetched >= howOftenToFetch))
				)
				{
					Fetch();
				}
			}
			else if (Debug.isDebugBuild)
			{
				Debug.LogWarning("Cant load shop data");
			}
		}
		else
		{
			enabled = false;
		}
	}

	public void Fetch()
	{
		Debug.Log("fetching facebook shop data");
		isFetching = true;
		
		facebook.FetchShopData(OnRoarFetchShopComplete);
	}
	
	void CalculateScrollBounds()
	{
		ScrollViewContentWidth = shopItemBounds.width;
		ScrollViewContentHeight = Mathf.Max(contentBounds.height, (shopEntries.Count + errorMessages.Count) * (shopItemBounds.height + shopItemSpacing));
	}
	
	void OnRoarFetchShopComplete(Roar.CallbackInfo< IDictionary<string,Roar.DomainObjects.FacebookShopEntry> > info)
	{
		whenLastFetched = Time.realtimeSinceStartup;
		isFetching = false;
		shopEntries = facebook.List();
		errorMessages = new List<string>();
		Debug.Log("faceboook count is "+facebook.List().Count);
		
		if( info.code!=IWebAPI.OK)
		{
			Debug.Log ( string.Format("Error loading shop:{0}:{1}", info.code, info.msg));
			FlashError( info.msg ); //This updates the scroll bounds.
			return;
		}

		CalculateScrollBounds();
	}
	
	protected override void DrawGUI(int windowId)
	{
		if (facebook == null || !IsLoggedIn) return;
		if (isFetching)
		{
			GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Fetching shop data...", "StatusNormal");
		}
		else
		{
			//TODO: Dont use same rect for errors and items
			Rect itemRect = shopItemBounds;

			foreach( string e in errorMessages )
			{
				GUI.Label ( itemRect, e );
				itemRect.y += itemRect.height + shopItemSpacing;
			}
			
			foreach (FacebookShopEntry item in shopEntries)
			{
				GUI.Label(itemRect, item.label, shopItemLabelStyle);
				GUI.Label(itemRect, item.description, shopItemDescriptionStyle);
				GUI.Label (itemRect, string.Format ("Costs {0} ", item.price), shopItemCostStyle ) ;
		
				GUI.BeginGroup(itemRect);
				
				//For now only check the costs
				
				if (GUI.Button(buyButtonBounds, "Buy", shopItemBuyButtonStyle))
				{
					Debug.Log("buying");
					Application.ExternalCall("buySomething");
				}
				GUI.enabled = true;
				GUI.EndGroup();
				
				itemRect.y += itemRect.height + shopItemSpacing;
			}
		}
	}
	
	protected void OnBuyComplete( CallbackInfo<Roar.WebObjects.Shop.BuyResponse> response )
	{
		isBuying = false;
		if( response.code!=WebAPI.OK )
		{
			FlashError( response.msg );
		}
	}
	
	protected void FlashError( string mesg )
	{
		errorMessages.Add( mesg );
		CalculateScrollBounds();
	}
}
