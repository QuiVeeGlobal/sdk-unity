using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Roar
{
	public static class RoarTypesCache
	{
		#region User Stats / Properties
		private static Dictionary<string, UserStat> userStats = new Dictionary<string, UserStat>();
		public static Dictionary<string, UserStat> UserStats
		{
			get { return userStats; }
		}
		public static UserStat UserStatByKey(string key)
		{
			if (userStats.ContainsKey(key))
				return userStats[key];
			
			// parse properties and see if the stats can be added
			Roar.Components.IProperties properties = DefaultRoar.Instance.Properties;
			if (properties != null)
			{
				UserStat userStat = new UserStat();
				userStat.key = key;
				userStat.Value = properties.GetValue(key) as string;

				object statProperty = properties.GetProperty(key);
				if (statProperty is Hashtable)
				{
					Hashtable property = (Hashtable)statProperty;
					if (property.ContainsKey("label"))
					{
						userStat.label = property["label"] as string;
					}
				}
				
				userStats.Add(key, userStat);
				return userStat;
			}
			
			return null;
		}
		public static UserStat AddUserStat(UserStat userStat)
		{
			if (userStats.ContainsKey(userStat.key))
				userStats.Add(userStat.key, userStat);
			else
				userStats[userStat.key] = userStat;
			return userStat;
		}
		#endregion
		
		#region Shop
		private static Dictionary<string, ShopItem> shopItems = new Dictionary<string, ShopItem>();
		public static Dictionary<string, ShopItem> ShopItems
		{
			get { return shopItems; }
		}
		public static ShopItem ShopItemByKey(string key)
		{
			if (userStats.ContainsKey(key))
				return shopItems[key];
			return null;
		}
		public static ShopItem AddShopItem(ShopItem shopItem)
		{
			if (shopItems.ContainsKey(shopItem.key))
				shopItems.Add(shopItem.key, shopItem);
			else
				shopItems[shopItem.key] = shopItem;
			return shopItem;
		}
		#endregion
	}
}
