using System;
using System.Collections;
using System.Collections.Generic;


namespace Roar.Components
{
	public interface IFriends
	{
		void ListFriends (Roar.Callback<WebObjects.Friends.ListResponse> callback);
		void AcceptFriendInvite (string friend_id, string invite_id, Roar.Callback<WebObjects.Friends.AcceptResponse> callback);
		void DeclineFriendInvite (string invite_id, Roar.Callback<WebObjects.Friends.DeclineResponse> callback);
		void InviteFriend (string friend_id, string player_id, Roar.Callback<WebObjects.Friends.InviteResponse> callback);
		void RemoveFriend (string friend_id, string player_id, Roar.Callback<WebObjects.Friends.RemoveResponse> callback);
		void ListFriendInvites (Roar.Callback<WebObjects.Friends.List_invitesResponse> callback);
		void FriendInviteInfo (string invite_id, Roar.Callback<WebObjects.Friends.Invite_infoResponse> callback);
	}
}

