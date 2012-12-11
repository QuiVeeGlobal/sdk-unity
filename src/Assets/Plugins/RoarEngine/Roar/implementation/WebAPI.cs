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
using System.Collections.Generic;
using UnityEngine;

public class WebAPI : IWebAPI
{
	protected IRequestSender requestSender_;

	public WebAPI (IRequestSender requestSender)
	{
		requestSender_ = requestSender;

		admin_ = new AdminActions (requestSender);
		appstore_ = new AppstoreActions (requestSender);
		chrome_web_store_ = new ChromeWebStoreActions (requestSender);
		facebook_ = new FacebookActions (requestSender);
		friends_ = new FriendsActions (requestSender);
		google_ = new GoogleActions (requestSender);
		info_ = new InfoActions (requestSender);
		items_ = new ItemsActions (requestSender);
		leaderboards_ = new LeaderboardsActions (requestSender);
		mail_ = new MailActions (requestSender);
		shop_ = new ShopActions (requestSender);
		scripts_ = new ScriptsActions (requestSender);
		tasks_ = new TasksActions (requestSender);
		user_ = new UserActions (requestSender);
		urbanairship_ = new UrbanairshipActions (requestSender);
	}

	public override IAdminActions admin { get { return admin_; } }

	public AdminActions admin_;

	public override IAppstoreActions appstore { get { return appstore_; } }

	public AppstoreActions appstore_;

	public override IChromeWebStoreActions chrome_web_store { get { return chrome_web_store_; } }

	public ChromeWebStoreActions chrome_web_store_;

	public override IFacebookActions facebook { get { return facebook_; } }

	public FacebookActions facebook_;

	public override IFriendsActions friends { get { return friends_; } }

	public FriendsActions friends_;

	public override IGoogleActions google { get { return google_; } }

	public GoogleActions google_;

	public override IInfoActions info { get { return info_; } }

	public InfoActions info_;

	public override IItemsActions items { get { return items_; } }

	public ItemsActions items_;

	public override ILeaderboardsActions leaderboards { get { return leaderboards_; } }

	public LeaderboardsActions leaderboards_;

	public override IMailActions mail { get { return mail_; } }

	public MailActions mail_;

	public override IShopActions shop { get { return shop_; } }

	public ShopActions shop_;

	public override IScriptsActions scripts { get { return scripts_; } }

	public ScriptsActions scripts_;

	public override ITasksActions tasks { get { return tasks_; } }

	public TasksActions tasks_;

	public override IUserActions user { get { return user_; } }

	public UserActions user_;

	public override IUrbanairshipActions urbanairship { get { return urbanairship_; } }

	public UrbanairshipActions urbanairship_;



	public class APIBridge
	{
		protected IRequestSender api;

		public APIBridge (IRequestSender caller)
		{
			api = caller;
		}
	}

	public class AdminActions : APIBridge, IAdminActions
	{
		public AdminActions (IRequestSender caller) : base(caller)
		{
		}

		public void delete_player( Roar.WebObjects.Admin.DeletePlayerArguments args, ZWebAPI.Callback<Roar.WebObjects.Admin.DeletePlayerResponse> cb)
		{
			api.MakeCall ("admin/delete_player", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Admin.DeletePlayerResponse>(cb));
		}

		public void increment_stat( Roar.WebObjects.Admin.IncrementStatArguments args, ZWebAPI.Callback<Roar.WebObjects.Admin.IncrementStatResponse> cb)
		{
			api.MakeCall ("admin/increment_stat", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Admin.IncrementStatResponse>(cb));
		}

		public void _set( Roar.WebObjects.Admin.SetArguments args, ZWebAPI.Callback<Roar.WebObjects.Admin.SetResponse> cb)
		{
			api.MakeCall ("admin/set", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Admin.SetResponse>(cb));
		}

		public void set_custom( Roar.WebObjects.Admin.SetCustomArguments args, ZWebAPI.Callback<Roar.WebObjects.Admin.SetCustomResponse> cb)
		{
			api.MakeCall ("admin/set_custom", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Admin.SetCustomResponse>(cb));
		}

		public void view_player( Roar.WebObjects.Admin.ViewPlayerArguments args, ZWebAPI.Callback<Roar.WebObjects.Admin.ViewPlayerResponse> cb)
		{
			api.MakeCall ("admin/view_player", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Admin.ViewPlayerResponse>(cb));
		}

	}

	public class AppstoreActions : APIBridge, IAppstoreActions
	{
		public AppstoreActions (IRequestSender caller) : base(caller)
		{
		}

		public void buy( Roar.WebObjects.Appstore.BuyArguments args, ZWebAPI.Callback<Roar.WebObjects.Appstore.BuyResponse> cb)
		{
			api.MakeCall ("appstore/buy", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Appstore.BuyResponse>(cb));
		}

		public void shop_list( Roar.WebObjects.Appstore.ShopListArguments args, ZWebAPI.Callback<Roar.WebObjects.Appstore.ShopListResponse> cb)
		{
			api.MakeCall ("appstore/shop_list", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Appstore.ShopListResponse>(cb));
		}

	}

	public class ChromeWebStoreActions : APIBridge, IChromeWebStoreActions
	{
		public ChromeWebStoreActions (IRequestSender caller) : base(caller)
		{
		}

		public void list( Roar.WebObjects.ChromeWebStore.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.ChromeWebStore.ListResponse> cb)
		{
			api.MakeCall ("chrome_web_store/list", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.ChromeWebStore.ListResponse>(cb));
		}

	}

	public class FacebookActions : APIBridge, IFacebookActions
	{
		public FacebookActions (IRequestSender caller) : base(caller)
		{
		}

		public void bind_signed( Roar.WebObjects.Facebook.BindSignedArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.BindSignedResponse> cb)
		{
			api.MakeCall ("facebook/bind_signed", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Facebook.BindSignedResponse>(cb));
		}

		public void create_oauth( Roar.WebObjects.Facebook.CreateOauthArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.CreateOauthResponse> cb)
		{
			api.MakeCall ("facebook/create_oauth", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Facebook.CreateOauthResponse>(cb));
		}

		public void create_signed( Roar.WebObjects.Facebook.CreateSignedArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.CreateSignedResponse> cb)
		{
			api.MakeCall ("facebook/create_signed", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Facebook.CreateSignedResponse>(cb));
		}

		public void fetch_oauth_token( Roar.WebObjects.Facebook.FetchOauthTokenArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.FetchOauthTokenResponse> cb)
		{
			api.MakeCall ("facebook/fetch_oauth_token", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Facebook.FetchOauthTokenResponse>(cb));
		}

		public void friends( Roar.WebObjects.Facebook.FriendsArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.FriendsResponse> cb)
		{
			api.MakeCall ("facebook/friends", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Facebook.FriendsResponse>(cb));
		}

		public void login_oauth( Roar.WebObjects.Facebook.LoginOauthArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.LoginOauthResponse> cb)
		{
			api.MakeCall ("facebook/login_oauth", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Facebook.LoginOauthResponse>(cb));
		}

		public void login_signed( Roar.WebObjects.Facebook.LoginSignedArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.LoginSignedResponse> cb)
		{
			api.MakeCall ("facebook/login_signed", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Facebook.LoginSignedResponse>(cb));
		}

		public void shop_list( Roar.WebObjects.Facebook.ShopListArguments args, ZWebAPI.Callback<Roar.WebObjects.Facebook.ShopListResponse> cb)
		{
			api.MakeCall ("facebook/shop_list", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Facebook.ShopListResponse>(cb));
		}

	}

	public class FriendsActions : APIBridge, IFriendsActions
	{
		public FriendsActions (IRequestSender caller) : base(caller)
		{
		}

		public void accept( Roar.WebObjects.Friends.AcceptArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.AcceptResponse> cb)
		{
			api.MakeCall ("friends/accept", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Friends.AcceptResponse>(cb));
		}

		public void decline( Roar.WebObjects.Friends.DeclineArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.DeclineResponse> cb)
		{
			api.MakeCall ("friends/decline", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Friends.DeclineResponse>(cb));
		}

		public void invite( Roar.WebObjects.Friends.InviteArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.InviteResponse> cb)
		{
			api.MakeCall ("friends/invite", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Friends.InviteResponse>(cb));
		}

		public void invite_info( Roar.WebObjects.Friends.InviteInfoArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.InviteInfoResponse> cb)
		{
			api.MakeCall ("friends/info", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Friends.InviteInfoResponse>(cb));
		}

		public void list( Roar.WebObjects.Friends.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.ListResponse> cb)
		{
			api.MakeCall ("friends/list", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Friends.ListResponse>(cb));
		}

		public void remove( Roar.WebObjects.Friends.RemoveArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.RemoveResponse> cb)
		{
			api.MakeCall ("friends/remove", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Friends.RemoveResponse>(cb));
		}

		public void list_invites( Roar.WebObjects.Friends.ListInvitesArguments args, ZWebAPI.Callback<Roar.WebObjects.Friends.ListInvitesResponse> cb)
		{
			api.MakeCall ("friends/list_invites", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Friends.ListInvitesResponse>(cb));
		}

	}

	public class GoogleActions : APIBridge, IGoogleActions
	{
		public GoogleActions (IRequestSender caller) : base(caller)
		{
		}

		public void bind_user( Roar.WebObjects.Google.BindUserArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.BindUserResponse> cb)
		{
			api.MakeCall ("google/bind_user", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Google.BindUserResponse>(cb));
		}

		public void bind_user_token( Roar.WebObjects.Google.BindUserTokenArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.BindUserTokenResponse> cb)
		{
			api.MakeCall ("google/bind_user_token", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Google.BindUserTokenResponse>(cb));
		}

		public void create_user( Roar.WebObjects.Google.CreateUserArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.CreateUserResponse> cb)
		{
			api.MakeCall ("google/create_user", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Google.CreateUserResponse>(cb));
		}

		public void create_user_token( Roar.WebObjects.Google.CreateUserTokenArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.CreateUserTokenResponse> cb)
		{
			api.MakeCall ("google/create_user_token", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Google.CreateUserTokenResponse>(cb));
		}

		public void friends( Roar.WebObjects.Google.FriendsArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.FriendsResponse> cb)
		{
			api.MakeCall ("google/friends", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Google.FriendsResponse>(cb));
		}

		public void login_user( Roar.WebObjects.Google.LoginUserArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.LoginUserResponse> cb)
		{
			api.MakeCall ("google/login_user", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Google.LoginUserResponse>(cb));
		}

		public void login_user_token( Roar.WebObjects.Google.LoginUserTokenArguments args, ZWebAPI.Callback<Roar.WebObjects.Google.LoginUserTokenResponse> cb)
		{
			api.MakeCall ("google/login_user_token", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Google.LoginUserTokenResponse>(cb));
		}

	}

	public class InfoActions : APIBridge, IInfoActions
	{
		public InfoActions (IRequestSender caller) : base(caller)
		{
		}

		public void get_bulk_player_info( Roar.WebObjects.Info.GetBulkPlayerInfoArguments args, ZWebAPI.Callback<Roar.WebObjects.Info.GetBulkPlayerInfoResponse> cb)
		{
			api.MakeCall ("info/get_bulk_player_info", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Info.GetBulkPlayerInfoResponse>(cb));
		}

		public void ping( Roar.WebObjects.Info.PingArguments args, ZWebAPI.Callback<Roar.WebObjects.Info.PingResponse> cb)
		{
			api.MakeCall ("info/ping", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Info.PingResponse>(cb));
		}

		public void user( Roar.WebObjects.Info.UserArguments args, ZWebAPI.Callback<Roar.WebObjects.Info.UserResponse> cb)
		{
			api.MakeCall ("info/user", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Info.UserResponse>(cb));
		}

		public void poll( Roar.WebObjects.Info.PollArguments args, ZWebAPI.Callback<Roar.WebObjects.Info.PollResponse> cb)
		{
			api.MakeCall ("info/poll", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Info.PollResponse>(cb));
		}

	}

	public class ItemsActions : APIBridge, IItemsActions
	{
		public ItemsActions (IRequestSender caller) : base(caller)
		{
		}

		public void equip( Roar.WebObjects.Items.EquipArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.EquipResponse> cb)
		{
			api.MakeCall ("items/equip", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Items.EquipResponse>(cb));
		}

		public void list( Roar.WebObjects.Items.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.ListResponse> cb)
		{
			api.MakeCall ("items/list", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Items.ListResponse>(cb));
		}

		public void sell( Roar.WebObjects.Items.SellArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.SellResponse> cb)
		{
			api.MakeCall ("items/sell", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Items.SellResponse>(cb));
		}

		public void _set( Roar.WebObjects.Items.SetArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.SetResponse> cb)
		{
			api.MakeCall ("items/set", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Items.SetResponse>(cb));
		}

		public void unequip( Roar.WebObjects.Items.UnequipArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.UnequipResponse> cb)
		{
			api.MakeCall ("items/unequip", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Items.UnequipResponse>(cb));
		}

		public void use( Roar.WebObjects.Items.UseArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.UseResponse> cb)
		{
			api.MakeCall ("items/use", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Items.UseResponse>(cb));
		}

		public void view( Roar.WebObjects.Items.ViewArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.ViewResponse> cb)
		{
			api.MakeCall ("items/view", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Items.ViewResponse>(cb));
		}

		public void view_all( Roar.WebObjects.Items.ViewAllArguments args, ZWebAPI.Callback<Roar.WebObjects.Items.ViewAllResponse> cb)
		{
			api.MakeCall ("items/view_all", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Items.ViewAllResponse>(cb));
		}

	}

	public class LeaderboardsActions : APIBridge, ILeaderboardsActions
	{
		public LeaderboardsActions (IRequestSender caller) : base(caller)
		{
		}

		public void list( Roar.WebObjects.Leaderboards.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Leaderboards.ListResponse> cb)
		{
			api.MakeCall ("leaderboards/list", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Leaderboards.ListResponse>(cb));
		}

		public void view( Roar.WebObjects.Leaderboards.ViewArguments args, ZWebAPI.Callback<Roar.WebObjects.Leaderboards.ViewResponse> cb)
		{
			api.MakeCall ("leaderboards/view", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Leaderboards.ViewResponse>(cb));
		}

	}

	public class MailActions : APIBridge, IMailActions
	{
		public MailActions (IRequestSender caller) : base(caller)
		{
		}

		public void accept( Roar.WebObjects.Mail.AcceptArguments args, ZWebAPI.Callback<Roar.WebObjects.Mail.AcceptResponse> cb)
		{
			api.MakeCall ("mail/accept", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Mail.AcceptResponse>(cb));
		}

		public void send( Roar.WebObjects.Mail.SendArguments args, ZWebAPI.Callback<Roar.WebObjects.Mail.SendResponse> cb)
		{
			api.MakeCall ("mail/send", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Mail.SendResponse>(cb));
		}

		public void what_can_i_accept( Roar.WebObjects.Mail.WhatCanIAcceptArguments args, ZWebAPI.Callback<Roar.WebObjects.Mail.WhatCanIAcceptResponse> cb)
		{
			api.MakeCall ("mail/what_can_i_accept", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Mail.WhatCanIAcceptResponse>(cb));
		}

		public void what_can_i_send( Roar.WebObjects.Mail.WhatCanISendArguments args, ZWebAPI.Callback<Roar.WebObjects.Mail.WhatCanISendResponse> cb)
		{
			api.MakeCall ("mail/what_can_i_send", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Mail.WhatCanISendResponse>(cb));
		}

	}

	public class ShopActions : APIBridge, IShopActions
	{
		public ShopActions (IRequestSender caller) : base(caller)
		{
		}

		public void list( Roar.WebObjects.Shop.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Shop.ListResponse> cb)
		{
			api.MakeCall ("shop/list", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Shop.ListResponse>(cb));
		}

		public void buy( Roar.WebObjects.Shop.BuyArguments args, ZWebAPI.Callback<Roar.WebObjects.Shop.BuyResponse> cb)
		{
			api.MakeCall ("shop/buy", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Shop.BuyResponse>(cb));
		}

	}

	public class ScriptsActions : APIBridge, IScriptsActions
	{
		public ScriptsActions (IRequestSender caller) : base(caller)
		{
		}

		public void run( Roar.WebObjects.Scripts.RunArguments args, ZWebAPI.Callback<Roar.WebObjects.Scripts.RunResponse> cb)
		{
			api.MakeCall ("scripts/run", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Scripts.RunResponse>(cb));
		}

	}

	public class TasksActions : APIBridge, ITasksActions
	{
		public TasksActions (IRequestSender caller) : base(caller)
		{
		}

		public void list( Roar.WebObjects.Tasks.ListArguments args, ZWebAPI.Callback<Roar.WebObjects.Tasks.ListResponse> cb)
		{
			api.MakeCall ("tasks/list", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Tasks.ListResponse>(cb));
		}

		public void start( Roar.WebObjects.Tasks.StartArguments args, ZWebAPI.Callback<Roar.WebObjects.Tasks.StartResponse> cb)
		{
			api.MakeCall ("tasks/start", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Tasks.StartResponse>(cb));
		}

	}

	public class UserActions : APIBridge, IUserActions
	{
		public UserActions (IRequestSender caller) : base(caller)
		{
		}

		public void achievements( Roar.WebObjects.User.AchievementsArguments args, ZWebAPI.Callback<Roar.WebObjects.User.AchievementsResponse> cb)
		{
			api.MakeCall ("user/achievements", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.AchievementsResponse>(cb));
		}

		public void change_name( Roar.WebObjects.User.ChangeNameArguments args, ZWebAPI.Callback<Roar.WebObjects.User.ChangeNameResponse> cb)
		{
			api.MakeCall ("user/change_name", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.ChangeNameResponse>(cb));
		}

		public void change_password( Roar.WebObjects.User.ChangePasswordArguments args, ZWebAPI.Callback<Roar.WebObjects.User.ChangePasswordResponse> cb)
		{
			api.MakeCall ("user/change_password", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.ChangePasswordResponse>(cb));
		}

		public void create( Roar.WebObjects.User.CreateArguments args, ZWebAPI.Callback<Roar.WebObjects.User.CreateResponse> cb)
		{
			api.MakeCall ("user/create", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.CreateResponse>(cb));
		}

		public void login( Roar.WebObjects.User.LoginArguments args, ZWebAPI.Callback<Roar.WebObjects.User.LoginResponse> cb)
		{
			api.MakeCall ("user/login", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.LoginResponse>(cb));
		}

		public void login_facebook_oauth( Roar.WebObjects.User.LoginFacebookOauthArguments args, ZWebAPI.Callback<Roar.WebObjects.User.LoginFacebookOauthResponse> cb)
		{
			api.MakeCall ("facebook/login_oauth", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.LoginFacebookOauthResponse>(cb));
		}

		public void logout( Roar.WebObjects.User.LogoutArguments args, ZWebAPI.Callback<Roar.WebObjects.User.LogoutResponse> cb)
		{
			api.MakeCall ("user/logout", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.LogoutResponse>(cb));
		}

		public void netdrive_save( Roar.WebObjects.User.NetdriveSaveArguments args, ZWebAPI.Callback<Roar.WebObjects.User.NetdriveSaveResponse> cb)
		{
			api.MakeCall ("user/netdrive_set", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.NetdriveSaveResponse>(cb));
		}

		public void netdrive_fetch( Roar.WebObjects.User.NetdriveFetchArguments args, ZWebAPI.Callback<Roar.WebObjects.User.NetdriveFetchResponse> cb)
		{
			api.MakeCall ("user/netdrive_get", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.NetdriveFetchResponse>(cb));
		}

		public void _set( Roar.WebObjects.User.SetArguments args, ZWebAPI.Callback<Roar.WebObjects.User.SetResponse> cb)
		{
			api.MakeCall ("user/set", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.SetResponse>(cb));
		}

		public void view( Roar.WebObjects.User.ViewArguments args, ZWebAPI.Callback<Roar.WebObjects.User.ViewResponse> cb)
		{
			api.MakeCall ("user/view", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.User.ViewResponse>(cb));
		}

	}

	public class UrbanairshipActions : APIBridge, IUrbanairshipActions
	{
		public UrbanairshipActions (IRequestSender caller) : base(caller)
		{
		}

		public void ios_register( Roar.WebObjects.Urbanairship.IosRegisterArguments args, ZWebAPI.Callback<Roar.WebObjects.Urbanairship.IosRegisterResponse> cb)
		{
			api.MakeCall ("urbanairship/ios_register", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Urbanairship.IosRegisterResponse>(cb));
		}

		public void push( Roar.WebObjects.Urbanairship.PushArguments args, ZWebAPI.Callback<Roar.WebObjects.Urbanairship.PushResponse> cb)
		{
			api.MakeCall ("urbanairship/push", args.ToHashtable(), new CallbackBridge<Roar.WebObjects.Urbanairship.PushResponse>(cb));
		}

	}


}

