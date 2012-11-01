/*
Copyright (c) 2012, Run With Robots
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the roar.io library nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY RUN WITH ROBOTS ''AS IS'' AND ANY
EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL MICHAEL ANDERSON BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System.Collections;
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

		public void Fetch (Roar.Callback callback)
		{
			dataStore.properties.Fetch (callback);
		}

		public bool HasDataFromServer { get { return dataStore.properties.HasDataFromServer; } }

		public ArrayList List ()
		{
			return List (null);
		}

		public ArrayList List (Roar.Callback callback)
		{
			if (callback != null)
				callback (new Roar.CallbackInfo<object> (dataStore.properties.List ()));
			return dataStore.properties.List ();
		}

		// Returns the *object* associated with attribute `key`
		public object GetProperty (string key)
		{
			return GetProperty (key, null);
		}

		public object GetProperty (string key, Roar.Callback callback)
		{
			if (callback != null)
				callback (new Roar.CallbackInfo<object> (dataStore.properties.Get (key)));
			return dataStore.properties.Get (key);
		}

		// Returns the *value* of attribute `key`
		public string GetValue (string ikey)
		{
			return GetValue (ikey, null);
		}

		public string GetValue (string ikey, Roar.Callback callback)
		{
			if (callback != null)
				callback (new Roar.CallbackInfo<object> (dataStore.properties.GetValue (ikey)));
			return dataStore.properties.GetValue (ikey);
		}

		protected void OnUpdate (IXMLNode update)
		{
			//Since you can get change events from login calls, when the Properties object is not yet setup we need to be careful here:
			if (! HasDataFromServer)
				return;

			//var d = event['data'] as Hashtable;

			var v = GetProperty (update.GetAttribute ("ikey")) as Hashtable;
			if (v != null) {
				v ["value"] = update.GetAttribute ("value");
			}
		}	

	}
}