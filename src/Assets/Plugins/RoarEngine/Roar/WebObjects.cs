using System;
using System.Collections;
using System.Collections.Generic;


namespace Roar.WebObjects
{
	public interface IResponse
	{
		void ParseXml( IXMLNode nn );
	}


	//Namespace for typesafe arguments and responses to Roars admin/foo calls.
	namespace Admin
	{

		// Arguments to admin/delete_player
		public class Delete_playerArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from admin/delete_player
		public class Delete_playerResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to admin/inrement_stat
		public class Inrement_statArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from admin/inrement_stat
		public class Inrement_statResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class SetResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to admin/set_custom
		public class Set_customArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from admin/set_custom
		public class Set_customResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to admin/view_player
		public class View_playerArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from admin/view_player
		public class View_playerResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
				retval ["receipt"] = receipt;
				retval ["sandbox"] = Convert.ToString (sandbox);
				return retval;
			}
		}
		
		// Response from appstore/buy
		public class BuyResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to appstore/shop_list
		public class Shop_listArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from appstore/shop_list
		public class Shop_listResponse : IResponse
		{
			public List<string> productIdentifiers;
			
			public void ParseXml( IXMLNode nn )
			{
				productIdentifiers = new List<string>();
				
				// extract the product identifiers from the xml
				string path = "roar>0>appstore>0>shop_list>0>shopitem";
				List<IXMLNode> products = nn.GetNodeList (path);
				if (products == null) {
					return;
					// TODO: Reinstate some logging here
					// logger.DebugLog (string.Format ("data.GetNodeList('{0}') return null", path));
				}
				foreach (IXMLNode product in products)
				{
					string pid = product.GetAttribute ("product_identifier");
					if (!string.IsNullOrEmpty (pid)) {
						productIdentifiers.Add (pid);
					}
				}
			}
		}

	}

	//Namespace for typesafe arguments and responses to Roars chrome_web_store/foo calls.
	namespace Chrome_web_store
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
		public class ListResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

	}

	//Namespace for typesafe arguments and responses to Roars facebook/foo calls.
	namespace Facebook
	{

		// Arguments to facebook/bind_signed
		public class Bind_signedArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/bind_signed
		public class Bind_signedResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to facebook/create_oauth
		public class Create_oauthArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/create_oauth
		public class Create_oauthResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to facebook/create_signed
		public class Create_signedArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/create_signed
		public class Create_signedResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to facebook/fetch_oauth_token
		public class Fetch_oauth_tokenArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/fetch_oauth_token
		public class Fetch_oauth_tokenResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class FriendsResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to facebook/login_oauth
		public class Login_oauthArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/login_oauth
		public class Login_oauthResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to facebook/login_signed
		public class Login_signedArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/login_signed
		public class Login_signedResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to facebook/shop_list
		public class Shop_listArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from facebook/shop_list
		public class Shop_listResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

	}

	//Namespace for typesafe arguments and responses to Roars friends/foo calls.
	namespace Friends
	{

		// Arguments to friends/accept
		public class AcceptArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from friends/accept
		public class AcceptResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to friends/decline
		public class DeclineArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from friends/decline
		public class DeclineResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to friends/invite
		public class InviteArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from friends/invite
		public class InviteResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to friends/invite_info
		public class Invite_infoArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from friends/invite_info
		public class Invite_infoResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		
		// Response from friends/list
		public class ListResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to friends/remove
		public class RemoveArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from friends/remove
		public class RemoveResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

	}

	//Namespace for typesafe arguments and responses to Roars google/foo calls.
	namespace Google
	{

		// Arguments to google/bind_user
		public class Bind_userArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/bind_user
		public class Bind_userResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to google/bind_user_token
		public class Bind_user_tokenArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/bind_user_token
		public class Bind_user_tokenResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to google/create_user
		public class Create_userArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/create_user
		public class Create_userResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to google/create_user_token
		public class Create_user_tokenArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/create_user_token
		public class Create_user_tokenResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class FriendsResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to google/login_user
		public class Login_userArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/login_user
		public class Login_userResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to google/login_user_token
		public class Login_user_tokenArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from google/login_user_token
		public class Login_user_tokenResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

	}

	//Namespace for typesafe arguments and responses to Roars info/foo calls.
	namespace Info
	{

		// Arguments to info/get_bulk_player_info
		public class Get_bulk_player_infoArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from info/get_bulk_player_info
		public class Get_bulk_player_infoResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class PingResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class UserResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class PollResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
				retval ["item_id"] = item_id;
				return retval;
			}
		}
		
		// Response from items/equip
		public class EquipResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class ListResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to items/sell
		public class SellArguments
		{
			public string item_id;
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval ["item_id"] = item_id;
				return retval;
			}
		}
		
		// Response from items/sell
		public class SellResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class SetResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to items/unequip
		public class UnequipArguments
		{
			public string item_id;
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval ["item_id"] = item_id;
				return retval;
			}
		}
		
		// Response from items/unequip
		public class UnequipResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to items/use
		public class UseArguments
		{
			public string item_id;
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval ["item_id"] = item_id;
				return retval;
			}
		}
		
		// Response from items/use
		public class UseResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class ViewResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to items/view_all
		public class View_allArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from items/view_all
		public class View_allResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class ListResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class ViewResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class AcceptResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class SendResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to mail/what_can_i_accept
		public class What_can_i_acceptArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from mail/what_can_i_accept
		public class What_can_i_acceptResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to mail/what_can_i_send
		public class What_can_i_sendArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from mail/what_can_i_send
		public class What_can_i_sendResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class ListResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class BuyResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class RunResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class ListResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to tasks/start
		public class StartArguments
		{
			public string ikey;
			
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["task_ikey"] = ikey;
				return retval;
			}
		}
		
		// Response from tasks/start
		public class StartResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class AchievementsResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to user/change_name
		public class Change_nameArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from user/change_name
		public class Change_nameResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to user/change_password
		public class Change_passwordArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from user/change_password
		public class Change_passwordResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to user/create
		public class CreateArguments
		{
			public string name;
			public string hash;
			
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["name"]=name;
				retval["hash"]=hash;
				return retval;
			}
		}
		
		// Response from user/create
		public class CreateResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to user/login
		public class LoginArguments
		{
			public string name;
			public string hash;
			
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["name"]=name;
				retval["hash"]=hash;
				return retval;
			}
		}
		
		// Response from user/login
		public class LoginResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to user/login_facebook_oauth
		public class Login_facebook_oauthArguments
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
		public class Login_facebook_oauthResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class LogoutResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to user/netdrive_save
		public class Netdrive_saveArguments
		{
			public string ikey;
			public string data;
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["ikey"]=ikey;
				retval["data"]=data;
				return retval;
			}
		}
		
		// Response from user/netdrive_save
		public class Netdrive_saveResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

		// Arguments to user/netdrive_fetch
		public class Netdrive_fetchArguments
		{
			public string ikey;
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				retval["ikey"]=ikey;
				return retval;
			}
		}
		
		// Response from user/netdrive_fetch
		public class Netdrive_fetchResponse : IResponse
		{
			public string data;
			public void ParseXml( IXMLNode nn )
			{
				//TODO: Implement me!
			}
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
		public class SetResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class ViewResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

	}

	//Namespace for typesafe arguments and responses to Roars urbanairship/foo calls.
	namespace Urbanairship
	{

		// Arguments to urbanairship/ios_register
		public class Ios_registerArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from urbanairship/ios_register
		public class Ios_registerResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
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
		public class PushResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}

	}

	
}

