using System;
using System.Collections;
using System.Collections.Generic;


namespace Roar.Components
{
	public interface IFriends
	{
		bool HasDataFromServer { get; }
		void ListFriends (Roar.Callback callback);
		void AcceptFriendInvite (string friend_id, string invite_id, Roar.Callback callback);
		void DeclineFriendInvite (string invite_id, Roar.Callback callback);
		void InviteFriend (string friend_id, string player_id, Roar.Callback callback);
		void RemoveFriend (string friend_id, string player_id, Roar.Callback callback);
		void ListFriendInvites (Roar.Callback callback);
		void FriendInviteInfo (string invite_id, Roar.Callback callback);
	}
}

