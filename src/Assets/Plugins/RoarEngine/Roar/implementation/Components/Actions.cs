using System.Collections.Generic;
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

		public void Fetch (Roar.Callback< IDictionary<string,Foo> > callback)
		{
			dataStore.actions.Fetch (callback);
		}

		public IList<Foo> List ()
		{
			return dataStore.actions.List ();
		}

		public void Execute (string ikey, Roar.Callback<WebObjects.Tasks.StartResponse> callback)
		{
			WebObjects.Tasks.StartArguments args = new WebObjects.Tasks.StartArguments();
			args.ikey = ikey;
			taskActions.start (args, new OnActionsDo (callback));
		}
		class OnActionsDo : ZWebAPI.Callback<WebObjects.Tasks.StartResponse>
		{
			//Actions actions;
			Roar.Callback<WebObjects.Tasks.StartResponse> cb_;

			public OnActionsDo (Roar.Callback<WebObjects.Tasks.StartResponse> in_cb)
			{
				cb_ = in_cb;
			}

			public void OnError( Roar.RequestResult nn )
			{
				cb_( new CallbackInfo<WebObjects.Tasks.StartResponse>(null, nn.code, nn.msg) );
			}

			public void OnSuccess ( CallbackInfo<WebObjects.Tasks.StartResponse> info)
			{
				cb_(info);
			}
		}
	}

}
