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
using UnityEngine;

namespace Roar.implementation.Components
{
	public class Gifts : IGifts
	{
		protected DataStore dataStore;
		protected ILogger logger;
  
		public Gifts (DataStore dataStore, ILogger logger)
		{
			this.dataStore = dataStore;
			this.logger = logger;
		}
    
		public void Fetch (Roar.Callback callback)
		{
			dataStore.gifts.Fetch (callback);
		}

		public bool HasDataFromServer { get { return dataStore.gifts.HasDataFromServer; } }
  
		public ArrayList List ()
		{
			return List (null);
		}

		public ArrayList List (Roar.Callback callback)
		{
			if (callback != null)
				callback (new Roar.CallbackInfo<object> (dataStore.gifts.List ()));
			return dataStore.gifts.List ();
		}
  
		// Returns the gift Hashtable associated with attribute `id`
		public Hashtable GetGift (string id)
		{
			return GetGift (id, null);
		}

		public Hashtable GetGift (string id, Roar.Callback callback)
		{
			if (callback != null)
				callback (new Roar.CallbackInfo<object> (dataStore.gifts.Get (id)));
			return dataStore.gifts.Get (id);
		}
	}
}