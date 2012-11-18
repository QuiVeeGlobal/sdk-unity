using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Roar
{
	[System.Serializable]
	public class UserStat
	{
		public string key;
		public string label;
		
		private string title;
		private string value;
		
		public string Value
		{
			get { return this.value; }
			set { this.value = value; }
		}
		
		public string Title
		{
			get
			{
				if (title == null) return label;
				return title;
			}
			set
			{
				title = value;
			}
		}
	}

	[System.Serializable]
	public class ShopItem
	{
		public string key = string.Empty;
		public string label = string.Empty;
		public string description = string.Empty;
		public List<ItemCost> costs = new List<ItemCost>(2);
		public List<ItemModifier> modifiers = new List<ItemModifier>(2);
	}
	[System.Serializable]
	public class ItemCost
	{
		public string type = string.Empty;
		public string key = string.Empty;
		public int amount;
		public bool ok;
	}
	[System.Serializable]
	public class ItemModifier
	{
		public string key = string.Empty;
		public string type = string.Empty;
		public string name = string.Empty;
		public int amount;
	}
	
}
