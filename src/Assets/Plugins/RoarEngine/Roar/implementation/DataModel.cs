using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DC = Roar.implementation.DataConversion;
using Roar.DomainObjects;

public interface Fecther
{
};


public class DataModel<T> where T : Roar.DomainObjects.IDomainObject
{
	public string name;
	public Dictionary<string,T> attributes = new Dictionary<string,T> ();
	private Dictionary<string,T> previousAttributes = new Dictionary<string,T>();
	private bool hasChanged = false;
	private string serverDataAPI;
	private string node;
	private bool isServerCalling = false;
	public bool HasDataFromServer { get; set; }
	protected DC.IXmlToObject<T> xmlParser;
	protected IRequestSender api;
	protected Roar.ILogger logger;

	public DataModel (string name, string url, string node, ArrayList conditions, DC.IXmlToObject<T> xmlParser, IRequestSender api, Roar.ILogger logger)
	{
		this.name = name;
		serverDataAPI = url;
		this.node = node;
		this.xmlParser = xmlParser;
		this.api = api;
		this.logger = logger;
	}

	// Return code for calls attempting to access/modify Model data
	// if none is present
	private void OnNoData ()
	{
		OnNoData (null);
	}

	private void OnNoData (string key)
	{
		string msg = "No data intialised for Model: " + name;
		if (key != null)
			msg += " (Invalid access for \"" + key + "\")";

		logger.DebugLog ("[roar] -- " + msg);
	}

	// Removes all attributes from the model
	public void Clear (bool silent = false)
	{
		attributes = new Dictionary<string,T> ();

		// Set internal changed flag
		this.hasChanged = true;

		if (!silent) {
			RoarManager.OnComponentChange (name);
		}
	}


	// Internal call to retrieve model data from server and pass back
	// to `callback`. `params` is optional obj to pass to RoarAPI call.
	// `persistModel` optional can prevent Model data clearing.
	public bool Fetch (Roar.RequestCallback cb)
	{
		return Fetch (cb, null, false);
	}

	public bool Fetch (Roar.RequestCallback cb, Hashtable p)
	{
		return Fetch (cb, p, false);
	}

	public bool Fetch (Roar.RequestCallback cb, Hashtable p, bool persist)
	{
		// Bail out if call for this Model is already underway
		if (this.isServerCalling)
			return false;

		// Reset the internal register
		if (!persist)
			attributes = new Dictionary<string,T> ();

		// Using direct call (serverDataAPI url) rather than API mapping
		// - Unity doesn't easily support functions as strings: func['sub']['mo']()
		api.MakeCall (serverDataAPI, p, new OnFetch (cb, this));

		this.isServerCalling = true;
		return true;
	}

	private class OnFetch : SimpleRequestCallback
	{
		protected DataModel<T> model;

		public OnFetch (Roar.RequestCallback in_cb, DataModel<T> in_model) : base(in_cb)
		{
			model = in_model;
		}

		public override void Prologue ()
		{
			// Reset this function call
			model.isServerCalling = false;
		}

		public override void OnSuccess (Roar.RequestResult info)
		{
			model.logger.DebugLog ("onFetch got given: " + info.data.DebugAsString ());

			// First process the data for Model use
			string[] t = model.serverDataAPI.Split ('/');
			if (t.Length != 2)
				throw new System.ArgumentException ("Invalid url format - must be abc/def");
			string path = "roar>0>" + t [0] + ">0>" + t [1] + ">0>" + model.node;
			List<IXMLNode> nn = info.data.GetNodeList (path);
			if (nn == null) {
				model.logger.DebugLog (string.Format ("Unable to get node\nFor path = {0}\nXML = {1}", path, info.data.DebugAsString ()));
			} else {
				model.ProcessData (nn);
			}
		}
	}

	// Preps the data from server and places it within the Model
	private void ProcessData (List<IXMLNode> d)
	{
		Dictionary<string,T> o = new Dictionary<string,T> ();

		if (d == null)
			logger.DebugLog ("[roar] -- No data to process!");
		else {
			for (var i=0; i<d.Count; i++) {
				string key = xmlParser.GetKey (d [i]);
				if (key == null) {
					logger.DebugLog (string.Format ("no key found for {0}", d [i].DebugAsString ()));
					continue;
				}
				if (o.ContainsKey (key)) {
					logger.DebugLog ("Duplicate key found");
				} else {
					o [key] = xmlParser.Build(d [i]);;
				}
			}
		}

		// Flag server cache called
		// Must do before `set()` to flag before change events are fired
		HasDataFromServer = true;

		// Update the Model
		this.Set (o);

		logger.DebugLog ("Setting the model in " + name + " to : " + Roar.Json.ObjectToJSON (o));
		logger.DebugLog ("[roar] -- Data Loaded: " + name);

		// Broadcast data ready event
		RoarManager.OnComponentReady (this.name);
	}


	// Shallow clone object
	public static Dictionary<string,T> Clone (Dictionary<string,T> obj)
	{
		if (obj == null)
			return null;

		Dictionary<string,T> copy = new Dictionary<string,T> ();
		foreach (KeyValuePair<string,T> prop in obj) {
			copy [prop.Key] = prop.Value;
		}

		return copy;
	}


	// Have to prefix 'set' as '_set' due to Unity function name restrictions
	public DataModel<T> Set (Dictionary<string,T> data)
	{
		return Set (data, false);
	}

	public DataModel<T> Set ( Dictionary<string,T> data, bool silent)
	{
		// Setup temporary copy of attributes to be assigned
		// to the previousAttributes register if a change occurs
		var prev = Clone (this.attributes);

		foreach (KeyValuePair<string,T> prop in data) {
			this.attributes [prop.Key] = prop.Value;

			// Set internal changed flag
			this.hasChanged = true;

			// Broadcasts an attribute specific change event of the form:
			// **change:attribute_name**
			if (!silent) {
				RoarManager.OnComponentChange (this.name);
			}
		}

		// Broadcasts a `change` event if the model changed
		if (HasChanged && !silent) {
			this.previousAttributes = prev;
			this.Change ();
		}

		return this;
	}


	// Removes an attribute from the data model
	// and fires a change event unless `silent` is passed as an option
	public void Unset (string key)
	{
		Unset (key, false);
	}

	public void Unset (string key, bool silent)
	{
		// Setup temporary copy of attributes to be assigned
		// to the previousAttributes register if a change occurs
		var prev = Clone (this.attributes);

		// Check that server data is present
		if (!HasDataFromServer) {
			this.OnNoData (key);
			return;
		}

		if (this.attributes [key] != null) {
			// Remove the specific element
			this.attributes.Remove (key);

			this.hasChanged = true;
			// Broadcasts an attribute specific change event of the form:
			// **change:attribute_name**
			if (!silent) {
				RoarManager.OnComponentChange (this.name);
			}
		}

		// Broadcasts a `change` event if the model changed
		if (HasChanged && !silent) {
			this.previousAttributes = prev;
			this.Change ();
		}
	}

	// Returns the value of a given data key (usually an object)
	// Using '_get' due to Unity restrictions on function names
	public T Get (string key)
	{
		// Check that server data is present
		if (!HasDataFromServer) {
			this.OnNoData (key);
			return default(T);
		}

		if (this.attributes [key] != null) {
			return this.attributes[key];
		}
		logger.DebugLog ("[roar] -- No property found: " + key);
		return default(T);
	}


	// Returns an array of all the elements in this.attributes
	public IList<T> List ()
	{
		var l = new List<T>();

		// Check that server data is present
		if (!HasDataFromServer) {
			this.OnNoData();
			return l;
		}

		foreach (KeyValuePair<string,T> prop in this.attributes) {
			l.Add (prop.Value);
		}
		return l;
	}

	// Returns the object of an attribute key from the PREVIOUS register
	public T Previous (string key)
	{
		// Check that server data is present
		if (!HasDataFromServer) {
			this.OnNoData (key);
			return default(T);
		}

		return this.previousAttributes[key];
	}

	// Checks whether element `key` is present in the
	// list of ikeys in the Model. Optional `number` to search, default 1
	// Returns true if player has equal or greater number, false if not, and
	// null for an invalid query.
	public bool Has (string key)
	{
		return Has (key, 1);
	}

	public bool Has (string key, int number)
	{
		// Fire warning *only* if no data intitialised, but continue
		if (!HasDataFromServer) {
			this.OnNoData (key);
			return false;
		}

		int count = 0;
		foreach (KeyValuePair<string,T> i in this.attributes) {
			if ( i.Value.MatchesKey(key) ) count++;
		}

		return count >= number;
	}

	// Similar to Model.Has(), but returns the number of elements in the
	// Model of id or ikey `key`.
	public int Quantity (string key)
	{
		// Fire warning *only* if no data initialised, but continue
		if (!HasDataFromServer) {
			this.OnNoData (key);
			return 0;
		}

		int count = 0;
		foreach (KeyValuePair<string,T> i in this.attributes) {
			// Search `ikey`, `id` and `shop_ikey` keys and increment counter if found
			if ( i.Value.MatchesKey(key) ) count++;
		}

		return count;
	}

	// Flag to indicate whether the model has changed since last "change" event
	public bool HasChanged
	{
		get
		{
			return hasChanged;
		}
	}

	// Manually fires a "change" event on this model
	public void Change ()
	{
		RoarManager.OnComponentChange (this.name);
		this.hasChanged = false;
	}
}


public class DataModelZ<CT,DT> where DT:class
{
	public Dictionary<string,CT> attributes = new Dictionary<string,CT> ();
	private Dictionary<string,CT> previousAttributes = new Dictionary<string,CT>();
	private bool hasChanged = false;
	private bool isServerCalling = false;
	public bool HasDataFromServer { get; set; }
	public string name;
	public IDomGetter<DT> getter;
	public IDomToCache<DT,CT> converter;
	
	
	protected Roar.ILogger logger;
	
	public DataModelZ (string name, IDomGetter<DT> getter, IDomToCache<DT,CT> converter, Roar.ILogger logger)
	{
		this.name = name;
		this.logger = logger;
		this.getter = getter;
		this.converter = converter;
	}

	// Return code for calls attempting to access/modify Model data
	// if none is present
	private void OnNoData ()
	{
		OnNoData (null);
	}

	private void OnNoData (string key)
	{
		string msg = "No data intialised for Model: " + name;
		if (key != null)
			msg += " (Invalid access for \"" + key + "\")";

		logger.DebugLog ("[roar] -- " + msg);
	}

	// Removes all attributes from the model
	public void Clear (bool silent = false)
	{
		attributes = new Dictionary<string,CT> ();

		// Set internal changed flag
		this.hasChanged = true;

		if (!silent) {
			RoarManager.OnComponentChange (name);
		}
	}


	// Internal call to retrieve model data from server and pass back
	// to `callback`. `params` is optional obj to pass to RoarAPI call.
	// `persistModel` optional can prevent Model data clearing.
	public bool Fetch (Roar.Callback< IDictionary<string,CT> > cb)
	{
		return Fetch (cb, null, false);
	}

	public bool Fetch (Roar.Callback< IDictionary<string,CT> > cb, Hashtable p)
	{
		return Fetch (cb, p, false);
	}

	//TODO: Should this take Roar.Callback<CT> instead?a
	public bool Fetch (Roar.Callback< IDictionary<string,CT> > cb, Hashtable p, bool persist)
	{
		// Bail out if call for this Model is already underway
		if (this.isServerCalling)
			return false;

		// Reset the internal register
		if (!persist)
			attributes = new Dictionary<string,CT> ();

		getter.get( new OnFetch( cb, this ) );

		this.isServerCalling = true;
		return true;
	}

	private class OnFetch : ZWebAPI.Callback<DT>
	{
		protected DataModelZ<CT,DT> model;
		protected Roar.Callback< IDictionary<string,CT> > cb;

		public OnFetch (Roar.Callback< IDictionary<string,CT> > in_cb, DataModelZ<CT,DT> in_model)
		{
			model = in_model;
			cb = in_cb;
		}

		public void OnError ( Roar.RequestResult info)
		{
			// Reset this function call
			model.isServerCalling = false;
			cb( new Roar.CallbackInfo< IDictionary<string,CT> >(null, info.code, info.msg ) );
		}

		public void OnSuccess(Roar.CallbackInfo<DT> info)
		{
			model.isServerCalling = false;
			model.logger.DebugLog ("onFetch got given: " + info.data.ToString() );
			model.ProcessData (info.data, cb);
		}
	}

	// Preps the data from server and places it within the Model
	// Not clear whether this should ammend or replace the data
	// At the moment it ammends!
	private void ProcessData ( DT d, Roar.Callback< IDictionary<string,CT> > cb)
	{
		Dictionary<string,CT> o = converter.convert(d);

		// Flag server cache called
		// Must do before `set()` to flag before change events are fired
		HasDataFromServer = true;

		// Update the Model
		this.Set (o);
		cb( new Roar.CallbackInfo< IDictionary<string,CT> >( o, WebAPI.OK, null ) );

		logger.DebugLog ("Setting the model in " + name + " to : " + Roar.Json.ObjectToJSON (o));
		logger.DebugLog ("[roar] -- Data Loaded: " + name);

		// Broadcast data ready event
		RoarManager.OnComponentReady (this.name);
	}


	// Shallow clone object
	public static Dictionary<string,CT> Clone (Dictionary<string,CT> obj)
	{
		return (obj == null) ? null : new Dictionary<string, CT>( obj );
	}


	// Have to prefix 'set' as '_set' due to Unity function name restrictions
	public DataModelZ<CT,DT> Set (Dictionary<string,CT> data)
	{
		return Set (data, false);
	}

	//TODO: This should probably be called "Add" as it seems to 
	// update/add to the model rather than replace all the entries.
	
	public DataModelZ<CT,DT> Set ( Dictionary<string,CT> data, bool silent)
	{
		// Setup temporary copy of attributes to be assigned
		// to the previousAttributes register if a change occurs
		var prev = Clone (this.attributes);

		foreach (KeyValuePair<string,CT> prop in data) {
			this.attributes [prop.Key] = prop.Value;

			// Set internal changed flag
			this.hasChanged = true;

			// Broadcasts an attribute specific change event of the form:
			// **change:attribute_name**
			if (!silent) {
				RoarManager.OnComponentChange (this.name);
			}
		}

		// Broadcasts a `change` event if the model changed
		if (HasChanged && !silent) {
			this.previousAttributes = prev;
			this.Change ();
		}

		return this;
	}


	// Removes an attribute from the data model
	// and fires a change event unless `silent` is passed as an option
	public void Unset (string key)
	{
		Unset (key, false);
	}

	public void Unset (string key, bool silent)
	{
		// Setup temporary copy of attributes to be assigned
		// to the previousAttributes register if a change occurs
		var prev = Clone (this.attributes);

		// Check that server data is present
		if (!HasDataFromServer) {
			this.OnNoData (key);
			return;
		}

		if (this.attributes [key] != null) {
			// Remove the specific element
			this.attributes.Remove (key);

			this.hasChanged = true;
			// Broadcasts an attribute specific change event of the form:
			// **change:attribute_name**
			if (!silent) {
				RoarManager.OnComponentChange (this.name);
			}
		}

		// Broadcasts a `change` event if the model changed
		if (HasChanged && !silent) {
			this.previousAttributes = prev;
			this.Change ();
		}
	}

	// Returns the value of a given data key (usually an object)
	// Using '_get' due to Unity restrictions on function names
	public CT Get (string key)
	{
		// Check that server data is present
		if (!HasDataFromServer) {
			this.OnNoData (key);
			return default(CT);
		}

		if (this.attributes [key] != null) {
			return this.attributes[key];
		}
		logger.DebugLog ("[roar] -- No property found: " + key);
		return default(CT);
	}


	// Returns an array of all the elements in this.attributes
	public IList<CT> List ()
	{
		var l = new List<CT>();

		// Check that server data is present
		if (!HasDataFromServer) {
			this.OnNoData();
			return l;
		}

		foreach (KeyValuePair<string,CT> prop in this.attributes) {
			l.Add (prop.Value);
		}
		return l;
	}

	// Returns the object of an attribute key from the PREVIOUS register
	public CT Previous (string key)
	{
		// Check that server data is present
		if (!HasDataFromServer) {
			this.OnNoData (key);
			return default(CT);
		}

		return this.previousAttributes[key];
	}

	// Flag to indicate whether the model has changed since last "change" event
	public bool HasChanged
	{
		get
		{
			return hasChanged;
		}
	}

	// Manually fires a "change" event on this model
	public void Change ()
	{
		RoarManager.OnComponentChange (this.name);
		this.hasChanged = false;
	}
}