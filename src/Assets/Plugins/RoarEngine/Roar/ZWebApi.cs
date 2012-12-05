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
using WebObjects = Roar.WebObjects;


public class ZWebAPI
{
	public IWebAPI iwebapi_;

	public ZWebAPI (IWebAPI iwebapi)
	{
		iwebapi_ = iwebapi;

		admin_ = new AdminActions (iwebapi.admin);
		appstore_ = new AppstoreActions (iwebapi.appstore);
		chrome_web_store_ = new Chrome_web_storeActions (iwebapi.chrome_web_store);
		facebook_ = new FacebookActions (iwebapi.facebook);
		friends_ = new FriendsActions (iwebapi.friends);
		google_ = new GoogleActions (iwebapi.google);
		info_ = new InfoActions (iwebapi.info);
		items_ = new ItemsActions (iwebapi.items);
		leaderboards_ = new LeaderboardsActions (iwebapi.leaderboards);
		mail_ = new MailActions (iwebapi.mail);
		shop_ = new ShopActions (iwebapi.shop);
		scripts_ = new ScriptsActions (iwebapi.scripts);
		tasks_ = new TasksActions (iwebapi.tasks);
		user_ = new UserActions (iwebapi.user);
		urbanairship_ = new UrbanairshipActions (iwebapi.urbanairship);
	}

	public AdminActions admin { get { return admin_; } }
	public AdminActions admin_;

	public AppstoreActions appstore { get { return appstore_; } }
	public AppstoreActions appstore_;

	public Chrome_web_storeActions chrome_web_store { get { return chrome_web_store_; } }
	public Chrome_web_storeActions chrome_web_store_;

	public FacebookActions facebook { get { return facebook_; } }
	public FacebookActions facebook_;

	public FriendsActions friends { get { return friends_; } }
	public FriendsActions friends_;

	public GoogleActions google { get { return google_; } }
	public GoogleActions google_;

	public InfoActions info { get { return info_; } }
	public InfoActions info_;

	public ItemsActions items { get { return items_; } }
	public ItemsActions items_;

	public LeaderboardsActions leaderboards { get { return leaderboards_; } }
	public LeaderboardsActions leaderboards_;

	public MailActions mail { get { return mail_; } }
	public MailActions mail_;

	public ShopActions shop { get { return shop_; } }
	public ShopActions shop_;

	public ScriptsActions scripts { get { return scripts_; } }
	public ScriptsActions scripts_;

	public TasksActions tasks { get { return tasks_; } }
	public TasksActions tasks_;

	public UserActions user { get { return user_; } }
	public UserActions user_;

	public UrbanairshipActions urbanairship { get { return urbanairship_; } }
	public UrbanairshipActions urbanairship_;



	public interface Callback<T>
	{
		void OnError( Roar.RequestResult nn );
		void OnSuccess( Roar.CallbackInfo<T> nn );
	}


	public class AdminActions 
	{
		public IWebAPI.IAdminActions actions_;

		public AdminActions (IWebAPI.IAdminActions actions)
		{
			actions_=actions;
		}

		public void delete_player (WebObjects.Admin.Delete_playerArguments args, Callback<WebObjects.Admin.Delete_playerResponse> cb)
		{
			actions_.delete_player(args,cb);
		}

		public void inrement_stat (WebObjects.Admin.Inrement_statArguments args, Callback<WebObjects.Admin.Inrement_statResponse> cb)
		{
			actions_.inrement_stat(args,cb);
		}

		public void _set (WebObjects.Admin.SetArguments args, Callback<WebObjects.Admin.SetResponse> cb)
		{
			actions_._set(args,cb);
		}

		public void set_custom (WebObjects.Admin.Set_customArguments args, Callback<WebObjects.Admin.Set_customResponse> cb)
		{
			actions_.set_custom(args,cb);
		}

		public void view_player (WebObjects.Admin.View_playerArguments args, Callback<WebObjects.Admin.View_playerResponse> cb)
		{
			actions_.view_player(args,cb);
		}

	}


	public class AppstoreActions 
	{
		public IWebAPI.IAppstoreActions actions_;

		public AppstoreActions (IWebAPI.IAppstoreActions actions)
		{
			actions_=actions;
		}

		public void buy (WebObjects.Appstore.BuyArguments args, Callback<WebObjects.Appstore.BuyResponse> cb)
		{
			actions_.buy(args,cb);
		}

		public void shop_list (WebObjects.Appstore.Shop_listArguments args, Callback<WebObjects.Appstore.Shop_listResponse> cb)
		{
			actions_.shop_list(args,cb);
		}

	}


	public class Chrome_web_storeActions 
	{
		public IWebAPI.IChrome_web_storeActions actions_;

		public Chrome_web_storeActions (IWebAPI.IChrome_web_storeActions actions)
		{
			actions_=actions;
		}

		public void list (WebObjects.Chrome_web_store.ListArguments args, Callback<WebObjects.Chrome_web_store.ListResponse> cb)
		{
			actions_.list(args,cb);
		}

	}


	public class FacebookActions 
	{
		public IWebAPI.IFacebookActions actions_;

		public FacebookActions (IWebAPI.IFacebookActions actions)
		{
			actions_=actions;
		}

		public void bind_signed (WebObjects.Facebook.Bind_signedArguments args, Callback<WebObjects.Facebook.Bind_signedResponse> cb)
		{
			actions_.bind_signed(args,cb);
		}

		public void create_oauth (WebObjects.Facebook.Create_oauthArguments args, Callback<WebObjects.Facebook.Create_oauthResponse> cb)
		{
			actions_.create_oauth(args,cb);
		}

		public void create_signed (WebObjects.Facebook.Create_signedArguments args, Callback<WebObjects.Facebook.Create_signedResponse> cb)
		{
			actions_.create_signed(args,cb);
		}

		public void fetch_oauth_token (WebObjects.Facebook.Fetch_oauth_tokenArguments args, Callback<WebObjects.Facebook.Fetch_oauth_tokenResponse> cb)
		{
			actions_.fetch_oauth_token(args,cb);
		}

		public void friends (WebObjects.Facebook.FriendsArguments args, Callback<WebObjects.Facebook.FriendsResponse> cb)
		{
			actions_.friends(args,cb);
		}

		public void login_oauth (WebObjects.Facebook.Login_oauthArguments args, Callback<WebObjects.Facebook.Login_oauthResponse> cb)
		{
			actions_.login_oauth(args,cb);
		}

		public void login_signed (WebObjects.Facebook.Login_signedArguments args, Callback<WebObjects.Facebook.Login_signedResponse> cb)
		{
			actions_.login_signed(args,cb);
		}

		public void shop_list (WebObjects.Facebook.Shop_listArguments args, Callback<WebObjects.Facebook.Shop_listResponse> cb)
		{
			actions_.shop_list(args,cb);
		}

	}


	public class FriendsActions 
	{
		public IWebAPI.IFriendsActions actions_;

		public FriendsActions (IWebAPI.IFriendsActions actions)
		{
			actions_=actions;
		}

		public void accept (WebObjects.Friends.AcceptArguments args, Callback<WebObjects.Friends.AcceptResponse> cb)
		{
			actions_.accept(args,cb);
		}

		public void decline (WebObjects.Friends.DeclineArguments args, Callback<WebObjects.Friends.DeclineResponse> cb)
		{
			actions_.decline(args,cb);
		}

		public void invite (WebObjects.Friends.InviteArguments args, Callback<WebObjects.Friends.InviteResponse> cb)
		{
			actions_.invite(args,cb);
		}

		public void invite_info (WebObjects.Friends.Invite_infoArguments args, Callback<WebObjects.Friends.Invite_infoResponse> cb)
		{
			actions_.invite_info(args,cb);
		}

		public void list (WebObjects.Friends.ListArguments args, Callback<WebObjects.Friends.ListResponse> cb)
		{
			actions_.list(args,cb);
		}

		public void remove (WebObjects.Friends.RemoveArguments args, Callback<WebObjects.Friends.RemoveResponse> cb)
		{
			actions_.remove(args,cb);
		}

	}


	public class GoogleActions 
	{
		public IWebAPI.IGoogleActions actions_;

		public GoogleActions (IWebAPI.IGoogleActions actions)
		{
			actions_=actions;
		}

		public void bind_user (WebObjects.Google.Bind_userArguments args, Callback<WebObjects.Google.Bind_userResponse> cb)
		{
			actions_.bind_user(args,cb);
		}

		public void bind_user_token (WebObjects.Google.Bind_user_tokenArguments args, Callback<WebObjects.Google.Bind_user_tokenResponse> cb)
		{
			actions_.bind_user_token(args,cb);
		}

		public void create_user (WebObjects.Google.Create_userArguments args, Callback<WebObjects.Google.Create_userResponse> cb)
		{
			actions_.create_user(args,cb);
		}

		public void create_user_token (WebObjects.Google.Create_user_tokenArguments args, Callback<WebObjects.Google.Create_user_tokenResponse> cb)
		{
			actions_.create_user_token(args,cb);
		}

		public void friends (WebObjects.Google.FriendsArguments args, Callback<WebObjects.Google.FriendsResponse> cb)
		{
			actions_.friends(args,cb);
		}

		public void login_user (WebObjects.Google.Login_userArguments args, Callback<WebObjects.Google.Login_userResponse> cb)
		{
			actions_.login_user(args,cb);
		}

		public void login_user_token (WebObjects.Google.Login_user_tokenArguments args, Callback<WebObjects.Google.Login_user_tokenResponse> cb)
		{
			actions_.login_user_token(args,cb);
		}

	}


	public class InfoActions 
	{
		public IWebAPI.IInfoActions actions_;

		public InfoActions (IWebAPI.IInfoActions actions)
		{
			actions_=actions;
		}

		public void get_bulk_player_info (WebObjects.Info.Get_bulk_player_infoArguments args, Callback<WebObjects.Info.Get_bulk_player_infoResponse> cb)
		{
			actions_.get_bulk_player_info(args,cb);
		}

		public void ping (WebObjects.Info.PingArguments args, Callback<WebObjects.Info.PingResponse> cb)
		{
			actions_.ping(args,cb);
		}

		public void user (WebObjects.Info.UserArguments args, Callback<WebObjects.Info.UserResponse> cb)
		{
			actions_.user(args,cb);
		}

		public void poll (WebObjects.Info.PollArguments args, Callback<WebObjects.Info.PollResponse> cb)
		{
			actions_.poll(args,cb);
		}

	}


	public class ItemsActions 
	{
		public IWebAPI.IItemsActions actions_;

		public ItemsActions (IWebAPI.IItemsActions actions)
		{
			actions_=actions;
		}

		public void equip (WebObjects.Items.EquipArguments args, Callback<WebObjects.Items.EquipResponse> cb)
		{
			actions_.equip(args,cb);
		}

		public void list (WebObjects.Items.ListArguments args, Callback<WebObjects.Items.ListResponse> cb)
		{
			actions_.list(args,cb);
		}

		public void sell (WebObjects.Items.SellArguments args, Callback<WebObjects.Items.SellResponse> cb)
		{
			actions_.sell(args,cb);
		}

		public void _set (WebObjects.Items.SetArguments args, Callback<WebObjects.Items.SetResponse> cb)
		{
			actions_._set(args,cb);
		}

		public void unequip (WebObjects.Items.UnequipArguments args, Callback<WebObjects.Items.UnequipResponse> cb)
		{
			actions_.unequip(args,cb);
		}

		public void use (WebObjects.Items.UseArguments args, Callback<WebObjects.Items.UseResponse> cb)
		{
			actions_.use(args,cb);
		}

		public void view (WebObjects.Items.ViewArguments args, Callback<WebObjects.Items.ViewResponse> cb)
		{
			actions_.view(args,cb);
		}

		public void view_all (WebObjects.Items.View_allArguments args, Callback<WebObjects.Items.View_allResponse> cb)
		{
			actions_.view_all(args,cb);
		}

	}


	public class LeaderboardsActions 
	{
		public IWebAPI.ILeaderboardsActions actions_;

		public LeaderboardsActions (IWebAPI.ILeaderboardsActions actions)
		{
			actions_=actions;
		}

		public void list (WebObjects.Leaderboards.ListArguments args, Callback<WebObjects.Leaderboards.ListResponse> cb)
		{
			actions_.list(args,cb);
		}

		public void view (WebObjects.Leaderboards.ViewArguments args, Callback<WebObjects.Leaderboards.ViewResponse> cb)
		{
			actions_.view(args,cb);
		}

	}


	public class MailActions 
	{
		public IWebAPI.IMailActions actions_;

		public MailActions (IWebAPI.IMailActions actions)
		{
			actions_=actions;
		}

		public void accept (WebObjects.Mail.AcceptArguments args, Callback<WebObjects.Mail.AcceptResponse> cb)
		{
			actions_.accept(args,cb);
		}

		public void send (WebObjects.Mail.SendArguments args, Callback<WebObjects.Mail.SendResponse> cb)
		{
			actions_.send(args,cb);
		}

		public void what_can_i_accept (WebObjects.Mail.What_can_i_acceptArguments args, Callback<WebObjects.Mail.What_can_i_acceptResponse> cb)
		{
			actions_.what_can_i_accept(args,cb);
		}

		public void what_can_i_send (WebObjects.Mail.What_can_i_sendArguments args, Callback<WebObjects.Mail.What_can_i_sendResponse> cb)
		{
			actions_.what_can_i_send(args,cb);
		}

	}


	public class ShopActions 
	{
		public IWebAPI.IShopActions actions_;

		public ShopActions (IWebAPI.IShopActions actions)
		{
			actions_=actions;
		}

		public void list (WebObjects.Shop.ListArguments args, Callback<WebObjects.Shop.ListResponse> cb)
		{
			actions_.list(args,cb);
		}

		public void buy (WebObjects.Shop.BuyArguments args, Callback<WebObjects.Shop.BuyResponse> cb)
		{
			actions_.buy(args,cb);
		}

	}


	public class ScriptsActions 
	{
		public IWebAPI.IScriptsActions actions_;

		public ScriptsActions (IWebAPI.IScriptsActions actions)
		{
			actions_=actions;
		}

		public void run (WebObjects.Scripts.RunArguments args, Callback<WebObjects.Scripts.RunResponse> cb)
		{
			actions_.run(args,cb);
		}

	}


	public class TasksActions 
	{
		public IWebAPI.ITasksActions actions_;

		public TasksActions (IWebAPI.ITasksActions actions)
		{
			actions_=actions;
		}

		public void list (WebObjects.Tasks.ListArguments args, Callback<WebObjects.Tasks.ListResponse> cb)
		{
			actions_.list(args,cb);
		}

		public void start (WebObjects.Tasks.StartArguments args, Callback<WebObjects.Tasks.StartResponse> cb)
		{
			actions_.start(args,cb);
		}

	}


	public class UserActions 
	{
		public IWebAPI.IUserActions actions_;

		public UserActions (IWebAPI.IUserActions actions)
		{
			actions_=actions;
		}

		public void achievements (WebObjects.User.AchievementsArguments args, Callback<WebObjects.User.AchievementsResponse> cb)
		{
			actions_.achievements(args,cb);
		}

		public void change_name (WebObjects.User.Change_nameArguments args, Callback<WebObjects.User.Change_nameResponse> cb)
		{
			actions_.change_name(args,cb);
		}

		public void change_password (WebObjects.User.Change_passwordArguments args, Callback<WebObjects.User.Change_passwordResponse> cb)
		{
			actions_.change_password(args,cb);
		}

		public void create (WebObjects.User.CreateArguments args, Callback<WebObjects.User.CreateResponse> cb)
		{
			actions_.create(args,cb);
		}

		public void login (WebObjects.User.LoginArguments args, Callback<WebObjects.User.LoginResponse> cb)
		{
			actions_.login(args,cb);
		}

		public void login_facebook_oauth (WebObjects.User.Login_facebook_oauthArguments args, Callback<WebObjects.User.Login_facebook_oauthResponse> cb)
		{
			actions_.login_facebook_oauth(args,cb);
		}

		public void logout (WebObjects.User.LogoutArguments args, Callback<WebObjects.User.LogoutResponse> cb)
		{
			actions_.logout(args,cb);
		}

		public void netdrive_save (WebObjects.User.Netdrive_saveArguments args, Callback<WebObjects.User.Netdrive_saveResponse> cb)
		{
			actions_.netdrive_save(args,cb);
		}

		public void netdrive_fetch (WebObjects.User.Netdrive_fetchArguments args, Callback<WebObjects.User.Netdrive_fetchResponse> cb)
		{
			actions_.netdrive_fetch(args,cb);
		}

		public void _set (WebObjects.User.SetArguments args, Callback<WebObjects.User.SetResponse> cb)
		{
			actions_._set(args,cb);
		}

		public void view (WebObjects.User.ViewArguments args, Callback<WebObjects.User.ViewResponse> cb)
		{
			actions_.view(args,cb);
		}

	}


	public class UrbanairshipActions 
	{
		public IWebAPI.IUrbanairshipActions actions_;

		public UrbanairshipActions (IWebAPI.IUrbanairshipActions actions)
		{
			actions_=actions;
		}

		public void ios_register (WebObjects.Urbanairship.Ios_registerArguments args, Callback<WebObjects.Urbanairship.Ios_registerResponse> cb)
		{
			actions_.ios_register(args,cb);
		}

		public void push (WebObjects.Urbanairship.PushArguments args, Callback<WebObjects.Urbanairship.PushResponse> cb)
		{
			actions_.push(args,cb);
		}

	}



}

