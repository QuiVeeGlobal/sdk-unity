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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class RoarManager
{
	public class GoodInfo
	{
		public GoodInfo( string in_id, string in_ikey, string in_label )
		{
			id = in_id;
			ikey = in_ikey;
			label = in_label;
		}
		public string id;
		public string ikey;
		public string label;
	};

	public class PurchaseInfo
	{
		public PurchaseInfo( string in_ikey, Roar.DomainObjects.ModifierResult in_response )
		{
			ikey = in_ikey;
			response = in_response;
		}
		public string ikey;
		public Roar.DomainObjects.ModifierResult response;
	};

	public class CallInfo
	{
		public CallInfo( object xml_node, int code, string message, string call_id)
		{
		}
	};



	public static event Action<string> logInFailedEvent;
	public static void OnLogInFailed( string mesg ) { if(logInFailedEvent!=null) logInFailedEvent(mesg); }

	public static event Action<string> createUserFailedEvent;
	public static void OnCreateUserFailed( string mesg ) { if(createUserFailedEvent!=null) createUserFailedEvent(mesg); }

	public static event Action loggedOutEvent;
	public static void OnLoggedOut() { if(loggedOutEvent!=null) loggedOutEvent(); }

	public static event Action loggedInEvent;
	public static void OnLoggedIn() { if(loggedInEvent!=null) loggedInEvent(); }

	public static event Action createdUserEvent;
	public static void OnCreatedUser() { if(createdUserEvent!=null) createdUserEvent(); }

	public static event Action<GoodInfo> goodActivatedEvent;
	public static void OnGoodActivated( GoodInfo info ) { if(goodActivatedEvent!=null) goodActivatedEvent(info); }

	public static event Action<GoodInfo> goodDeactivatedEvent;
	public static void OnGoodDeactivated( GoodInfo info ) { if(goodDeactivatedEvent!=null) goodDeactivatedEvent(info); }

	public static event Action<GoodInfo> goodUsedEvent;
	public static void OnGoodUsed( GoodInfo info ) { if(goodUsedEvent!=null) goodUsedEvent(info); }

	public static event Action<GoodInfo> goodSoldEvent;
	public static void OnGoodSold( GoodInfo info ) { if(goodSoldEvent!=null) goodSoldEvent(info); }

	/**
	 * Fired when a shop item has been successfully purchased.
	 */
	public static event Action<PurchaseInfo> goodBoughtEvent;
	public static void OnGoodBought( PurchaseInfo info ) { if(goodBoughtEvent!=null) goodBoughtEvent(info); }

	public static event Action<IXMLNode> eventDoneEvent;
	public static void OnEventDone( IXMLNode eventInfo ) { if(eventDoneEvent!=null) eventDoneEvent(eventInfo); }

	public static event Action<string,string> dataLoadedEvent;
	public static void OnDataLoaded( string key, string value ) { if(dataLoadedEvent!=null) dataLoadedEvent(key, value); }

	public static event Action<string,string> dataSavedEvent;
	public static void OnDataSaved( string key, string value ) { if(dataSavedEvent!=null) dataSavedEvent(key, value); }

	public static event Action roarNetworkStartEvent;
	public static void OnRoarNetworkStart() { if(roarNetworkStartEvent!=null) roarNetworkStartEvent(); }

	public static event Action<string> roarNetworkEndEvent;
	public static void OnRoarNetworkEnd( string call_id ) { if(roarNetworkEndEvent!=null) roarNetworkEndEvent(call_id); }

	public static event Action<CallInfo> callCompleteEvent;
	public static void OnCallComplete( CallInfo info ) { if(callCompleteEvent!=null) callCompleteEvent(info); }

	/**
	 * @note The object is really an XML Node.
	 * @todo It's ugly to be using an implementation detail like this.
	 */
	public static event Action<object> roarServerAllEvent;
	public static void OnRoarServerAll( object info ) { if(roarServerAllEvent!=null) roarServerAllEvent(info); }

	/**
	 * @todo These should be generated for each component.
	 */
	public static event Action xxxChangeEvent;
	public static void OnXxxChange() { if(xxxChangeEvent!=null) xxxChangeEvent(); }

	/**
	 * Fired when the data have been retrieved from the server.
	 */
	public static event Action propertiesReadyEvent;
	public static void OnPropertiesReady() { if(propertiesReadyEvent!=null) propertiesReadyEvent(); }

	/**
	 * Fired when the data changes.
	 */
	public static event Action propertiesChangeEvent;
	public static void OnPropertiesChange() { if(propertiesChangeEvent!=null) propertiesChangeEvent(); }

	/**
	 * Fired when the data have been retrieved from the server.
	 */
	public static event Action leaderboardsReadyEvent;
	public static void OnLeaderboardsReady() { if(leaderboardsReadyEvent!=null) leaderboardsReadyEvent(); }

	/**
	 * Fired when the data changes.
	 */
	public static event Action leaderboardsChangeEvent;
	public static void OnLeaderboardsChange() { if(leaderboardsChangeEvent!=null) leaderboardsChangeEvent(); }

	/**
	 * Fired when the data have been retrieved from the server.
	 */
	public static event Action rankingReadyEvent;
	public static void OnRankingReady() { if(rankingReadyEvent!=null) rankingReadyEvent(); }

	/**
	 * Fired when the data changes.
	 */
	public static event Action rankingChangeEvent;
	public static void OnRankingChange() { if(rankingChangeEvent!=null) rankingChangeEvent(); }

	/**
	 * Fired when the data have been retrieved from the server.
	 */
	public static event Action shopReadyEvent;
	public static void OnShopReady() { if(shopReadyEvent!=null) shopReadyEvent(); }

	/**
	 * Fired when the data changes.
	 */
	public static event Action shopChangeEvent;
	public static void OnShopChange() { if(shopChangeEvent!=null) shopChangeEvent(); }

	/**
	 * Fired when the data have been retrieved from the server.
	 */
	public static event Action inventoryReadyEvent;
	public static void OnInventoryReady() { if(inventoryReadyEvent!=null) inventoryReadyEvent(); }

	/**
	 * Fired when the data changes.
	 */
	public static event Action inventoryChangeEvent;
	public static void OnInventoryChange() { if(inventoryChangeEvent!=null) inventoryChangeEvent(); }

	/**
	 * Fired when the data have been retrieved from the server.
	 */
	public static event Action cacheReadyEvent;
	public static void OnCacheReady() { if(cacheReadyEvent!=null) cacheReadyEvent(); }

	/**
	 * Fired when the data changes.
	 */
	public static event Action cacheChangeEvent;
	public static void OnCacheChange() { if(cacheChangeEvent!=null) cacheChangeEvent(); }

	/**
	 * Fired when the data have been retrieved from the server.
	 */
	public static event Action tasksReadyEvent;
	public static void OnTasksReady() { if(tasksReadyEvent!=null) tasksReadyEvent(); }

	/**
	 * Fired when the data changes.
	 */
	public static event Action tasksChangeEvent;
	public static void OnTasksChange() { if(tasksChangeEvent!=null) tasksChangeEvent(); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 */
	public static event Action<IXMLNode> roarServerUpdateEvent;
	public static void OnRoarServerUpdate( IXMLNode info ) { if(roarServerUpdateEvent!=null) roarServerUpdateEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <item_use item_id="1234">
	 */
	public static event Action<IXMLNode> roarServerItemUseEvent;
	public static void OnRoarServerItemUse( IXMLNode info ) { if(roarServerItemUseEvent!=null) roarServerItemUseEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <item_lose item_id="1234" item_ikey="somthing"/>
	 */
	public static event Action<IXMLNode> roarServerItemLoseEvent;
	public static void OnRoarServerItemLose( IXMLNode info ) { if(roarServerItemLoseEvent!=null) roarServerItemLoseEvent(info); }

	/**
	 * common.php says there is no info in this xml so probably dont need to parse it.
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 */
	public static event Action<IXMLNode> roarServerInventoryChangedEvent;
	public static void OnRoarServerInventoryChanged( IXMLNode info ) { if(roarServerInventoryChangedEvent!=null) roarServerInventoryChangedEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 */
	public static event Action<IXMLNode> roarServerRegenEvent;
	public static void OnRoarServerRegen( IXMLNode info ) { if(roarServerRegenEvent!=null) roarServerRegenEvent(info); }

	/**
	 * @todo Ugly to be using a hash here
	 * @todo Implement more server update functions
	 * 
	 *     <item_add item_id="1234" item_ikey="somthing"/>
	 */
	public static event Action<IXMLNode> roarServerItemAddEvent;
	public static void OnRoarServerItemAdd( IXMLNode info ) { if(roarServerItemAddEvent!=null) roarServerItemAddEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 */
	public static event Action<IXMLNode> roarServerTaskCompleteEvent;
	public static void OnRoarServerTaskComplete( IXMLNode info ) { if(roarServerTaskCompleteEvent!=null) roarServerTaskCompleteEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 */
	public static event Action<Roar.DomainObjects.Achievement> roarServerAchievementCompleteEvent;
	public static void OnRoarServerAchievementComplete( Roar.DomainObjects.Achievement info ) { if(roarServerAchievementCompleteEvent!=null) roarServerAchievementCompleteEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <level_up value="5" />
	 */
	public static event Action<IXMLNode> roarServerLevelUpEvent;
	public static void OnRoarServerLevelUp( IXMLNode info ) { if(roarServerLevelUpEvent!=null) roarServerLevelUpEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <collect_changed ikey="health" next="12313231" />
	 */
	public static event Action<IXMLNode> roarServerCollectChangedEvent;
	public static void OnRoarServerCollectChanged( IXMLNode info ) { if(roarServerCollectChangedEvent!=null) roarServerCollectChangedEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <invite_accepted name="Lex Luthor" player_id="12313231" level="123" />
	 */
	public static event Action<IXMLNode> roarServerInviteAcceptedEvent;
	public static void OnRoarServerInviteAccepted( IXMLNode info ) { if(roarServerInviteAcceptedEvent!=null) roarServerInviteAcceptedEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <friend_request name="Lex Luthor" from_player_id="12313231" level="123" />
	 */
	public static event Action<IXMLNode> roarServerFriendRequestEvent;
	public static void OnRoarServerFriendRequest( IXMLNode info ) { if(roarServerFriendRequestEvent!=null) roarServerFriendRequestEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <transaction ikey="diamonds" value="120" /> 
	 */
	public static event Action<IXMLNode> roarServerTransactionEvent;
	public static void OnRoarServerTransaction( IXMLNode info ) { if(roarServerTransactionEvent!=null) roarServerTransactionEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <mail_in/>
	 */
	public static event Action<IXMLNode> roarServerMailInEvent;
	public static void OnRoarServerMailIn( IXMLNode info ) { if(roarServerMailInEvent!=null) roarServerMailInEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <equip item_id="1234"/>
	 */
	public static event Action<IXMLNode> roarServerEquipEvent;
	public static void OnRoarServerEquip( IXMLNode info ) { if(roarServerEquipEvent!=null) roarServerEquipEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <unequip item_id="1234"/>
	 */
	public static event Action<IXMLNode> roarServerUnequipEvent;
	public static void OnRoarServerUnequip( IXMLNode info ) { if(roarServerUnequipEvent!=null) roarServerUnequipEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 * Generated by lua like this
	 * 
	 *     p:notify('abc','blah')
	 * 
	 *     <script key="abc" value="blah"/>
	 */
	public static event Action<IXMLNode> roarServerScriptEvent;
	public static void OnRoarServerScript( IXMLNode info ) { if(roarServerScriptEvent!=null) roarServerScriptEvent(info); }

	/**
	 * @todo Ugly to be using a hash here.
	 * @todo Implement more server update functions.
	 * 
	 *     <chrome_web_store ikey="abc" transaction_id="def">
	 *       <costs> ... </costs>
	 *       ...
	 *     </chrome_web_store>
	 */
	public static event Action<IXMLNode> roarServerChromeWebStoreEvent;
	public static void OnRoarServerChromeWebStore( IXMLNode info ) { if(roarServerChromeWebStoreEvent!=null) roarServerChromeWebStoreEvent(info); }


  	/**
  	 * Fire the correct event for a server chunk.
	 *
  	 * @param key the event name.
  	 * @param info the event info.
  	 **/
	public static void OnServerEvent( string key, IXMLNode info )
	{
		switch(key)
		{
			case "update":
				OnRoarServerUpdate(info);
				break;
			case "item_use":
				OnRoarServerItemUse(info);
				break;
			case "item_lose":
				OnRoarServerItemLose(info);
				break;
			case "inventory_changed":
				OnRoarServerInventoryChanged(info);
				break;
			case "regen":
				OnRoarServerRegen(info);
				break;
			case "item_add":
				OnRoarServerItemAdd(info);
				break;
			case "task_complete":
				OnRoarServerTaskComplete(info);
				break;
			case "achievement_complete":
				OnRoarServerAchievementComplete(Roar.DomainObjects.Achievement.CreateFromXml(info));
				break;
			case "level_up":
				OnRoarServerLevelUp(info);
				break;
			case "collect_changed":
				OnRoarServerCollectChanged(info);
				break;
			case "invite_accepted":
				OnRoarServerInviteAccepted(info);
				break;
			case "friend_request":
				OnRoarServerFriendRequest(info);
				break;
			case "transaction":
				OnRoarServerTransaction(info);
				break;
			case "mail_in":
				OnRoarServerMailIn(info);
				break;
			case "equip":
				OnRoarServerEquip(info);
				break;
			case "unequip":
				OnRoarServerUnequip(info);
				break;
			case "script":
				OnRoarServerScript(info);
				break;
			case "chrome_web_store":
				OnRoarServerChromeWebStore(info);
				break;

			default:
				Debug.Log("Server event "+key+" not yet implemented");
				break;
		}
	}

	/** 
	 * Fire the correct event for a component change.
	 *
	 * @param name The name of the event.
	 */
	public static void OnComponentChange( string name )
	{
		switch(name)
		{
		case "properties":
			OnPropertiesChange();
			break;
		case "leaderboards":
			OnLeaderboardsChange();
			break;
		case "ranking":
			OnRankingChange();
			break;
		case "shop":
			OnShopChange();
			break;
		case "inventory":
			OnInventoryChange();
			break;
		case "cache":
			OnCacheChange();
			break;
		case "tasks":
			OnTasksChange();
			break;

		default:
			Debug.Log ("Component change event for "+name+" not yet implemented");
			break;
		}
	}

	/**
	 * Fire the correct event for a component ready.
	 *
	 * @param name The name of the event.
	 */
	public static void OnComponentReady( string name )
	{
		switch(name)
		{
		case "properties":
			OnPropertiesReady();
			break;
		case "leaderboards":
			OnLeaderboardsReady();
			break;
		case "ranking":
			OnRankingReady();
			break;
		case "shop":
			OnShopReady();
			break;
		case "inventory":
			OnInventoryReady();
			break;
		case "cache":
			OnCacheReady();
			break;
		case "tasks":
			OnTasksReady();
			break;

		default:
			Debug.Log ("Component ready event for "+name+" not yet implemented");
			break;
		}
	}

	/**
	 * Fire off the events for all the contained server events.
	 */
	public static void NotifyOfServerChanges( IXMLNode node )
	{
		if( node == null ) return;
		OnRoarServerAll( node );
		foreach( IXMLNode nn in node.Children )
		{
			OnServerEvent( nn.Name, nn );
		}
	}

}
