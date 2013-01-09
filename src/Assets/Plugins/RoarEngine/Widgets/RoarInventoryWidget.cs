using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoarInventoryWidget : RoarUIWidget
{
	protected Roar.Components.IInventory inventory;
	protected bool isFetching=false;
	
	protected override void OnEnable ()
	{
		if (IsLoggedIn)
		{
			inventory = DefaultRoar.Instance.Inventory;
		}
		
		Fetch();
	}
	
	protected override void DrawGUI(int windowId)
	{
		if (inventory == null || !IsLoggedIn) return;
		
		if (isFetching)
		{
			GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Fetching inventory data...", "StatusNormal");
			return;
		}
		
		if( ! inventory.HasDataFromServer )
		{
			GUI.Label (new Rect(0,0,ContentWidth,ContentHeight), "Inventory data not loaded!", "StatusNormal");
			return;
		}
		
		IList<Roar.DomainObjects.InventoryItem> items = inventory.List();

		//TODO: Fixup the hardcoded dimensions here!
		Rect rect = new Rect(0,0,ContentWidth, 32);
		GUI.Label ( rect, string.Format("Contains {0} items", items.Count) );
		rect.y += rowHeight;
		foreach( Roar.DomainObjects.InventoryItem item in items )
		{
			GUI.Label ( rect, item.label);
			rect.y += rowHeight;
		}
	}
	
	public void Fetch()
	{
		isFetching = true;
		inventory.Fetch(OnRoarFetchInventoryComplete);
	}
	
	void OnRoarFetchInventoryComplete( Roar.CallbackInfo< IDictionary<string,Roar.DomainObjects.InventoryItem> > data )
	{
		isFetching = false;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
