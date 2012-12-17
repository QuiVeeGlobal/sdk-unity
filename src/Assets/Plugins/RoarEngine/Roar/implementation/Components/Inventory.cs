using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Roar.Components;
using UnityEngine;

namespace Roar.implementation.Components
{
	public class Inventory : IInventory
	{
		protected IDataStore dataStore;
		protected IWebAPI.IItemsActions itemActions;
		protected ILogger logger;

		public Inventory (IWebAPI.IItemsActions itemActions, IDataStore dataStore, ILogger logger)
		{
			this.itemActions = itemActions;
			this.dataStore = dataStore;
			this.logger = logger;
			RoarManager.roarServerItemAddEvent += this.OnServerItemAdd;
		}

		public bool HasDataFromServer { get { return  dataStore.inventory.HasDataFromServer; } }

		public void Fetch (Roar.Callback< IDictionary<string,Roar.DomainObjects.InventoryItem> > callback)
		{
			dataStore.inventory.Fetch (callback);
		}

		public IList<DomainObjects.InventoryItem> List ()
		{
			return dataStore.inventory.List ();
		}

		public void Activate (string id, Roar.Callback<Roar.WebObjects.Items.EquipResponse> callback)
		{
			var item = dataStore.inventory.Get (id);
			if (item == null) {
				logger.DebugLog ("[roar] -- Failed: no record with id: " + id);
				return;
			}


			Roar.WebObjects.Items.EquipArguments args = new Roar.WebObjects.Items.EquipArguments();
			args.item_id = id;
			
			itemActions.equip (args, new ActivateCallback (callback, this, id));
		}
		

		
		class ActivateCallback : CBBase<Roar.WebObjects.Items.EquipResponse>
		{
			Inventory inventory;
			string id;

			public ActivateCallback (Callback<Roar.WebObjects.Items.EquipResponse> in_cb, Inventory in_inventory, string in_id) : base(in_cb)
			{
				inventory = in_inventory;
				id = in_id;
			}

			public override void HandleSuccess ( CallbackInfo<WebObjects.Items.EquipResponse> info)
			{
				DomainObjects.InventoryItem item = inventory.dataStore.inventory.Get (id);
				item.equipped = true;
				
				//TODO: Fixup this return value
				Hashtable returnObj = new Hashtable ();
				returnObj ["id"] = item.id;
				returnObj ["ikey"] = item.ikey;
				returnObj ["label"] = item.label;

				RoarManager.OnGoodActivated (new RoarManager.GoodInfo (id, item.ikey, item.label));
			}
		}

		public void Deactivate (string id, Roar.Callback<Roar.WebObjects.Items.UnequipResponse> callback)
		{
			var item = dataStore.inventory.Get (id as string);
			if (item == null) {
				logger.DebugLog ("[roar] -- Failed: no record with id: " + id);
				return;
			}

			WebObjects.Items.UnequipArguments args = new Roar.WebObjects.Items.UnequipArguments();
			args.item_id = id;

			itemActions.unequip (args, new DeactivateCallback (callback, this, id));
		}
		class DeactivateCallback : CBBase<Roar.WebObjects.Items.UnequipResponse>
		{
			Inventory inventory;
			string id;


			public DeactivateCallback (Callback<Roar.WebObjects.Items.UnequipResponse> in_cb, Inventory in_inventory, string in_id) : base( in_cb )
			{
				inventory = in_inventory;
				id = in_id;
			}

			public override void HandleSuccess ( CallbackInfo<Roar.WebObjects.Items.UnequipResponse> info)
			{
				DomainObjects.InventoryItem item = inventory.dataStore.inventory.Get (id);
				item.equipped = false;
				
				//TODO: Fix this up
				Hashtable returnObj = new Hashtable ();
				returnObj ["id"] = item.id;
				returnObj ["ikey"] = item.ikey;
				returnObj ["label"] = item.label;

				RoarManager.OnGoodDeactivated (new RoarManager.GoodInfo (item.id, item.ikey, item.label));
			}
		}

		// `has( key, num )` boolean checks whether user has object `key`
		// and optionally checks for a `num` number of `keys` *(default 1)*
		public bool Has (string ikey, int num=1)
		{
			return dataStore.inventory.List().Where( v => (v.ikey == ikey) ).Count() >= num;
		}


		// `quantity( key )` returns the number of `key` objects held by user
		public int Quantity (string ikey)
		{
			return dataStore.inventory.List().Where( v => (v.ikey == ikey) ).Count();
		}


		// `sell(id)` performs a sell on the item `id` specified
		public void Sell (string id, Roar.Callback<Roar.WebObjects.Items.SellResponse> callback)
		{

			DomainObjects.InventoryItem item = dataStore.inventory.Get(id);
			if (item == null) {
				logger.DebugLog ("[roar] -- Failed: no record with id: " + id);
				return;
			}

			// Ensure item is sellable first
			if ( !item.sellable ) {
				var error = item.ikey + ": Good is not sellable";
				logger.DebugLog ("[roar] -- " + error);
				if (callback != null)
					callback (new Roar.CallbackInfo<Roar.WebObjects.Items.SellResponse> (null, IWebAPI.DISALLOWED, error));
				return;
			}

			WebObjects.Items.SellArguments args = new Roar.WebObjects.Items.SellArguments();
			args.item_id = id;

			itemActions.sell (args, new SellCallback (callback, this, id));
		}

		class SellCallback : CBBase<Roar.WebObjects.Items.SellResponse>
		{
			Inventory inventory;
			string id;

			public SellCallback (Roar.Callback<Roar.WebObjects.Items.SellResponse> in_cb, Inventory in_inventory, string in_id) : base(in_cb)
			{
				inventory = in_inventory;
				id = in_id;
			}

			public override void HandleSuccess (Roar.CallbackInfo<Roar.WebObjects.Items.SellResponse> info)
			{
				DomainObjects.InventoryItem item = inventory.dataStore.inventory.Get (id);
				
				//TODO: Fix this up
				Hashtable returnObj = new Hashtable ();
				returnObj ["id"] = id;
				returnObj ["ikey"] = item.ikey;
				returnObj ["label"] = item.label;

				inventory.dataStore.inventory.Unset(item.id);

				RoarManager.OnGoodSold (new RoarManager.GoodInfo (item.id, item.ikey, item.label));
			}
		}

		// `use(id)` consumes/uses the item `id`
		public void Use (string id, Roar.Callback<Roar.WebObjects.Items.UseResponse> callback)
		{

			var item = dataStore.inventory.Get (id as string);

			if (item == null) {
				logger.DebugLog ("[roar] -- Failed: no record with id: " + id);
				return;
			}

			// GH#152: Ensure item is consumable first
			logger.DebugLog (Roar.Json.ObjectToJSON (item));

			if (!item.consumable) {
				var error = item.ikey + ": Good is not consumable";
				logger.DebugLog ("[roar] -- " + error);
				if (callback != null)
					callback (new Roar.CallbackInfo<Roar.WebObjects.Items.UseResponse> (null, IWebAPI.DISALLOWED, error));
				return;
			}

			WebObjects.Items.UseArguments args = new Roar.WebObjects.Items.UseArguments();
			args.item_id = id;
			
			itemActions.use (args, new UseCallback (callback, this, id));
		}

		class UseCallback : CBBase<Roar.WebObjects.Items.UseResponse>
		{
			Inventory inventory;
			string id;

			public UseCallback (Roar.Callback<Roar.WebObjects.Items.UseResponse> in_cb, Inventory in_inventory, string in_id) : base(in_cb)
			{
				inventory = in_inventory;
				id = in_id;
			}

			public override void HandleSuccess (Roar.CallbackInfo<Roar.WebObjects.Items.UseResponse> info)
			{
				DomainObjects.InventoryItem item = inventory.dataStore.inventory.Get (id);
				
				//TODO: Make this work
				Hashtable returnObj = new Hashtable ();
				returnObj ["id"] = item.id;
				returnObj ["ikey"] = item.ikey;
				returnObj ["label"] = item.label;

				inventory.dataStore.inventory.Unset (item.id);

				RoarManager.OnGoodUsed (new RoarManager.GoodInfo (item.id, item.ikey, item.label));
			}
		}

		// `remove(id)` for now is simply an *alias* to sell
		public void Remove (string id, Roar.Callback<Roar.WebObjects.Items.SellResponse> callback)
		{
			Sell (id, callback);
		}

		// Returns raw data object for inventory
		public DomainObjects.InventoryItem GetGood (string id)
		{
			return dataStore.inventory.Get (id);
		}

		protected void OnServerItemAdd (IXMLNode d)
		{
			// Only add to inventory if it Has previously been intialised
			if (HasDataFromServer) {
			
				//TODO: Implement this!
				
				DomainObjects.InventoryItem item = new DomainObjects.InventoryItem();
				item.id = d.GetAttribute ("item_id");
				item.ikey = d.GetAttribute ("item_ikey");
				

				if ( ! dataStore.cache.List ().Any( i => (i.ikey == item.ikey) ) )
				{
					dataStore.cache.AddToCache (new List<string> { item.ikey }, h => AddToInventory (item.ikey, item.id));
				}
				else
				{
					AddToInventory (item.ikey, item.id);
				}
			}
		}

		protected void AddToInventory (string ikey, string id)
		{
			// Prepare the item to manually add to Inventory
			DomainObjects.InventoryItem item = new DomainObjects.InventoryItem();
			dataStore.inventory.AddOrUpdate(id,item);
		}

	}

}

