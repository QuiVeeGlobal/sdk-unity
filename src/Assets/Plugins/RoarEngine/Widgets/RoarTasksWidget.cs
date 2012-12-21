using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//TODO: Should this be called RoarActionsWidget?

public class RoarTasksWidget : RoarUIWidget
{
	
	protected Roar.Components.IActions actions;
	protected bool isFetching=false;
	
	protected override void OnEnable ()
	{
		if (!IsLoggedIn)
		{
			Debug.Log ("RoarTasksWidget enabled before login - unable to fetch data");
			return;
		}
		
		actions = DefaultRoar.Instance.Actions;		
		Fetch();
	}
	
		
	protected override void DrawGUI(int windowId)
	{
		if (actions == null || !IsLoggedIn) return;
		
		if (isFetching)
		{
			GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Fetching tasks data...", "StatusNormal");
			return;
		}
		
		if( ! actions.HasDataFromServer )
		{
			GUI.Label (new Rect(0,0,ContentWidth,ContentHeight), "Inventory data not loaded!", "StatusNormal");
			return;
		}
		

		IList<Roar.DomainObjects.Task> items = actions.List();

		//TODO: Fixup the hardcoded dimensions here!
		Rect rect = new Rect(0,0,ContentWidth, 32);
		GUI.Label ( rect, string.Format("Contains {0} items", items.Count) );
		rect.y += 32;

		foreach( Roar.DomainObjects.Task item in items )
		{
			GUI.Label ( rect, item.label + " : " + item.description);
			rect.y += 32;
		}
	}
	
	public void Fetch()
	{
		isFetching = true;
		actions.Fetch(OnRoarFetchActionsComplete);
	}
	
	void OnRoarFetchActionsComplete( Roar.CallbackInfo< IDictionary<string,Roar.DomainObjects.Task> > data )
	{
		isFetching = false;
	}

}
