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

public abstract class IWebAPI
{
	public abstract IAdminActions admin { get; }
	public abstract IAppstoreActions appstore { get; }
	public abstract IChrome_web_storeActions chrome_web_store { get; }
	public abstract IFacebookActions facebook { get; }
	public abstract IFriendsActions friends { get; }
	public abstract IGoogleActions google { get; }
	public abstract IInfoActions info { get; }
	public abstract IItemsActions items { get; }
	public abstract ILeaderboardsActions leaderboards { get; }
	public abstract IMailActions mail { get; }
	public abstract IShopActions shop { get; }
	public abstract IScriptsActions scripts { get; }
	public abstract ITasksActions tasks { get; }
	public abstract IUserActions user { get; }
	public abstract IUrbanairshipActions urbanairship { get; }


	public const int UNKNOWN_ERR  = 0;    // Default unspecified error (parse manually)
	public const int UNAUTHORIZED = 1;    // Auth token is no longer valid. Relogin.
	public const int BAD_INPUTS   = 2;    // Incorrect parameters passed to Roar
	public const int DISALLOWED   = 3;    // Action was not allowed (but otherwise successful)
	public const int FATAL_ERROR  = 4;    // Server died somehow (sad/bad/etc)
	public const int AWESOME      = 11;   // Turn it up.
	public const int OK           = 200;  // Everything ok - proceed


	public interface IAdminActions
	{
		void delete_player( Roar.WebObjects.Admin.Delete_playerArguments args, ZWebAPI.Callback<Roar.WebObjects.Admin.Delete_playerResponse> cb);
		void inrement_stat( Roar.WebObjects.Admin.Inrement_statArguments args, ZWebAPI.Callback<Roar.WebObjects.Admin.Inrement_statResponse> cb);
		void _set( Roar.WebObjects.Admin.SetArguments args, ZWebAPI.Callback<Roar.WebObjects.Admin.SetResponse> cb);
		void set_custom( Roar.WebObjects.Admin.Set_customArguments args, ZWebAPI.Callback<Roar.WebObjects.Admin.Set_customResponse> cb);
		void view_player( Roar.WebObjects.Admin.View_playerArguments args, ZWebAPI.Callback<Roar.WebObjects.Admin.View_playerResponse> cb);
	}

	public interface IAppstoreActions
	{
		void buy( Roar.WebObjects.Appstore.BuyArguments args, ZWebAPI.Callback<Roar.WebObjects.Appstore.BuyResponse> cb);
		void shop_list( Roar.WebObjects.Appstore.Shop_listArguments args, ZWebAPI.Callback<Roar.WebObjects.Appstore.Shop_listResponse> cb);
	}

	public interface IChrome_web_storeActions
	{
		void list( Roar.WebObjects.Chrome_web_store.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Chrome_web_store.ListResponse> cb);
	}

	public interface IFacebookActions
	{
		void bind_signed( Roar.WebObjects.Facebook.Bind_signedArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.Bind_signedResponse> cb);
		void create_oauth( Roar.WebObjects.Facebook.Create_oauthArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.Create_oauthResponse> cb);
		void create_signed( Roar.WebObjects.Facebook.Create_signedArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.Create_signedResponse> cb);
		void fetch_oauth_token( Roar.WebObjects.Facebook.Fetch_oauth_tokenArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.Fetch_oauth_tokenResponse> cb);
		void friends( Roar.WebObjects.Facebook.FriendsArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.FriendsResponse> cb);
		void login_oauth( Roar.WebObjects.Facebook.Login_oauthArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.Login_oauthResponse> cb);
		void login_signed( Roar.WebObjects.Facebook.Login_signedArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.Login_signedResponse> cb);
		void shop_list( Roar.WebObjects.Facebook.Shop_listArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.Shop_listResponse> cb);
	}

	public interface IFriendsActions
	{
		void accept( Roar.WebObjects.Friends.AcceptArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.AcceptResponse> cb);
		void decline( Roar.WebObjects.Friends.DeclineArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.DeclineResponse> cb);
		void invite( Roar.WebObjects.Friends.InviteArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.InviteResponse> cb);
		void invite_info( Roar.WebObjects.Friends.Invite_infoArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.Invite_infoResponse> cb);
		void list( Roar.WebObjects.Friends.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.ListResponse> cb);
		void remove( Roar.WebObjects.Friends.RemoveArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.RemoveResponse> cb);
	}

	public interface IGoogleActions
	{
		void bind_user( Roar.WebObjects.Google.Bind_userArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.Bind_userResponse> cb);
		void bind_user_token( Roar.WebObjects.Google.Bind_user_tokenArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.Bind_user_tokenResponse> cb);
		void create_user( Roar.WebObjects.Google.Create_userArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.Create_userResponse> cb);
		void create_user_token( Roar.WebObjects.Google.Create_user_tokenArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.Create_user_tokenResponse> cb);
		void friends( Roar.WebObjects.Google.FriendsArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.FriendsResponse> cb);
		void login_user( Roar.WebObjects.Google.Login_userArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.Login_userResponse> cb);
		void login_user_token( Roar.WebObjects.Google.Login_user_tokenArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.Login_user_tokenResponse> cb);
	}

	public interface IInfoActions
	{
		void get_bulk_player_info( Roar.WebObjects.Info.Get_bulk_player_infoArguments args, ZWebAPI.Callback<Roar.WebObjects.Info.Get_bulk_player_infoResponse> cb);
		void ping( Roar.WebObjects.Info.PingArguments args, ZWebAPI.Callback<Roar.WebObjects.Info.PingResponse> cb);
		void user( Roar.WebObjects.Info.UserArguments args, ZWebAPI.Callback<Roar.WebObjects.Info.UserResponse> cb);
		void poll( Roar.WebObjects.Info.PollArguments args, ZWebAPI.Callback<Roar.WebObjects.Info.PollResponse> cb);
	}

	public interface IItemsActions
	{
		void equip( Roar.WebObjects.Items.EquipArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.EquipResponse> cb);
		void list( Roar.WebObjects.Items.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.ListResponse> cb);
		void sell( Roar.WebObjects.Items.SellArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.SellResponse> cb);
		void _set( Roar.WebObjects.Items.SetArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.SetResponse> cb);
		void unequip( Roar.WebObjects.Items.UnequipArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.UnequipResponse> cb);
		void use( Roar.WebObjects.Items.UseArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.UseResponse> cb);
		void view( Roar.WebObjects.Items.ViewArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.ViewResponse> cb);
		void view_all( Roar.WebObjects.Items.View_allArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.View_allResponse> cb);
	}

	public interface ILeaderboardsActions
	{
		void list( Roar.WebObjects.Leaderboards.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Leaderboards.ListResponse> cb);
		void view( Roar.WebObjects.Leaderboards.ViewArguments args, ZWebAPI.Callback<Roar.WebObjects.Leaderboards.ViewResponse> cb);
	}

	public interface IMailActions
	{
		void accept( Roar.WebObjects.Mail.AcceptArguments args, ZWebAPI.Callback<Roar.WebObjects.Mail.AcceptResponse> cb);
		void send( Roar.WebObjects.Mail.SendArguments args, ZWebAPI.Callback<Roar.WebObjects.Mail.SendResponse> cb);
		void what_can_i_accept( Roar.WebObjects.Mail.What_can_i_acceptArguments args, ZWebAPI.Callback<Roar.WebObjects.Mail.What_can_i_acceptResponse> cb);
		void what_can_i_send( Roar.WebObjects.Mail.What_can_i_sendArguments args, ZWebAPI.Callback<Roar.WebObjects.Mail.What_can_i_sendResponse> cb);
	}

	public interface IShopActions
	{
		void list( Roar.WebObjects.Shop.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Shop.ListResponse> cb);
		void buy( Roar.WebObjects.Shop.BuyArguments args, ZWebAPI.Callback<Roar.WebObjects.Shop.BuyResponse> cb);
	}

	public interface IScriptsActions
	{
		void run( Roar.WebObjects.Scripts.RunArguments args, ZWebAPI.Callback<Roar.WebObjects.Scripts.RunResponse> cb);
	}

	public interface ITasksActions
	{
		void list( Roar.WebObjects.Tasks.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Tasks.ListResponse> cb);
		void start( Roar.WebObjects.Tasks.StartArguments args, ZWebAPI.Callback<Roar.WebObjects.Tasks.StartResponse> cb);
	}

	public interface IUserActions
	{
		void achievements( Roar.WebObjects.User.AchievementsArguments args, ZWebAPI.Callback<Roar.WebObjects.User.AchievementsResponse> cb);
		void change_name( Roar.WebObjects.User.Change_nameArguments args, ZWebAPI.Callback<Roar.WebObjects.User.Change_nameResponse> cb);
		void change_password( Roar.WebObjects.User.Change_passwordArguments args, ZWebAPI.Callback<Roar.WebObjects.User.Change_passwordResponse> cb);
		void create( Roar.WebObjects.User.CreateArguments args, ZWebAPI.Callback<Roar.WebObjects.User.CreateResponse> cb);
		void login( Roar.WebObjects.User.LoginArguments args, ZWebAPI.Callback<Roar.WebObjects.User.LoginResponse> cb);
		void login_facebook_oauth( Roar.WebObjects.User.Login_facebook_oauthArguments args, ZWebAPI.Callback<Roar.WebObjects.User.Login_facebook_oauthResponse> cb);
		void logout( Roar.WebObjects.User.LogoutArguments args, ZWebAPI.Callback<Roar.WebObjects.User.LogoutResponse> cb);
		void netdrive_save( Roar.WebObjects.User.Netdrive_saveArguments args, ZWebAPI.Callback<Roar.WebObjects.User.Netdrive_saveResponse> cb);
		void netdrive_fetch( Roar.WebObjects.User.Netdrive_fetchArguments args, ZWebAPI.Callback<Roar.WebObjects.User.Netdrive_fetchResponse> cb);
		void _set( Roar.WebObjects.User.SetArguments args, ZWebAPI.Callback<Roar.WebObjects.User.SetResponse> cb);
		void view( Roar.WebObjects.User.ViewArguments args, ZWebAPI.Callback<Roar.WebObjects.User.ViewResponse> cb);
	}

	public interface IUrbanairshipActions
	{
		void ios_register( Roar.WebObjects.Urbanairship.Ios_registerArguments args, ZWebAPI.Callback<Roar.WebObjects.Urbanairship.Ios_registerResponse> cb);
		void push( Roar.WebObjects.Urbanairship.PushArguments args, ZWebAPI.Callback<Roar.WebObjects.Urbanairship.PushResponse> cb);
	}

}

