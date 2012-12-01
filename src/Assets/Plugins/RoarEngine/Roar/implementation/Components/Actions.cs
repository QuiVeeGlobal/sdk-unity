using System.Collections;
using Roar.Components;

namespace Roar.implementation.Components
{
	public class Actions : IActions
	{
		protected DataStore dataStore;
		protected IWebAPI.ITasksActions taskActions;

		public Actions (IWebAPI.ITasksActions taskActions, DataStore dataStore)
		{
			this.taskActions = taskActions;
			this.dataStore = dataStore;
		}

		public bool HasDataFromServer { get { return dataStore.actions.HasDataFromServer; } }

		public void Fetch (Roar.RequestCallback callback)
		{
			dataStore.actions.Fetch (callback);
		}

		public ArrayList List ()
		{
			return dataStore.actions.List ();
		}

		public void Execute (string ikey, Roar.RequestCallback callback)
		{

			Hashtable args = new Hashtable ();
			args ["task_ikey"] = ikey;

			taskActions.start (args, new OnActionsDo (callback, this));
		}
		class OnActionsDo : SimpleRequestCallback
		{
			//Actions actions;

			public OnActionsDo (Roar.RequestCallback in_cb, Actions in_actions) : base(in_cb)
			{
				//actions = in_actions;
			}

			public override void OnSuccess (RequestResult info)
			{
				// Event complete info (task_complete) is sent in a <server> chunk
				// (backend quirk related to potentially asynchronous tasks)
				// In this case its ALWAYS a synchronous call, so we KNOW the data will
				// be available - data is formatted in WebAPI Class.
				//var eventData = d["server"] as Hashtable;
				IXMLNode eventData = info.data.GetFirstChild ("server");

				RoarManager.OnEventDone (eventData);
			}
		}
	}

}
