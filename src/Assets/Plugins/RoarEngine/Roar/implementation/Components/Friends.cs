using System.Collections;
using System.Collections.Generic;
using Roar.Components;

namespace Roar.implementation.Components
{

  public class Friends : IFriends
  {
    protected DataStore dataStore;
    IWebAPI.IFriendsActions friendsActions;
    ILogger logger;
    
    public Friends (IWebAPI.IFriendsActions friendsActions, DataStore datastore, ILogger logger)
    {
      this.friendsActions = friendsActions;
      this.dataStore = datastore;
      this.logger = logger;
    }
    
    public bool HasDataFromServer { get { return dataStore.friends.HasDataFromServer || dataStore.friendInvites.HasDataFromServer || dataStore.friendInviteInfo.HasDataFromServer; } }
    
    public void AcceptFriendInvite (string friends_id, string invite_id, Roar.Callback cb)
    {
      if (friends_id == "" || invite_id == "")
      {
        logger.DebugLog ("[roar] -- Must specify friends_id and invite_id for accepting a friend invite.");
        return;
      }
      Hashtable args = new Hashtable ();
      args["friends_id"] = friends_id;
      args["invite_id"] = invite_id;
      friendsActions.accept (args, new AcceptFriendsCallback (cb));
    }
    
    protected class AcceptFriendsCallback : SimpleRequestCallback<IXMLNode>
    {
      public AcceptFriendsCallback (Roar.Callback in_cb) : base (in_cb) {}
      public override object OnSuccess (CallbackInfo<IXMLNode> info) {return info.d;}
    }
    
    public void DeclineFriendInvite (string invite_id, Roar.Callback cb)
    {
      if (invite_id == "")
      {
        logger.DebugLog ("[roar] -- Must specify invite_id for declining a friend invite.");
        return;
      }
      Hashtable args = new Hashtable ();
      args["invite_id"] = invite_id;
      friendsActions.decline (args, new DeclineFriendsCallback (cb));
    }
    
    protected class DeclineFriendsCallback : SimpleRequestCallback<IXMLNode>
    {
      public DeclineFriendsCallback (Roar.Callback in_cb) : base (in_cb) {}
      public override object OnSuccess (CallbackInfo<IXMLNode> info) {return info.d;}
    }
    
    public void InviteFriend (string friend_id, string player_id, Roar.Callback cb)
    {
      if (friend_id == "")
      {
        logger.DebugLog("[roar] -- Must specify friend_id for inviting a friend.");
        return;
      }
      if (player_id == "")
      {
        logger.DebugLog("[roar] -- Must specify player_id for inviting a friend.");
      }
      Hashtable args = new Hashtable();
      args["friend_id"] = friend_id;
      args["player_id"] = player_id;
      friendsActions.invite(args, new InviteFriendCallback(cb));
    }
    
    protected class InviteFriendCallback : SimpleRequestCallback<IXMLNode>
    {
      public InviteFriendCallback (Roar.Callback in_cb) : base (in_cb) {}
      public override object OnSuccess (CallbackInfo<IXMLNode> info) {return info.d;}
    }
    
    public void RemoveFriend (string friend_id, string player_id, Roar.Callback cb)
    {
      if (friend_id == "")
      {
        logger.DebugLog("[roar] -- Must specify friend_id for removing a friend.");
      }
      if (player_id == "")
      {
        logger.DebugLog("[roar] -- Must specify player_id for removing a friend.");
      }
      Hashtable args = new Hashtable();
      args["friend_id"] = friend_id;
      args["player_id"] = player_id;
      friendsActions.remove(args, new RemoveFriendCallback(cb));
    }
    
    protected class RemoveFriendCallback : SimpleRequestCallback<IXMLNode>
    {
      public RemoveFriendCallback (Roar.Callback in_cb) : base (in_cb) {}
      public override object OnSuccess (CallbackInfo<IXMLNode> info) {return info.d;}
    }
    
    public void ListFriendInvites (Roar.Callback cb)
    {
      dataStore.friendInvites.Fetch(null);
      if (cb != null)
      {
        cb (new Roar.CallbackInfo<IList<DomainObjects.FriendInvite>>(dataStore.friendInvites.List()));
      }
    }
    
    public void ListFriends (Roar.Callback cb)
    {
      dataStore.friends.Fetch(null);
      if (cb != null)
      {
        cb (new Roar.CallbackInfo<IList<DomainObjects.Friend>>(dataStore.friends.List()));
      }
    }
    
    public void FriendInviteInfo (string invite_id, Roar.Callback cb)
    {
      if (invite_id == "")
      {
        logger.DebugLog("[roar] -- Must specify invite_id for fetch invite info.");
      }
      Hashtable args = new Hashtable();
      args["invite_id"] = invite_id;
      dataStore.friendInviteInfo.Fetch(null, args);
      if (cb != null)
      {
        cb (new Roar.CallbackInfo<DomainObjects.FriendInviteInfo>(dataStore.friendInviteInfo.List()[0]));
      }
    }
    
  }
  
}
