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
using System.Linq;
using UnityEngine;
using Roar.DomainObjects;


namespace Roar.DataConversion.Responses
{
	namespace Admin
	{
		//Response from admin/create_player
		public class CreatePlayer : IXmlToObject< Roar.WebObjects.Admin.CreatePlayerResponse >
		{
			public Roar.WebObjects.Admin.CreatePlayerResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Admin.CreatePlayerResponse retval = new Roar.WebObjects.Admin.CreatePlayerResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from admin/delete_player
		public class DeletePlayer : IXmlToObject< Roar.WebObjects.Admin.DeletePlayerResponse >
		{
			public Roar.WebObjects.Admin.DeletePlayerResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Admin.DeletePlayerResponse retval = new Roar.WebObjects.Admin.DeletePlayerResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from admin/increment_stat
		public class IncrementStat : IXmlToObject< Roar.WebObjects.Admin.IncrementStatResponse >
		{
			public Roar.WebObjects.Admin.IncrementStatResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Admin.IncrementStatResponse retval = new Roar.WebObjects.Admin.IncrementStatResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from admin/login_user
		public class LoginUser : IXmlToObject< Roar.WebObjects.Admin.LoginUserResponse >
		{
			public Roar.WebObjects.Admin.LoginUserResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Admin.LoginUserResponse retval = new Roar.WebObjects.Admin.LoginUserResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from admin/set
		public class Set : IXmlToObject< Roar.WebObjects.Admin.SetResponse >
		{
			public Roar.WebObjects.Admin.SetResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Admin.SetResponse retval = new Roar.WebObjects.Admin.SetResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from admin/set_custom
		public class SetCustom : IXmlToObject< Roar.WebObjects.Admin.SetCustomResponse >
		{
			public Roar.WebObjects.Admin.SetCustomResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Admin.SetCustomResponse retval = new Roar.WebObjects.Admin.SetCustomResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from admin/view_player
		public class ViewPlayer : IXmlToObject< Roar.WebObjects.Admin.ViewPlayerResponse >
		{
			public Roar.WebObjects.Admin.ViewPlayerResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Admin.ViewPlayerResponse retval = new Roar.WebObjects.Admin.ViewPlayerResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Appstore
	{
		//Response from appstore/buy
		public class Buy : IXmlToObject< Roar.WebObjects.Appstore.BuyResponse >
		{
			public Roar.WebObjects.Appstore.BuyResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Appstore.BuyResponse retval = new Roar.WebObjects.Appstore.BuyResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from appstore/shop_list
		public class ShopList : IXmlToObject< Roar.WebObjects.Appstore.ShopListResponse >
		{
			public Roar.WebObjects.Appstore.ShopListResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Appstore.ShopListResponse retval = new Roar.WebObjects.Appstore.ShopListResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace ChromeWebStore
	{
		//Response from chrome_web_store/list
		public class List : IXmlToObject< Roar.WebObjects.ChromeWebStore.ListResponse >
		{
			public Roar.WebObjects.ChromeWebStore.ListResponse Build(IXMLNode n)
			{
				Roar.WebObjects.ChromeWebStore.ListResponse retval = new Roar.WebObjects.ChromeWebStore.ListResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Facebook
	{
		//Response from facebook/bind_oauth
		public class BindOauth : IXmlToObject< Roar.WebObjects.Facebook.BindOauthResponse >
		{
			public Roar.WebObjects.Facebook.BindOauthResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Facebook.BindOauthResponse retval = new Roar.WebObjects.Facebook.BindOauthResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from facebook/bind_signed
		public class BindSigned : IXmlToObject< Roar.WebObjects.Facebook.BindSignedResponse >
		{
			public Roar.WebObjects.Facebook.BindSignedResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Facebook.BindSignedResponse retval = new Roar.WebObjects.Facebook.BindSignedResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from facebook/create_oauth
		public class CreateOauth : IXmlToObject< Roar.WebObjects.Facebook.CreateOauthResponse >
		{
			public Roar.WebObjects.Facebook.CreateOauthResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Facebook.CreateOauthResponse retval = new Roar.WebObjects.Facebook.CreateOauthResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from facebook/create_signed
		public class CreateSigned : IXmlToObject< Roar.WebObjects.Facebook.CreateSignedResponse >
		{
			public Roar.WebObjects.Facebook.CreateSignedResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Facebook.CreateSignedResponse retval = new Roar.WebObjects.Facebook.CreateSignedResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from facebook/fetch_oauth_token
		public class FetchOauthToken : IXmlToObject< Roar.WebObjects.Facebook.FetchOauthTokenResponse >
		{
			public Roar.WebObjects.Facebook.FetchOauthTokenResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Facebook.FetchOauthTokenResponse retval = new Roar.WebObjects.Facebook.FetchOauthTokenResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from facebook/friends
		public class Friends : IXmlToObject< Roar.WebObjects.Facebook.FriendsResponse >
		{
			public Roar.WebObjects.Facebook.FriendsResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Facebook.FriendsResponse retval = new Roar.WebObjects.Facebook.FriendsResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from facebook/login_oauth
		public class LoginOauth : IXmlToObject< Roar.WebObjects.Facebook.LoginOauthResponse >
		{
			public Roar.WebObjects.Facebook.LoginOauthResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Facebook.LoginOauthResponse retval = new Roar.WebObjects.Facebook.LoginOauthResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from facebook/login_signed
		public class LoginSigned : IXmlToObject< Roar.WebObjects.Facebook.LoginSignedResponse >
		{
			public Roar.WebObjects.Facebook.LoginSignedResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Facebook.LoginSignedResponse retval = new Roar.WebObjects.Facebook.LoginSignedResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from facebook/shop_list
		public class ShopList : IXmlToObject< Roar.WebObjects.Facebook.ShopListResponse >
		{
			public Roar.WebObjects.Facebook.ShopListResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Facebook.ShopListResponse retval = new Roar.WebObjects.Facebook.ShopListResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Friends
	{
		//Response from friends/accept
		public class Accept : IXmlToObject< Roar.WebObjects.Friends.AcceptResponse >
		{
			public Roar.WebObjects.Friends.AcceptResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Friends.AcceptResponse retval = new Roar.WebObjects.Friends.AcceptResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from friends/decline
		public class Decline : IXmlToObject< Roar.WebObjects.Friends.DeclineResponse >
		{
			public Roar.WebObjects.Friends.DeclineResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Friends.DeclineResponse retval = new Roar.WebObjects.Friends.DeclineResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from friends/invite
		public class Invite : IXmlToObject< Roar.WebObjects.Friends.InviteResponse >
		{
			public Roar.WebObjects.Friends.InviteResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Friends.InviteResponse retval = new Roar.WebObjects.Friends.InviteResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from friends/invite_info
		public class InviteInfo : IXmlToObject< Roar.WebObjects.Friends.InviteInfoResponse >
		{
			public Roar.WebObjects.Friends.InviteInfoResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Friends.InviteInfoResponse retval = new Roar.WebObjects.Friends.InviteInfoResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from friends/list
		public class List : IXmlToObject< Roar.WebObjects.Friends.ListResponse >
		{
			public Roar.WebObjects.Friends.ListResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Friends.ListResponse retval = new Roar.WebObjects.Friends.ListResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from friends/remove
		public class Remove : IXmlToObject< Roar.WebObjects.Friends.RemoveResponse >
		{
			public Roar.WebObjects.Friends.RemoveResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Friends.RemoveResponse retval = new Roar.WebObjects.Friends.RemoveResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from friends/list_invites
		public class ListInvites : IXmlToObject< Roar.WebObjects.Friends.ListInvitesResponse >
		{
			public Roar.WebObjects.Friends.ListInvitesResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Friends.ListInvitesResponse retval = new Roar.WebObjects.Friends.ListInvitesResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Google
	{
		//Response from google/bind_user
		public class BindUser : IXmlToObject< Roar.WebObjects.Google.BindUserResponse >
		{
			public Roar.WebObjects.Google.BindUserResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Google.BindUserResponse retval = new Roar.WebObjects.Google.BindUserResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from google/bind_user_token
		public class BindUserToken : IXmlToObject< Roar.WebObjects.Google.BindUserTokenResponse >
		{
			public Roar.WebObjects.Google.BindUserTokenResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Google.BindUserTokenResponse retval = new Roar.WebObjects.Google.BindUserTokenResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from google/create_user
		public class CreateUser : IXmlToObject< Roar.WebObjects.Google.CreateUserResponse >
		{
			public Roar.WebObjects.Google.CreateUserResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Google.CreateUserResponse retval = new Roar.WebObjects.Google.CreateUserResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from google/create_user_token
		public class CreateUserToken : IXmlToObject< Roar.WebObjects.Google.CreateUserTokenResponse >
		{
			public Roar.WebObjects.Google.CreateUserTokenResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Google.CreateUserTokenResponse retval = new Roar.WebObjects.Google.CreateUserTokenResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from google/friends
		public class Friends : IXmlToObject< Roar.WebObjects.Google.FriendsResponse >
		{
			public Roar.WebObjects.Google.FriendsResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Google.FriendsResponse retval = new Roar.WebObjects.Google.FriendsResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from google/login_or_create_user
		public class LoginOrCreateUser : IXmlToObject< Roar.WebObjects.Google.LoginOrCreateUserResponse >
		{
			public Roar.WebObjects.Google.LoginOrCreateUserResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Google.LoginOrCreateUserResponse retval = new Roar.WebObjects.Google.LoginOrCreateUserResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from google/login_user
		public class LoginUser : IXmlToObject< Roar.WebObjects.Google.LoginUserResponse >
		{
			public Roar.WebObjects.Google.LoginUserResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Google.LoginUserResponse retval = new Roar.WebObjects.Google.LoginUserResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from google/login_user_token
		public class LoginUserToken : IXmlToObject< Roar.WebObjects.Google.LoginUserTokenResponse >
		{
			public Roar.WebObjects.Google.LoginUserTokenResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Google.LoginUserTokenResponse retval = new Roar.WebObjects.Google.LoginUserTokenResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from google/token
		public class Token : IXmlToObject< Roar.WebObjects.Google.TokenResponse >
		{
			public Roar.WebObjects.Google.TokenResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Google.TokenResponse retval = new Roar.WebObjects.Google.TokenResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Info
	{
		//Response from info/get_bulk_player_info
		public class GetBulkPlayerInfo : IXmlToObject< Roar.WebObjects.Info.GetBulkPlayerInfoResponse >
		{
			public Roar.WebObjects.Info.GetBulkPlayerInfoResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Info.GetBulkPlayerInfoResponse retval = new Roar.WebObjects.Info.GetBulkPlayerInfoResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from info/ping
		public class Ping : IXmlToObject< Roar.WebObjects.Info.PingResponse >
		{
			public Roar.WebObjects.Info.PingResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Info.PingResponse retval = new Roar.WebObjects.Info.PingResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from info/user
		public class User : IXmlToObject< Roar.WebObjects.Info.UserResponse >
		{
			public Roar.WebObjects.Info.UserResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Info.UserResponse retval = new Roar.WebObjects.Info.UserResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from info/poll
		public class Poll : IXmlToObject< Roar.WebObjects.Info.PollResponse >
		{
			public Roar.WebObjects.Info.PollResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Info.PollResponse retval = new Roar.WebObjects.Info.PollResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Items
	{
		//Response from items/equip
		public class Equip : IXmlToObject< Roar.WebObjects.Items.EquipResponse >
		{
			public Roar.WebObjects.Items.EquipResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Items.EquipResponse retval = new Roar.WebObjects.Items.EquipResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from items/list
		public class List : IXmlToObject< Roar.WebObjects.Items.ListResponse >
		{
			public Roar.WebObjects.Items.ListResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Items.ListResponse retval = new Roar.WebObjects.Items.ListResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from items/sell
		public class Sell : IXmlToObject< Roar.WebObjects.Items.SellResponse >
		{
			public Roar.WebObjects.Items.SellResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Items.SellResponse retval = new Roar.WebObjects.Items.SellResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from items/set
		public class Set : IXmlToObject< Roar.WebObjects.Items.SetResponse >
		{
			public Roar.WebObjects.Items.SetResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Items.SetResponse retval = new Roar.WebObjects.Items.SetResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from items/unequip
		public class Unequip : IXmlToObject< Roar.WebObjects.Items.UnequipResponse >
		{
			public Roar.WebObjects.Items.UnequipResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Items.UnequipResponse retval = new Roar.WebObjects.Items.UnequipResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from items/use
		public class Use : IXmlToObject< Roar.WebObjects.Items.UseResponse >
		{
			public Roar.WebObjects.Items.UseResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Items.UseResponse retval = new Roar.WebObjects.Items.UseResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from items/view
		public class View : IXmlToObject< Roar.WebObjects.Items.ViewResponse >
		{
			public Roar.WebObjects.Items.ViewResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Items.ViewResponse retval = new Roar.WebObjects.Items.ViewResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from items/view_all
		public class ViewAll : IXmlToObject< Roar.WebObjects.Items.ViewAllResponse >
		{
			public Roar.WebObjects.Items.ViewAllResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Items.ViewAllResponse retval = new Roar.WebObjects.Items.ViewAllResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Leaderboards
	{
		//Response from leaderboards/list
		public class List : IXmlToObject< Roar.WebObjects.Leaderboards.ListResponse >
		{
			public Roar.WebObjects.Leaderboards.ListResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Leaderboards.ListResponse retval = new Roar.WebObjects.Leaderboards.ListResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from leaderboards/view
		public class View : IXmlToObject< Roar.WebObjects.Leaderboards.ViewResponse >
		{
			public Roar.WebObjects.Leaderboards.ViewResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Leaderboards.ViewResponse retval = new Roar.WebObjects.Leaderboards.ViewResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Mail
	{
		//Response from mail/accept
		public class Accept : IXmlToObject< Roar.WebObjects.Mail.AcceptResponse >
		{
			public Roar.WebObjects.Mail.AcceptResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Mail.AcceptResponse retval = new Roar.WebObjects.Mail.AcceptResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from mail/send
		public class Send : IXmlToObject< Roar.WebObjects.Mail.SendResponse >
		{
			public Roar.WebObjects.Mail.SendResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Mail.SendResponse retval = new Roar.WebObjects.Mail.SendResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from mail/what_can_i_accept
		public class WhatCanIAccept : IXmlToObject< Roar.WebObjects.Mail.WhatCanIAcceptResponse >
		{
			public Roar.WebObjects.Mail.WhatCanIAcceptResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Mail.WhatCanIAcceptResponse retval = new Roar.WebObjects.Mail.WhatCanIAcceptResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from mail/what_can_i_send
		public class WhatCanISend : IXmlToObject< Roar.WebObjects.Mail.WhatCanISendResponse >
		{
			public Roar.WebObjects.Mail.WhatCanISendResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Mail.WhatCanISendResponse retval = new Roar.WebObjects.Mail.WhatCanISendResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Shop
	{
		//Response from shop/list
		public class List : IXmlToObject< Roar.WebObjects.Shop.ListResponse >
		{
			public Roar.WebObjects.Shop.ListResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Shop.ListResponse retval = new Roar.WebObjects.Shop.ListResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Scripts
	{
		//Response from scripts/run
		public class Run : IXmlToObject< Roar.WebObjects.Scripts.RunResponse >
		{
			public Roar.WebObjects.Scripts.RunResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Scripts.RunResponse retval = new Roar.WebObjects.Scripts.RunResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from scripts/run_admin
		public class RunAdmin : IXmlToObject< Roar.WebObjects.Scripts.RunAdminResponse >
		{
			public Roar.WebObjects.Scripts.RunAdminResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Scripts.RunAdminResponse retval = new Roar.WebObjects.Scripts.RunAdminResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Tasks
	{
		//Response from tasks/list
		public class List : IXmlToObject< Roar.WebObjects.Tasks.ListResponse >
		{
			public Roar.WebObjects.Tasks.ListResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Tasks.ListResponse retval = new Roar.WebObjects.Tasks.ListResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from tasks/start
		public class Start : IXmlToObject< Roar.WebObjects.Tasks.StartResponse >
		{
			public Roar.WebObjects.Tasks.StartResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Tasks.StartResponse retval = new Roar.WebObjects.Tasks.StartResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace User
	{
		//Response from user/achievements
		public class Achievements : IXmlToObject< Roar.WebObjects.User.AchievementsResponse >
		{
			public Roar.WebObjects.User.AchievementsResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.AchievementsResponse retval = new Roar.WebObjects.User.AchievementsResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/change_name
		public class ChangeName : IXmlToObject< Roar.WebObjects.User.ChangeNameResponse >
		{
			public Roar.WebObjects.User.ChangeNameResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.ChangeNameResponse retval = new Roar.WebObjects.User.ChangeNameResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/change_password
		public class ChangePassword : IXmlToObject< Roar.WebObjects.User.ChangePasswordResponse >
		{
			public Roar.WebObjects.User.ChangePasswordResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.ChangePasswordResponse retval = new Roar.WebObjects.User.ChangePasswordResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/create
		public class Create : IXmlToObject< Roar.WebObjects.User.CreateResponse >
		{
			public Roar.WebObjects.User.CreateResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.CreateResponse retval = new Roar.WebObjects.User.CreateResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/login
		public class Login : IXmlToObject< Roar.WebObjects.User.LoginResponse >
		{
			public Roar.WebObjects.User.LoginResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.LoginResponse retval = new Roar.WebObjects.User.LoginResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/login_facebook_oauth
		public class LoginFacebookOauth : IXmlToObject< Roar.WebObjects.User.LoginFacebookOauthResponse >
		{
			public Roar.WebObjects.User.LoginFacebookOauthResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.LoginFacebookOauthResponse retval = new Roar.WebObjects.User.LoginFacebookOauthResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/logout
		public class Logout : IXmlToObject< Roar.WebObjects.User.LogoutResponse >
		{
			public Roar.WebObjects.User.LogoutResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.LogoutResponse retval = new Roar.WebObjects.User.LogoutResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/netdrive_save
		public class NetdriveSave : IXmlToObject< Roar.WebObjects.User.NetdriveSaveResponse >
		{
			public Roar.WebObjects.User.NetdriveSaveResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.NetdriveSaveResponse retval = new Roar.WebObjects.User.NetdriveSaveResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/netdrive_fetch
		public class NetdriveFetch : IXmlToObject< Roar.WebObjects.User.NetdriveFetchResponse >
		{
			public Roar.WebObjects.User.NetdriveFetchResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.NetdriveFetchResponse retval = new Roar.WebObjects.User.NetdriveFetchResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/set
		public class Set : IXmlToObject< Roar.WebObjects.User.SetResponse >
		{
			public Roar.WebObjects.User.SetResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.SetResponse retval = new Roar.WebObjects.User.SetResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/view
		public class View : IXmlToObject< Roar.WebObjects.User.ViewResponse >
		{
			public Roar.WebObjects.User.ViewResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.ViewResponse retval = new Roar.WebObjects.User.ViewResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/private_set
		public class PrivateSet : IXmlToObject< Roar.WebObjects.User.PrivateSetResponse >
		{
			public Roar.WebObjects.User.PrivateSetResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.PrivateSetResponse retval = new Roar.WebObjects.User.PrivateSetResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from user/private_get
		public class PrivateGet : IXmlToObject< Roar.WebObjects.User.PrivateGetResponse >
		{
			public Roar.WebObjects.User.PrivateGetResponse Build(IXMLNode n)
			{
				Roar.WebObjects.User.PrivateGetResponse retval = new Roar.WebObjects.User.PrivateGetResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}
	namespace Urbanairship
	{
		//Response from urbanairship/ios_register
		public class IosRegister : IXmlToObject< Roar.WebObjects.Urbanairship.IosRegisterResponse >
		{
			public Roar.WebObjects.Urbanairship.IosRegisterResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Urbanairship.IosRegisterResponse retval = new Roar.WebObjects.Urbanairship.IosRegisterResponse();
				//TODO: Implement me
				return retval;
			}
		}
		//Response from urbanairship/push
		public class Push : IXmlToObject< Roar.WebObjects.Urbanairship.PushResponse >
		{
			public Roar.WebObjects.Urbanairship.PushResponse Build(IXMLNode n)
			{
				Roar.WebObjects.Urbanairship.PushResponse retval = new Roar.WebObjects.Urbanairship.PushResponse();
				//TODO: Implement me
				return retval;
			}
		}

 	}

}

