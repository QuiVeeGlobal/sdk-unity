
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;
using NMock2;

using Roar.implementation.Components;
using Roar.Components;

[TestFixture]
public class WebAPIFriendsTests : ComponentTests
{

  private Mockery mockery = null;
  protected IFriends friends = null;
  
  [SetUp]
  new public void TestInitialise()
  {
    base.TestInitialise();
    mockery = new Mockery();
    friends = roar.Friends;
    Assert.IsNotNull(friends);
    Assert.IsFalse(friends.HasDataFromServer);
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
    Roar.Callback cb = (Roar.CallbackInfo info) => {
      Assert.IsNotNull(info.d);
    };
    friends.AcceptFriendInvite("123", "456", cb);
    
    accept_result = "<roar tick='123'><friends><accept status='error'><error type='0'></error></accept></friends></roar>";
    requestSender.addMockResponse("friends/accept", accept_result);
    cb = (Roar.CallbackInfo info) => {
      Assert.IsNull(info.d);
    };
    friends.AcceptFriendInvite("123", "456", cb);
  }
  
  [Test]
  public void TestDeclineFriend()
  {
    string decline_result = "<roar tick='123'><friends><decline status='ok'/></friends></roar>";
    requestSender.addMockResponse("friends/decline", decline_result);
    Roar.Callback cb = (Roar.CallbackInfo info) => {
      Assert.IsNotNull(info.d);
    };
    friends.DeclineFriendInvite("123", cb);
    
    decline_result = "<roar tick='123'><friends><decline status='error'><error/></decline></friends></roar>";
    requestSender.addMockResponse("friends/decline", decline_result);
    cb = (Roar.CallbackInfo info) => {
      Assert.IsNull(info.d);
    };
    friends.DeclineFriendInvite("123", cb);
  }
  
  [Test]
  public void TestInviteFriend()
  {
    string invite_result = "<roar tick='123'><friends><invite status='ok'/></friends></roar>";
    requestSender.addMockResponse("friends/invite", invite_result);
    Roar.Callback cb = (Roar.CallbackInfo info) => {
      Assert.IsNotNull(info.d);
    };
    friends.InviteFriend("123", "234", cb);
    
    invite_result = "<roar tick='123'><friends><invite status='error'><error/></invite></friends></roar>";
    requestSender.addMockResponse("friends/invite", invite_result);
    cb = (Roar.CallbackInfo info) => {
      Assert.IsNull(info.d);
    };
    friends.InviteFriend("123", "234", cb);
  }
  
  [Test]
  public void TestRemoveFriend()
  {
    string remove_result = "<roar tick='123'><friends><remove status='ok'/></friends></roar>";
    requestSender.addMockResponse("friends/remove", remove_result);
    Roar.Callback cb = (Roar.CallbackInfo info) => {
      Assert.IsNotNull(info.d);
    };
    friends.RemoveFriend("123", "345", cb);
    
    remove_result = "<roar tick='123'><friends><remove status='error'><error/></remove></friends></roar>";
    requestSender.addMockResponse("friends/remove", remove_result);
    cb = (Roar.CallbackInfo info) => {
      Assert.IsNull(info.d);
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
    int friend_list_count = 2;
    Assert.IsFalse(friends.HasDataFromServer);
    requestSender.addMockResponse("friends/list", friend_list);
    bool callback_executed = false;
    IList<Roar.DomainObjects.Friend> list_of_friends = null;
    Roar.Callback cb = (Roar.CallbackInfo cb_info) => {
      callback_executed = true;
      Assert.AreEqual(cb_info.code, IWebAPI.OK);
      Assert.IsNotNull(cb_info.d);
      list_of_friends = cb_info.d as IList<Roar.DomainObjects.Friend>;
    };
    friends.ListFriends(cb);
    Assert.IsTrue(friends.HasDataFromServer);
    Assert.IsTrue(callback_executed);
    Assert.AreEqual(list_of_friends.Count, friend_list_count);
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
    Assert.IsFalse(friends.HasDataFromServer);
    requestSender.addMockResponse("friends/info", invite_info);
    bool callback_executed = false;
    Roar.DomainObjects.FriendInviteInfo friend_info = null;
    Roar.Callback cb = (Roar.CallbackInfo info) => {
      callback_executed = true;
      friend_info = info.d as Roar.DomainObjects.FriendInviteInfo;
    };
    friends.FriendInviteInfo("123", cb);
    Assert.IsTrue(friends.HasDataFromServer);
    Assert.IsTrue(callback_executed);
    Assert.AreEqual(friend_info.player_id, "234");
    Assert.AreEqual(friend_info.name, "Brenda Lear");
    Assert.AreEqual(friend_info.level, 9);
    Assert.AreEqual(friend_info.friend_invite_row_id, "23452673456");
    Assert.AreEqual(friend_info.message, "Hello World");
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
    int invite_list_count = 2;
    Assert.IsFalse(friends.HasDataFromServer);
    requestSender.addMockResponse("friends/list_invites", invite_list);
    bool callback_executed = false;
    IList<Roar.DomainObjects.FriendInvite> friend_invites = null;
    Roar.Callback cb = (Roar.CallbackInfo cb_info) => {
      callback_executed = true;
      Assert.AreEqual(cb_info.code, IWebAPI.OK);
      Assert.IsNotNull(cb_info.d);
      friend_invites = cb_info.d as IList<Roar.DomainObjects.FriendInvite>;
    };
    friends.ListFriendInvites(cb);
    Assert.IsTrue(friends.HasDataFromServer);
    Assert.IsTrue(callback_executed);
    Assert.AreEqual(friend_invites.Count, invite_list_count);
  }
  
}
