using System.Collections;
using System.Collections.Generic;
using Roar.Components;

namespace Roar.implementation.Components
{
	public class Properties : IProperties
	{
		protected DataStore dataStore;

		public Properties (DataStore dataStore)
		{
			this.dataStore = dataStore;
			RoarManager.roarServerUpdateEvent += this.OnUpdate;
		}

		public void Fetch (Roar.Callback< IDictionary<string,Property> > callback)
		{
			dataStore.properties.Fetch (callback);
		}

		public bool HasDataFromServer { get { return dataStore.properties.HasDataFromServer; } }

		public IList<Property> List ()
		{
			return dataStore.properties.List ();
		}

		// Returns the *object* associated with attribute `key`
		public Property GetProperty (string key)
		{
			return dataStore.properties.Get (key);
		}

		// Returns the *value* of attribute `key`
		public string GetValue (string ikey)
		{
			return dataStore.properties.Get(ikey).value;
		}

		protected void OnUpdate (IXMLNode update)
		{
			//Since you can get change events from login calls, when the Properties object is not yet setup we need to be careful here:
			if (! HasDataFromServer)
				return;

			//var d = event['data'] as Hashtable;

			Property v = GetProperty (update.GetAttribute ("ikey"));
			if (v != null) {
				v.value = update.GetAttribute ("value");
			}
		}

	}
}
