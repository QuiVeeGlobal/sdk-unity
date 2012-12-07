
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;
using NMock2;

using Roar.implementation.Components;
using Roar.Components;

[TestFixture]
public class WebAPIFriendsTests : ComponentTests
{

  protected IFriends friends = null;
  
  [SetUp]
  new public void TestInitialise()
  {
    base.TestInitialise();
    friends = roar.Friends;
    Assert.IsNotNull(friends);
  }
  
  /*
  [Test]
  public void GeneralTest()
  {
    Logger logger = new Logger ();
  	IRequestSender caller = mockery.NewMock<IRequestSender>();
    WebAPI.FriendsActions friendsActions = new WebAPI.FriendsActions (caller);
    
    Roar.implementation.DataStore datastore = new Roar.implementation.DataStore (caller, logger);
    Friends friends = new Friends(friendsActions, datastore, logger);
    
    Expect.AtLeastOnce.On(caller).Method("MakeCall").With("friends/decline", Is.Anything, Is.Anything);
    
    Roar.Callback cb = null;
    friends.DeclineFriendInvite("whatever", cb);
  }
  */
  
  [Test]
  public void TestAcceptFriend()
  {
    string accept_result = "<roar tick='123'><friends><accept status='ok'/></friends></roar>";
    requestSender.addMockResponse("friends/accept", accept_result);
    Roar.Callback<Roar.WebObjects.Friends.AcceptResponse> cb = (Roar.CallbackInfo<Roar.WebObjects.Friends.AcceptResponse> info) => {
      Assert.IsNotNull(info.data);
    };
    friends.AcceptFriendInvite("123", "456", cb);
    
    accept_result = "<roar tick='123'><friends><accept status='error'><error type='0'></error></accept></friends></roar>";
    requestSender.addMockResponse("friends/accept", accept_result);
    cb = (Roar.CallbackInfo<Roar.WebObjects.Friends.AcceptResponse> info) => {
      Assert.IsNull(info.data);
    };
    friends.AcceptFriendInvite("123", "456", cb);
  }
  
  [Test]
  public void TestDeclineFriend()
  {
    string decline_result = "<roar tick='123'><friends><decline status='ok'/></friends></roar>";
    requestSender.addMockResponse("friends/decline", decline_result);
    Roar.Callback<Roar.WebObjects.Friends.DeclineResponse> cb = (Roar.CallbackInfo<Roar.WebObjects.Friends.DeclineResponse> info) => {
      Assert.IsNotNull(info.data);
    };
    friends.DeclineFriendInvite("123", cb);
    
    decline_result = "<roar tick='123'><friends><decline status='error'><error/></decline></friends></roar>";
    requestSender.addMockResponse("friends/decline", decline_result);
    cb = (Roar.CallbackInfo<Roar.WebObjects.Friends.DeclineResponse> info) => {
      Assert.IsNull(info.data);
    };
    friends.DeclineFriendInvite("123", cb);
  }
  
  [Test]
  public void TestInviteFriend()
  {
    string invite_result = "<roar tick='123'><friends><invite status='ok'/></friends></roar>";
    requestSender.addMockResponse("friends/invite", invite_result);
    Roar.Callback<Roar.WebObjects.Friends.InviteResponse> cb = (Roar.CallbackInfo<Roar.WebObjects.Friends.InviteResponse> info) => {
      Assert.IsNotNull(info.data);
    };
    friends.InviteFriend("123", "234", cb);
    
    invite_result = "<roar tick='123'><friends><invite status='error'><error/></invite></friends></roar>";
    requestSender.addMockResponse("friends/invite", invite_result);
    cb = (Roar.CallbackInfo<Roar.WebObjects.Friends.InviteResponse> info) => {
      Assert.IsNull(info.data);
    };
    friends.InviteFriend("123", "234", cb);
  }
  
  [Test]
  public void TestRemoveFriend()
  {
    string remove_result = "<roar tick='123'><friends><remove status='ok'/></friends></roar>";
    requestSender.addMockResponse("friends/remove", remove_result);
    Roar.Callback<Roar.WebObjects.Friends.RemoveResponse> cb = (Roar.CallbackInfo<Roar.WebObjects.Friends.RemoveResponse> info) => {
      Assert.IsNotNull(info.data);
    };
    friends.RemoveFriend("123", "345", cb);
    
    remove_result = "<roar tick='123'><friends><remove status='error'><error/></remove></friends></roar>";
    requestSender.addMockResponse("friends/remove", remove_result);
    cb = (Roar.CallbackInfo<Roar.WebObjects.Friends.RemoveResponse> info) => {
      Assert.IsNull(info.data);
    };
    friends.RemoveFriend("123", "234", cb);
  }
  
  [Test]
  public void TestListFriends()
  {
    string friend_list =
    @"<roar tick='123' status='ok'>
      <friends>
        <list status='ok'>
          <friend>
            <player_id>123</player_id>
            <name>Brenda Lear</name>
            <level>9</level>
          </friend>
          <friend>
            <player_id>456</player_id>
            <name>Paul Barley</name>
            <level>7</level>
          </friend>
        </list>
      </friends>
    </roar>";
    requestSender.addMockResponse("friends/list", friend_list);
    bool callback_executed = false;
    Roar.Callback< IDictionary<string,Roar.DomainObjects.Friend> > cb = (Roar.CallbackInfo< IDictionary<string,Roar.DomainObjects.Friend> > cb_info) => {
      callback_executed = true;
      Assert.AreEqual(cb_info.code, IWebAPI.OK);
      Assert.IsNotNull(cb_info.data);
      Assert.AreEqual (2, cb_info.data.Count);
    };

    friends.Fetch(cb);

    IList<Roar.DomainObjects.Friend> friends_list = friends.List();
    Assert.AreEqual (2, friends_list.Count);
    Assert.IsTrue(callback_executed);
  }
  
  [Test]
  public void TestInviteInfo()
  {
    string invite_info =
    @"<roar tick='123'>
      <friends>
        <info>
          <message value='Hello World'/>
          <from player_id='234' name='Brenda Lear' level='9' friend_invite_row_id='23452673456'/>
        </info>
      </friends>
    </roar>";
    requestSender.addMockResponse("friends/info", invite_info);
    bool callback_executed = false;
    Roar.Callback<Roar.WebObjects.Friends.Invite_infoResponse> cb = (Roar.CallbackInfo<Roar.WebObjects.Friends.Invite_infoResponse> info) => {
      callback_executed = true;
      Roar.WebObjects.Friends.Invite_infoResponse friend_info = info.data;
       Assert.AreEqual("234", friend_info.info.player_id);
       Assert.AreEqual("Brenda Lear", friend_info.info.name);
       Assert.AreEqual(9, friend_info.info.level);
       Assert.AreEqual("Hello World", friend_info.info.message);
    };
    friends.FriendInviteInfo("123", cb);
    Assert.IsTrue(callback_executed);
  }
  
  [Test]
  public void TestListInvites()
  {
    string invite_list =
    @"<roar tick='123' status='ok'>
      <friends>
        <list_invites status='ok'>
          <friend_invite invite_id='123'>
            <player_id>234</player_id>
          </friend_invite>
          <friend_invite invite_id='456'>
            <player_id>567</player_id>
          </friend_invite>
        </list_invites>
      </friends>
    </roar>";
    requestSender.addMockResponse("friends/list_invites", invite_list);
    bool callback_executed = false;
    Roar.Callback<Roar.WebObjects.Friends.List_invitesResponse> cb = (Roar.CallbackInfo<Roar.WebObjects.Friends.List_invitesResponse> cb_info) => {
      callback_executed = true;
      Assert.AreEqual(cb_info.code, IWebAPI.OK);
      Assert.IsNotNull(cb_info.data);
      Assert.AreEqual(cb_info.data.invites.Count, 2);
    };
    friends.ListFriendInvites(cb);
    Assert.IsTrue(callback_executed);
  }
  
}
