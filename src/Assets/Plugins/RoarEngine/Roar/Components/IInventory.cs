using System.Collections;
using System.Collections.Generic;


namespace Roar.Components
{

/**
 * \brief IInventory is an interface for viewing and manipulating a user's inventory.
 *
 * One can perform the following functions on an inventory item:
 * - equip
 * - unequip
 * - sell
 * - use
 *
 * An inventory is composed of a list of inventory items.
 *
 * An inventory item contains properties to describe its use, along with specific
 * flags to indicate status such as equipped, consumable and collect tick/window.
 *
 *
 * An inventory item can be accessed through its Hashtable interface:
 *
 * \code
 *  - id                     [uint]
 *  - ikey                   [string]
 *  - label                  [string]
 *  - description            [string]
 *  - count                  [int]
 *  - stats                  [array]
 *     - ikey                [string]
 *     - value               [int]
 *  - custom_attributes      [array]
 *     - attribute_ikey      [string]
 *     - value               [int]
 *  - tags                   [array of strings]
 *  - type                   [string]
 *  - consumable             [bool]
 *  - equipped               [bool]
 *  - sellable               [bool]
 * \endcode
 *
 * @note The inventory view and manipulation functions of this interface can only be called after the inventory has been fetched from the server via a call to #fetch.
 * @note once #fetch has received and processed the inventory from the server, the #hasDataFromServer
 * property will return true and calls to this interface will be functional.
 **/
	public interface IInventory
	{
		/**
	   * Fetch inventory information from the server.
	   *
	   * On success:
	   * - invokes callback with parameter *Hastable data* containing the data for the inventory.
	   * - fires the RoarManager#inventoryReadyEvent
	   * - sets #hasDataFromServer to true
	   *
	   * On failure:
	   * - invokes callback with error code and error message
	   *
	   * @param callback the callback function to be passed this function's result.
	   *
	   * @returns nothing - use a callback and/or subscribe to RoarManager events for results of non-blocking calls.
	   */
		void Fetch(Roar.Callback< IDictionary<string,Roar.DomainObjects.InventoryItem> > callback);

		/**
	   * Check whether any inventory data has been obtained from the server.
	   *
	   * @returns true if #fetch has completed execution.
	   */
		bool HasDataFromServer { get; }

		/**
	   * Get a list of all the inventory items for the authenticated user.
	   *
	   * @param callback the callback function to be passed this function's result.
	   *
	   * @returns A list of Hashtables for each inventory item.
	   *
	   * @note This does _not_ make a server call. It requires the inventory to
	   *       have already been fetched via a call to #fetch. If this function
	   *       is called prior to the successful completion of a #fetch call,
	   *       it will return an empty array.
	   **/
		IList<DomainObjects.InventoryItem> List ();

		/**
	   * Equippes an item in the user's inventory.
	   *
	   * On success:
	   * - fires the RoarManager.goodEquippedEvent
	   * - invokes callback passing *data* parameter a Hashtable containing the:
	   *  - "id" : the id of the inventory item instance
	   *  - "ikey" : the id of the inventory item type
	   *  - "label" : the label of the inventory item
	   *
	   * On failure:
	   * - invokes callback with error code and error message
	   *
	   * @param id the key that uniquely identifies an inventory item.
	   * @param callback the callback function to be passed this function's result.
	   *
	   * @returns nothing - use a callback and/or subscribe to RoarManager events for results of non-blocking calls.
	   *
	   **/
		void Equip (string id, Roar.Callback<Roar.WebObjects.Items.EquipResponse> callback);

		/**
	   * Unequippes an item in the user's inventory.
	   *
	   * On success:
	   * - fires the RoarManager.goodUnequippedEvent
	   * - invokes callback passing *data* parameter a Hashtable containing the:
	   *  - "id" : the id of the inventory item instance
	   *  - "ikey" : the id of the inventory item type
	   *  - "label" : the label of the inventory item
	   *
	   * On failure:
	   * - invokes callback with error code and error message
	   *
	   * @param id the key that uniquely identifies an inventory item.
	   * @param callback the callback function to be passed this function's result.
	   *
	   * @returns nothing - use a callback and/or subscribe to RoarManager events for results of non-blocking calls.
	   *
	   **/
		void Unequip (string id, Roar.Callback<Roar.WebObjects.Items.UnequipResponse> callback);

		/**
		 * Checks if the user's inventory contains at least num of a given item.
		 *
		 * @param ikey the key that identifies an inventory item.
		 * @param num the required number of the item.
		 * @returns true if num or more instances of a given inventory item belong to the user.
		 *
		 * @note This does _not_ make a server call. It requires the inventory to
		 *       have already been fetched via a call to #fetch. If this function
		 *       is called prior to the successful completion of a #fetch call,
		 *       it will return false.
		 **/
		bool Has (string ikey, int num);


		/**
		 * Checks if the user's inventory contains at least one of a given item.
		 *
		 * @param ikey the key that identifies an inventory item.
		 * @returns true if one or more instances of a given inventory item belong to the user.
		 *
		 * @note This does _not_ make a server call. It requires the inventory to
		 *       have already been fetched via a call to #fetch. If this function
		 *       is called prior to the successful completion of a #fetch call,
		 *       it will return false.
		 **/
		bool Has (string ikey);


		/**
	   * @param ikey the key that identifies an inventory item.
	   * @returns the quantity of a given inventory item held by the user.
	   *
	   * @note This does _not_ make a server call. It requires the inventory to
	   *       have already been fetched via a call to #fetch. If this function
	   *       is called prior to the successful completion of a #fetch call,
	   *       it will return 0.
	   **/
		int Quantity (string ikey);

		/**
	   * Sells an item in the user's inventory.
	   *
	   * On success:
	   * - fires the RoarManager.goodSoldEvent
	   * - invokes callback passing *data* parameter a Hashtable containing the:
	   *  - "id" : the id of the inventory item instance
	   *  - "ikey" : the id of the inventory item type
	   *  - "label" : the label of the inventory item
	   *
	   * On failure:
	   * - invokes callback with error code and error message
	   *
	   * @param id the key that uniquely identifies an inventory item.
	   * @param callback the callback function to be passed this function's result.
	   *
	   * @returns nothing - use a callback and/or subscribe to RoarManager events for results of non-blocking calls.
	   *
	   **/
		void Sell (string id, Roar.Callback<Roar.WebObjects.Items.SellResponse> callback);

		/**
	   * Consumes/uses an item in the user's inventory.
	   *
	   * On success:
	   * - fires the RoarManager.goodUsedEvent
	   * - invokes callback passing *data* parameter a Hashtable containing the:
	   *  - "id" : the id of the inventory item instance
	   *  - "ikey" : the id of the inventory item type
	   *  - "label" : the label of the inventory item
	   *
	   * On failure:
	   * - invokes callback with error code and error message
	   *
	   * @param id the key that uniquely identifies an inventory item.
	   * @param callback the callback function to be passed this function's result.
	   *
	   * @returns nothing - use a callback and/or subscribe to RoarManager events for results of non-blocking calls.
	   **/
		void Use (string id, Roar.Callback<Roar.WebObjects.Items.UseResponse> callback);

		/**
	   * Returns the inventory item for a given key.
	   *
	   * @param id the key that uniquely identifies an inventory item.
	   *
	   * @returns the inventory item Hashtable associated with the *id*
	   *          or null if the inventory item does not exist in the data store.
	   */
		DomainObjects.InventoryItem GetGood (string id);

	}

}
