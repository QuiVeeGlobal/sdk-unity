using System.Collections;
using Roar.Components;
using UnityEngine;

namespace Roar.implementation.Components
{
	public class Inventory : IInventory
	{
		protected DataStore dataStore;
		protected IWebAPI.IItemsActions itemActions;
		protected ILogger logger;

		public Inventory (IWebAPI.IItemsActions itemActions, DataStore dataStore, ILogger logger)
		{
			this.itemActions = itemActions;
			this.dataStore = dataStore;
			this.logger = logger;
			RoarManager.roarServerItemAddEvent += this.OnServerItemAdd;
		}

		public bool HasDataFromServer { get { return  dataStore.inventory.HasDataFromServer; } }

		public void Fetch (Roar.RequestCallback callback)
		{
			dataStore.inventory.Fetch (callback);
		}

		public ArrayList List ()
		{
			return List (null);
		}

		public ArrayList List (Roar.Callback<ArrayList> callback)
		{
			if (callback != null)
				callback (new Roar.CallbackInfo<ArrayList> (dataStore.inventory.List ()));
			return dataStore.inventory.List ();
		}

		public void Activate (string id, Roar.RequestCallback callback)
		{
			var item = dataStore.inventory.Get (id);
			if (item == null) {
				logger.DebugLog ("[roar] -- Failed: no record with id: " + id);
				return;
			}

			Hashtable args = new Hashtable ();
			args ["item_id"] = id;
			itemActions.equip (args, new ActivateCallback (callback, this, id));
		}
		class ActivateCallback : SimpleRequestCallback
		{
			Inventory inventory;
			string id;

			public ActivateCallback (Roar.RequestCallback in_cb, Inventory in_inventory, string in_id) : base(in_cb)
			{
				inventory = in_inventory;
				id = in_id;
			}

			public override void OnSuccess (RequestResult info)
			{
				var item = inventory.dataStore.inventory.Get (id);
				item ["equipped"] = true;
				Hashtable returnObj = new Hashtable ();
				returnObj ["id"] = id;
				returnObj ["ikey"] = item ["ikey"];
				returnObj ["label"] = item ["label"];

				RoarManager.OnGoodActivated (new RoarManager.GoodInfo (id, item ["ikey"] as string, item ["label"] as string));
			}
		}

		public void Deactivate (string id, Roar.RequestCallback callback)
		{
			var item = dataStore.inventory.Get (id as string);
			if (item == null) {
				logger.DebugLog ("[roar] -- Failed: no record with id: " + id);
				return;
			}

			Hashtable args = new Hashtable ();
			args ["item_id"] = id;

			itemActions.unequip (args, new DeactivateCallback (callback, this, id));
		}
		class DeactivateCallback : SimpleRequestCallback
		{
			Inventory inventory;
			string id;

			public DeactivateCallback (Roar.RequestCallback in_cb, Inventory in_inventory, string in_id) : base(in_cb)
			{
				inventory = in_inventory;
				id = in_id;
			}

			public override void OnSuccess (RequestResult info)
			{
				var item = inventory.dataStore.inventory.Get (id);
				item ["equipped"] = false;
				Hashtable returnObj = new Hashtable ();
				returnObj ["id"] = id;
				returnObj ["ikey"] = item ["ikey"];
				returnObj ["label"] = item ["label"];

				RoarManager.OnGoodDeactivated (new RoarManager.GoodInfo (id, item ["ikey"] as string, item ["label"] as string));
			}
		}

		// `has( key, num )` boolean checks whether user has object `key`
		// and optionally checks for a `num` number of `keys` *(default 1)*
		public bool Has (string ikey, int num=1)
		{
			return dataStore.inventory.Has (ikey, num);
		}


		// `quantity( key )` returns the number of `key` objects held by user
		public int Quantity (string ikey)
		{
			return  dataStore.inventory.Quantity (ikey);
		}


		// `sell(id)` performs a sell on the item `id` specified
		public void Sell (string id, Roar.RequestCallback callback)
		{

			var item = dataStore.inventory.Get (id as string);
			if (item == null) {
				logger.DebugLog ("[roar] -- Failed: no record with id: " + id);
				return;
			}

			// Ensure item is sellable first
			if ((bool)item ["sellable"] != true) {
				var error = item ["ikey"] + ": Good is not sellable";
				logger.DebugLog ("[roar] -- " + error);
				if (callback != null)
					callback (new Roar.RequestResult (null, IWebAPI.DISALLOWED, error));
				return;
			}

			Hashtable args = new Hashtable ();
			args ["item_id"] = id;

			itemActions.sell (args, new SellCallback (callback, this, id));
		}

		class SellCallback : SimpleRequestCallback
		{
			Inventory inventory;
			string id;

			public SellCallback (Roar.RequestCallback in_cb, Inventory in_inventory, string in_id) : base(in_cb)
			{
				inventory = in_inventory;
				id = in_id;
			}

			public override void OnSuccess (RequestResult info)
			{
				var item = inventory.dataStore.inventory.Get (id);
				Hashtable returnObj = new Hashtable ();
				returnObj ["id"] = id;
				returnObj ["ikey"] = item ["ikey"];
				returnObj ["label"] = item ["label"];

				inventory.dataStore.inventory.Unset (id);

				RoarManager.OnGoodSold (new RoarManager.GoodInfo (id, item ["ikey"] as string, item ["label"] as string));
			}
		}

		// `use(id)` consumes/uses the item `id`
		public void Use (string id, Roar.RequestCallback callback)
		{

			var item = dataStore.inventory.Get (id as string);

			if (item == null) {
				logger.DebugLog ("[roar] -- Failed: no record with id: " + id);
				return;
			}

			// GH#152: Ensure item is consumable first
			logger.DebugLog (Roar.Json.ObjectToJSON (item));

			if ((bool)item ["consumable"] != true) {
				var error = item ["ikey"] + ": Good is not consumable";
				logger.DebugLog ("[roar] -- " + error);
				if (callback != null)
					callback (new Roar.RequestResult (null, IWebAPI.DISALLOWED, error));
				return;
			}


			Hashtable args = new Hashtable ();
			args ["item_id"] = id;

			itemActions.use (args, new UseCallback (callback, this, id));
		}

		class UseCallback : SimpleRequestCallback
		{
			Inventory inventory;
			string id;

			public UseCallback (Roar.RequestCallback in_cb, Inventory in_inventory, string in_id) : base(in_cb)
			{
				inventory = in_inventory;
				id = in_id;
			}

			public override void OnSuccess (RequestResult info)
			{
				var item = inventory.dataStore.inventory.Get (id);
				Hashtable returnObj = new Hashtable ();
				returnObj ["id"] = id;
				returnObj ["ikey"] = item ["ikey"];
				returnObj ["label"] = item ["label"];

				inventory.dataStore.inventory.Unset (id);

				RoarManager.OnGoodUsed (new RoarManager.GoodInfo (id, item ["ikey"] as string, item ["label"] as string));
			}
		}

		// `remove(id)` for now is simply an *alias* to sell
		public void Remove (string id, Roar.RequestCallback callback)
		{
			Sell (id, callback);
		}

		// Returns raw data object for inventory
		public Hashtable GetGood (string id)
		{
			return dataStore.inventory.Get (id);
		}

		protected void OnServerItemAdd (IXMLNode d)
		{
			// Only add to inventory if it Has previously been intialised
			if (HasDataFromServer) {
				var keysToAdd = new ArrayList ();
				var id = d.GetAttribute ("item_id");
				var ikey = d.GetAttribute ("item_ikey");

				keysToAdd.Add (ikey);

				if (!dataStore.cache.Has (ikey)) {
					dataStore.cache.AddToCache (keysToAdd, h => AddToInventory (ikey, id));
				} else
					AddToInventory (ikey, id);
			}
		}

		protected void AddToInventory (string ikey, string id)
		{
			// Prepare the item to manually add to Inventory
			Hashtable item = new Hashtable ();
			item [id] = DataModel.Clone (dataStore.cache.Get (ikey));

			// Also set the internal reference id (used by templates)
			var idspec = item [id] as Hashtable;
			idspec ["id"] = id;

			// Manually add to inventory
			dataStore.inventory.Set (item);
		}

	}

}

