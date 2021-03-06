using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roar.DomainObjects;

public class RoarTasksWidget : RoarUIWidget
{
	
	protected Roar.Components.ITasks tasks;
	protected bool isFetching=false;
	public string labelFormat = "TaskLabel";
	public string descriptionFormat = "TaskDescription";
	public string locationFormat = "TaskLocation";
	public string detailFormat = "TaskDetail";
	
	public bool showDescription = true;
	public bool showType = true;
	public bool allowSorting = true;
	
	public int maxLabelWidth = 250;
	public int maxDescriptionFormatWidth = 350;
	public int maxLocationWidth = 100;
	public int rowHeight = 32;
	
	bool hasSelectedTask = false;
	Roar.DomainObjects.Task selectedTask = null;
	
	public class CRMToString : CRMVisitor<string>
	{
		public override string OnCostNull ()
		{
			return "No Costs!";
		}
		public override string OnCostStat (Roar.DomainObjects.Costs.Stat stat)
		{
			return " Stat : " + stat.ikey + " : " + stat.value;
		}
		
		public override string OnCostItem (Roar.DomainObjects.Costs.Item item)
		{
			return " Item: "+item.ikey+" Num:"+item.number_required;
		}
		
		public override string OnCostMultiple( Roar.DomainObjects.Costs.Multiple multi )
		{
			string subcosts = "";
			foreach( Cost c in multi.costs )
			{
				subcosts += this.visit_cost(c) + "\n";
			}
			return "Multiple" + subcosts;
		}
		
		public override string OnModifierNull ()
		{
			return "No modifiers";
		}
	
		public override string OnModifierGrantItem (Roar.DomainObjects.Modifiers.GrantItem mod)
		{
			return "Grants Item: "+mod.ikey;
		}
		
		public override string OnModifierGrantStat (Roar.DomainObjects.Modifiers.GrantStat mod)
		{
			return "Grants Stat: "+ mod.ikey+" Type: "+mod.type +" Amount: "+mod.value;
		}
		
		public override string OnModifierGrantStatRange (Roar.DomainObjects.Modifiers.GrantStatRange mod)
		{
			return "Grant Stat Range: " + mod.ikey+" Min-Max: "+mod.min+"-"+mod.max+" Type: "+ mod.type;
		}
		
		public override string OnModifierGrantXp (Roar.DomainObjects.Modifiers.GrantXp mod)
		{
			return "Grants Xp: "+mod.value;
		}
		
		public override string OnModifierGrantXpRange (Roar.DomainObjects.Modifiers.GrantXpRange mod)
		{
			return "Grants Xp Range: Min-Max"+ mod.min+"-"+mod.max;
		}
		
		public override string OnModifierIfThenElse (  Roar.DomainObjects.Modifiers.IfThenElse if_then_else )
		{
			string ifString = "", thenString="", elseString="";
			
			foreach( Requirement r in if_then_else.if_ )
			{
				ifString += visit_requirement(r);
			}
			
			foreach( Modifier m in if_then_else.then_ )
			{
				thenString += visit_modifier(m);
			}
			
			foreach( Modifier m in if_then_else.else_ )
			{
				elseString += visit_modifier(m);
			}
			
			
			return "if("+ ifString+") \n then (" + thenString+ ")\nelse "+elseString;
		}

		public override string OnModifierMultiple (Roar.DomainObjects.Modifiers.Multiple mod)
		{
			string subMods = "";
			foreach( Modifier m in mod.modifier )
			{
				subMods += this.visit_modifier(m) + "\n";
			}
			return "Multiple" + subMods; 
		}
		
		public override string OnModifierNamedReference (Roar.DomainObjects.Modifiers.NamedReference mod)
		{
			return "Named Ref: ??";
		}
		
		public override string OnModifierNothing (Roar.DomainObjects.Modifiers.Nothing mod)
		{
			return "Nothing";
		}
		
		public override string OnModifierRandomChoice (Roar.DomainObjects.Modifiers.RandomChoice mod)
		{
			
			string subs = "";
			foreach( Roar.DomainObjects.Modifiers.RandomChoice.ChoiceEntry ch in mod.choices )
			{
				foreach(Modifier m in ch.modifiers)
					subs += this.visit_modifier(m)+"\n";
				if(ch.requirements != null)
				{
					foreach(Requirement r in ch.requirements)
						subs += this.visit_requirement(r)+"\n";
				}
				subs += "Weight: "+ ch.weight+"\n";
			}
			return "Random Choice: " + subs;
			
		}
		
		public override string OnModifierRemoveItems (Roar.DomainObjects.Modifiers.RemoveItems mod)
		{
			return "Removes Items: "+ mod.ikey + " Count: "+mod.count;
		}
		
		public override string OnModifierScript (Roar.DomainObjects.Modifiers.Script mod)
		{
			return "Script: ??";
		}
		
		public override string OnRequirementNull ()
		{
			return "No Requirements!";
		}
		public override string OnRequirementFalse (Roar.DomainObjects.Requirements.False req)
		{
			return "False: ??";
		}
		
		public override string OnRequirementFriends (Roar.DomainObjects.Requirements.Friends req)
		{
			return "Friends Required: "+req.required;
		}
		
		public override string OnRequirementItem (Roar.DomainObjects.Requirements.Item req)
		{
			return "Item Required: "+req.ikey + " Count: "+req.number_required;
		}
		
		public override string OnRequirementLevel (Roar.DomainObjects.Requirements.Level req)
		{
			return "Lvl required: "+req.level;
		}

		public override string OnRequirementMultiple (Roar.DomainObjects.Requirements.Multiple req)
		{
			string subReq = "";
			foreach( Requirement r in req.requirements )
			{
				subReq += this.visit_requirement(r) + "\n";
			}
			return "Multiple: " + subReq; 
		}
		
		public override string OnRequirementStat (Roar.DomainObjects.Requirements.Stat req)
		{
			return "Stats reqd: "+req.ikey + " Count: "+req.value;
		}
		
		public override string OnRequirementTrue (Roar.DomainObjects.Requirements.True req)
		{
			return "True??";
		}
	}
	
	
	protected override void OnEnable ()
	{
		hasSelectedTask = false;
		if (!IsLoggedIn)
		{
			Debug.Log ("RoarTasksWidget enabled before login - unable to fetch data");
			enabled = false;
			return;
		}
		
		tasks = DefaultRoar.Instance.Tasks;		
		Fetch();
	}
	
		
	protected override void DrawGUI(int windowId)
	{
		if (tasks == null || !IsLoggedIn) return;
		
		if (isFetching)
		{
			GUI.Label(new Rect(0,0,ContentWidth,ContentHeight), "Fetching tasks data...", "StatusNormal");
			return;
		}
		
		if( ! tasks.HasDataFromServer )
		{
			GUI.Label (new Rect(0,0,ContentWidth,ContentHeight), "Task data not loaded!", "StatusNormal");
			return;
		}
		
		if(!hasSelectedTask)
		{
	
			IList<Roar.DomainObjects.Task> items = tasks.List();
			
			Rect rect = new Rect(0,0,ContentWidth, 32);
			GUI.Label ( rect, string.Format("Contains {0} items", items.Count) );
			rect.y += rowHeight;
			
			Vector2 lastLabelSize;
			lastLabelSize = GUI.skin.FindStyle(labelFormat).CalcSize(new GUIContent( "Label"));
			if(maxLabelWidth == 0)
				rect.width = lastLabelSize.x;
			else
				rect.width = maxLabelWidth;
			GUI.Label ( rect, "Label", labelFormat);
			
			rect.x += rect.width + 5;
			
			lastLabelSize =GUI.skin.FindStyle(descriptionFormat).CalcSize(new GUIContent("Description"));
			if(maxDescriptionFormatWidth == 0)
				rect.width = lastLabelSize.x;
			else
				rect.width = maxDescriptionFormatWidth;
				
			GUI.Label ( rect, "Description", descriptionFormat);
			rect.x += rect.width+ 5;
			
			lastLabelSize =GUI.skin.FindStyle(locationFormat).CalcSize(new GUIContent("Location"));
			if(maxLocationWidth == 0)
				rect.width = lastLabelSize.x;
			else
				rect.width = maxLocationWidth;
				
			GUI.Label ( rect, "Location", locationFormat);
			rect.x += rect.width+ 5;
			rect.y += lastLabelSize.y;
			foreach( Roar.DomainObjects.Task item in items )
			{
				
				rect.x =0;
				
				lastLabelSize = GUI.skin.FindStyle(labelFormat).CalcSize(new GUIContent( item.label));
				if(maxLabelWidth == 0)
					rect.width = lastLabelSize.x;
				else
					rect.width = maxLabelWidth;
				GUI.Label ( rect, item.label, labelFormat);
				
				rect.x += rect.width + 5;
				
				lastLabelSize =GUI.skin.FindStyle(descriptionFormat).CalcSize(new GUIContent(item.description));
				if(maxDescriptionFormatWidth == 0)
					rect.width = lastLabelSize.x;
				else
					rect.width = maxDescriptionFormatWidth;
					
				GUI.Label ( rect, item.description, descriptionFormat);
				rect.x += rect.width+ 5;
				
				lastLabelSize =GUI.skin.FindStyle(locationFormat).CalcSize(new GUIContent(item.location));
				if(maxLocationWidth == 0)
					rect.width = lastLabelSize.x;
				else
					rect.width = maxLocationWidth;
					
				GUI.Label ( rect, item.location, locationFormat);
				rect.x += rect.width+ 5;
				
			
				if(GUI.Button(rect, "View"))
				{
					selectedTask = item;
					hasSelectedTask = true;
					
				}
				
				
				rect.x = 0;
				rect.y += rowHeight;
			}
		}
		else
		{
			Rect rect = new Rect(0,0,ContentWidth, 32);
			Vector2 lastLabelSize;
			
			lastLabelSize = GUI.skin.FindStyle(labelFormat).CalcSize(new GUIContent( selectedTask.label));
			rect.height = lastLabelSize.y;
			rect.width = lastLabelSize.x;
			
			GUI.Label ( rect, selectedTask.label, labelFormat);
			
			rect.y += lastLabelSize.y;
			
			lastLabelSize =GUI.skin.FindStyle(descriptionFormat).CalcSize(new GUIContent(selectedTask.description));
			rect.height = lastLabelSize.y;
			rect.width = lastLabelSize.x;
				
			GUI.Label ( rect, selectedTask.description, descriptionFormat);
			rect.y += lastLabelSize.y;
			
			lastLabelSize =GUI.skin.FindStyle(locationFormat).CalcSize(new GUIContent("Location: "+selectedTask.location));
			rect.height = lastLabelSize.y;
			rect.width = lastLabelSize.x;
			
			
			if(maxLocationWidth == 0)
				rect.width = lastLabelSize.x;
			else
				rect.width = maxLocationWidth;
				
			GUI.Label ( rect, selectedTask.location, locationFormat);
			rect.y += lastLabelSize.y;
			
			lastLabelSize =GUI.skin.FindStyle(detailFormat).CalcSize(new GUIContent("Mastery lvl: "+selectedTask.mastery_level));
			rect.height = lastLabelSize.y;
			rect.width = lastLabelSize.x;
			
			if(maxLocationWidth == 0)
				rect.width = lastLabelSize.x;
			else
				rect.width = maxLocationWidth;
				
			GUI.Label ( rect, "Mastery lvl: "+selectedTask.mastery_level, locationFormat);
			rect.y += lastLabelSize.y;
			
			CRMVisitor<string> visitorObject = new CRMToString();
			
			string costsString = "";
			foreach(Roar.DomainObjects.Cost cost in selectedTask.costs)
			{
				costsString += visitorObject.visit_cost(cost);
			}
			
			lastLabelSize =GUI.skin.FindStyle(detailFormat).CalcSize(new GUIContent("Costs: "+costsString));
			rect.height = lastLabelSize.y;
			rect.width = lastLabelSize.x;
			
			if(maxLocationWidth == 0)
				rect.width = lastLabelSize.x;
			else
				rect.width = maxLocationWidth;
				
			
			GUI.Label ( rect, "Costs: "+costsString, detailFormat);
			rect.y += lastLabelSize.y;
			
			string rewardsString = "";
			foreach(Roar.DomainObjects.Modifier modifier in selectedTask.rewards)
			{
				rewardsString += visitorObject.visit_modifier(modifier)+"\n";
			}
			
			
			lastLabelSize =GUI.skin.FindStyle(detailFormat).CalcSize(new GUIContent("Rewards: "+rewardsString));
			rect.height = lastLabelSize.y;
			rect.width = lastLabelSize.x;
			
			if(maxLocationWidth == 0)
				rect.width = lastLabelSize.x;
			else
				rect.width = maxLocationWidth;
				
			GUI.Label (rect, "Rewards: "+rewardsString, detailFormat);
			rect.y += lastLabelSize.y;
			
			string requirementsString = "";
			foreach(Roar.DomainObjects.Requirement requirement in selectedTask.requirements)
			{
				requirementsString += visitorObject.visit_requirement(requirement)+"\n";
			}
			
			
			lastLabelSize =GUI.skin.FindStyle(detailFormat).CalcSize(new GUIContent("Requirements: "+requirementsString));
			rect.height = lastLabelSize.y;
			rect.width = lastLabelSize.x;
			
			if(maxLocationWidth == 0)
				rect.width = lastLabelSize.x;
			else
				rect.width = maxLocationWidth;
				
			GUI.Label (rect, "Requirements: "+requirementsString, detailFormat);
			rect.y += lastLabelSize.y;
		
			if(GUI.Button(rect, "Back"))
			{
				selectedTask = null;
				hasSelectedTask = false;
				
			}
		
		}
	}
	
	public void Fetch()
	{
		isFetching = true;
		tasks.Fetch(OnRoarFetchTasksComplete);
	}
	
	void OnRoarFetchTasksComplete( Roar.CallbackInfo< IDictionary<string,Roar.DomainObjects.Task> > data )
	{
		isFetching = false;
	}

}
