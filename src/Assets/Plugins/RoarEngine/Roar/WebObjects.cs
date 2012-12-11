using System;
using System.Collections;
using System.Collections.Generic;


namespace Roar.WebObjects
{

	//Namespace for typesafe arguments and responses to Roars admin/foo calls.
	namespace Admin
	{

		// Arguments to admin/delete_player
		public class DeletePlayerArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from admin/delete_player
		public class DeletePlayerResponse
		{

		}

		// Arguments to admin/increment_stat
		public class IncrementStatArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from admin/increment_stat
		public class IncrementStatResponse
		{

		}

		// Arguments to admin/set
		public class SetArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from admin/set
		public class SetResponse
		{

		}

		// Arguments to admin/set_custom
		public class SetCustomArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from admin/set_custom
		public class SetCustomResponse
		{

		}

		// Arguments to admin/view_player
		public class ViewPlayerArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from admin/view_player
		public class ViewPlayerResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars appstore/foo calls.
	namespace Appstore
	{

		// Arguments to appstore/buy
		public class BuyArguments
		{
			public string receipt;
			public bool sandbox;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["receipt"] = receipt;
				retval["sandbox"] = Convert.ToString(sandbox);
				return retval;
			}
		}
		
		// Response from appstore/buy
		public class BuyResponse
		{

		}

		// Arguments to appstore/shop_list
		public class ShopListArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from appstore/shop_list
		public class ShopListResponse
		{
			public List<string> productIdentifiers;

		}

	}

	//Namespace for typesafe arguments and responses to Roars chrome_web_store/foo calls.
	namespace ChromeWebStore
	{

		// Arguments to chrome_web_store/list
		public class ListArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from chrome_web_store/list
		public class ListResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars facebook/foo calls.
	namespace Facebook
	{

		// Arguments to facebook/bind_signed
		public class BindSignedArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/bind_signed
		public class BindSignedResponse
		{

		}

		// Arguments to facebook/create_oauth
		public class CreateOauthArguments
		{
			public string oauth_token;
			public string name;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["oauth_token"] = oauth_token;
				retval["name"] = name;
				return retval;
			}
		}
		
		// Response from facebook/create_oauth
		public class CreateOauthResponse
		{

		}

		// Arguments to facebook/create_signed
		public class CreateSignedArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/create_signed
		public class CreateSignedResponse
		{

		}

		// Arguments to facebook/fetch_oauth_token
		public class FetchOauthTokenArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/fetch_oauth_token
		public class FetchOauthTokenResponse
		{

		}

		// Arguments to facebook/friends
		public class FriendsArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/friends
		public class FriendsResponse
		{

		}

		// Arguments to facebook/login_oauth
		public class LoginOauthArguments
		{
			public string oauth_token;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["oauth_token"] = oauth_token;
				return retval;
			}
		}
		
		// Response from facebook/login_oauth
		public class LoginOauthResponse
		{

		}

		// Arguments to facebook/login_signed
		public class LoginSignedArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/login_signed
		public class LoginSignedResponse
		{

		}

		// Arguments to facebook/shop_list
		public class ShopListArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/shop_list
		public class ShopListResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars friends/foo calls.
	namespace Friends
	{

		// Arguments to friends/accept
		public class AcceptArguments
		{
			public string friends_id;
			public string invite_id;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["friends_id"] = friends_id;
				retval["invite_id"] = invite_id;
				return retval;
			}
		}
		
		// Response from friends/accept
		public class AcceptResponse
		{

		}

		// Arguments to friends/decline
		public class DeclineArguments
		{
			public string invite_id;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["invite_id"] = invite_id;
				return retval;
			}
		}
		
		// Response from friends/decline
		public class DeclineResponse
		{

		}

		// Arguments to friends/invite
		public class InviteArguments
		{
			public string friend_id;
			public string player_id; // TODO: This argument may not be needed 

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["friend_id"] = friend_id;
				retval["player_id"] = player_id;
				return retval;
			}
		}
		
		// Response from friends/invite
		public class InviteResponse
		{

		}

		// Arguments to friends/invite_info
		public class InviteInfoArguments
		{
			public string invite_id;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["invite_id"] = invite_id;
				return retval;
			}
		}
		
		// Response from friends/invite_info
		public class InviteInfoResponse
		{
			public DomainObjects.FriendInviteInfo info;

		}

		// Arguments to friends/list
		public class ListArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		public class ParseXmlTo
		{
			public static DomainObjects.Friend Friend( IXMLNode n )
			{
				DomainObjects.Friend f = new DomainObjects.Friend();
				f.player_id = n.GetFirstChild("player_id").Text;
				f.name = n.GetFirstChild("name").Text;
				f.level = System.Convert.ToInt32( n.GetFirstChild("level").Text );
				return f;
			}
		}

		
		
		// Response from friends/list
		/*
		 * <roar tick='123' status='ok'>
		 *   <friends>
		 *     <list status='ok'>
		 *       <friend>
		 *         <player_id>123</player_id>
		 *         <name>Brenda Lear</name>
		 *         <level>9</level>
		 *       </friend>
		 *       <friend>
		 *         <player_id>456</player_id>
		 *         <name>Paul Barley</name>
		 *         <level>7</level>
		 *       </friend>
		 *     </list>
		 *   </friends>
		 * </roar>
		 */
		public class ListResponse
		{
			public List<DomainObjects.Friend> friends;

			public void ParseXml( IXMLNode nn )
			{
				friends = new List<DomainObjects.Friend>();
				
				List<IXMLNode> friend_nodes = nn.GetNodeList("roar>0>friends>0>list>0>friend");
				foreach( IXMLNode n in friend_nodes )
				{
					DomainObjects.Friend a = ParseXmlTo.Friend(n);
					friends.Add(a);
				}
			}
		}

		// Arguments to friends/remove
		public class RemoveArguments
		{
			public string friend_id;
			public string player_id; // TODO: This argument may not be needed 

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["friend_id"] = friend_id;
				retval["player_id"] = player_id;
				return retval;
			}
		}
		
		// Response from friends/remove
		public class RemoveResponse
		{

		}

		// Arguments to friends/list_invites
		public class ListInvitesArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from friends/list_invites
		public class ListInvitesResponse
		{
			public List<DomainObjects.FriendInvite> invites;

			public void ParseXml( IXMLNode nn )
			{
				invites = new List<DomainObjects.FriendInvite>();
				List<IXMLNode> invite_nodes = nn.GetNodeList("roar>0>friends>0>list_invites>0>friend_invite");
				foreach( IXMLNode n in invite_nodes )
				{
					DomainObjects.FriendInvite invite = new DomainObjects.FriendInvite();
					Dictionary<string,string> kv = n.Attributes.ToDictionary( v => v.Key, v => v.Value );

					kv.TryGetValue("invite_id",out invite.invite_id);
					invite.player_id = n.GetFirstChild("player_id").Text;
					invites.Add(invite);
				}
			}
		}

	}

	//Namespace for typesafe arguments and responses to Roars google/foo calls.
	namespace Google
	{

		// Arguments to google/bind_user
		public class BindUserArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/bind_user
		public class BindUserResponse
		{

		}

		// Arguments to google/bind_user_token
		public class BindUserTokenArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/bind_user_token
		public class BindUserTokenResponse
		{

		}

		// Arguments to google/create_user
		public class CreateUserArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/create_user
		public class CreateUserResponse
		{

		}

		// Arguments to google/create_user_token
		public class CreateUserTokenArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/create_user_token
		public class CreateUserTokenResponse
		{

		}

		// Arguments to google/friends
		public class FriendsArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/friends
		public class FriendsResponse
		{

		}

		// Arguments to google/login_user
		public class LoginUserArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/login_user
		public class LoginUserResponse
		{

		}

		// Arguments to google/login_user_token
		public class LoginUserTokenArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/login_user_token
		public class LoginUserTokenResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars info/foo calls.
	namespace Info
	{

		// Arguments to info/get_bulk_player_info
		public class GetBulkPlayerInfoArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from info/get_bulk_player_info
		public class GetBulkPlayerInfoResponse
		{

		}

		// Arguments to info/ping
		public class PingArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from info/ping
		public class PingResponse
		{

		}

		// Arguments to info/user
		public class UserArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from info/user
		public class UserResponse
		{

		}

		// Arguments to info/poll
		public class PollArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from info/poll
		public class PollResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars items/foo calls.
	namespace Items
	{

		// Arguments to items/equip
		public class EquipArguments
		{
			public string item_id;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["item_id"] = item_id;
				return retval;
			}
		}
		
		// Response from items/equip
		public class EquipResponse
		{

		}

		// Arguments to items/list
		public class ListArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from items/list
		public class ListResponse
		{

		}

		// Arguments to items/sell
		public class SellArguments
		{
			public string item_id;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["item_id"] = item_id;
				return retval;
			}
		}
		
		// Response from items/sell
		public class SellResponse
		{

		}

		// Arguments to items/set
		public class SetArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from items/set
		public class SetResponse
		{

		}

		// Arguments to items/unequip
		public class UnequipArguments
		{
			public string item_id;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["item_id"] = item_id;
				return retval;
			}
		}
		
		// Response from items/unequip
		public class UnequipResponse
		{

		}

		// Arguments to items/use
		public class UseArguments
		{
			public string item_id;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["item_id"] = item_id;
				return retval;
			}
		}
		
		// Response from items/use
		public class UseResponse
		{

		}

		// Arguments to items/view
		public class ViewArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from items/view
		public class ViewResponse
		{

		}

		// Arguments to items/view_all
		public class ViewAllArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from items/view_all
		public class ViewAllResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars leaderboards/foo calls.
	namespace Leaderboards
	{

		// Arguments to leaderboards/list
		public class ListArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from leaderboards/list
		public class ListResponse
		{

		}

		// Arguments to leaderboards/view
		public class ViewArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from leaderboards/view
		public class ViewResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars mail/foo calls.
	namespace Mail
	{

		// Arguments to mail/accept
		public class AcceptArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from mail/accept
		public class AcceptResponse
		{

		}

		// Arguments to mail/send
		public class SendArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from mail/send
		public class SendResponse
		{

		}

		// Arguments to mail/what_can_i_accept
		public class WhatCanIAcceptArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from mail/what_can_i_accept
		public class WhatCanIAcceptResponse
		{

		}

		// Arguments to mail/what_can_i_send
		public class WhatCanISendArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from mail/what_can_i_send
		public class WhatCanISendResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars shop/foo calls.
	namespace Shop
	{

		// Arguments to shop/list
		public class ListArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from shop/list
		public class ListResponse
		{

		}

		// Arguments to shop/buy
		public class BuyArguments
		{
			public string shop_item_ikey;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["shop_item_ikey"] = shop_item_ikey;
				return retval;
			}
		}
		
		// Response from shop/buy
		public class BuyResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars scripts/foo calls.
	namespace Scripts
	{

		// Arguments to scripts/run
		public class RunArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from scripts/run
		public class RunResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars tasks/foo calls.
	namespace Tasks
	{

		// Arguments to tasks/list
		public class ListArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from tasks/list
		public class ListResponse
		{

		}

		// Arguments to tasks/start
		public class StartArguments
		{
			public string task_ikey;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["task_ikey"] = task_ikey;
				return retval;
			}
		}
		
		// Response from tasks/start
		public class StartResponse
		{

		}

	}

	//Namespace for typesafe arguments and responses to Roars user/foo calls.
	namespace User
	{

		// Arguments to user/achievements
		public class AchievementsArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from user/achievements
		public class AchievementsResponse
		{

		}

		// Arguments to user/change_name
		public class ChangeNameArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from user/change_name
		public class ChangeNameResponse
		{

		}

		// Arguments to user/change_password
		public class ChangePasswordArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from user/change_password
		public class ChangePasswordResponse
		{

		}

		// Arguments to user/create
		public class CreateArguments
		{
			public string name;
			public string hash;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["name"] = name;
				retval["hash"] = hash;
				return retval;
			}
		}
		
		// Response from user/create
		public class CreateResponse
		{

		}

		// Arguments to user/login
		public class LoginArguments
		{
			public string name;
			public string hash;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["name"] = name;
				retval["hash"] = hash;
				return retval;
			}
		}
		
		// Response from user/login
		public class LoginResponse
		{
			public string auth_token;
			public string player_id;

			public void ParseXml( IXMLNode nn )
			{
				auth_token = nn.GetNode("roar>0>user>0>login>0>auth_token>0").Text;
				player_id = nn.GetNode("roar>0>user>0>login>0>player_id>0").Text;
			}
		}

		// Arguments to user/login_facebook_oauth
		public class LoginFacebookOauthArguments
		{
			public string oauth_token;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["oauth_token"] = oauth_token;
				return retval;
			}
		}
		
		// Response from user/login_facebook_oauth
		public class LoginFacebookOauthResponse
		{

		}

		// Arguments to user/logout
		public class LogoutArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from user/logout
		public class LogoutResponse
		{

		}

		// Arguments to user/netdrive_save
		public class NetdriveSaveArguments
		{
			public string ikey;
			public string data;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["ikey"] = ikey;
				retval["data"] = data;
				return retval;
			}
		}
		
		// Response from user/netdrive_save
		public class NetdriveSaveResponse
		{

		}

		// Arguments to user/netdrive_fetch
		public class NetdriveFetchArguments
		{
			public string ikey;

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["ikey"] = ikey;
				return retval;
			}
		}
		
		// Response from user/netdrive_fetch
		public class NetdriveFetchResponse
		{
			public string data;

		}

		// Arguments to user/set
		public class SetArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from user/set
		public class SetResponse
		{

		}

		// Arguments to user/view
		public class ViewArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from user/view
		public class ViewResponse
		{
			public List<DomainObjects.PlayerAttribute> attributes;

		}

	}

	//Namespace for typesafe arguments and responses to Roars urbanairship/foo calls.
	namespace Urbanairship
	{

		// Arguments to urbanairship/ios_register
		public class IosRegisterArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from urbanairship/ios_register
		public class IosRegisterResponse
		{

		}

		// Arguments to urbanairship/push
		public class PushArguments
		{

			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from urbanairship/push
		public class PushResponse
		{

		}

	}

	
}

