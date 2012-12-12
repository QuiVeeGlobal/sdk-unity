using System;
using NUnit.Framework;
using NMock2;
using System.Collections;
using System.Collections.Generic;
using Roar.WebObjects.Friends;


namespace Testing
{
    [TestFixture()]
    class XMLToFriendsTests
    {
        [Test()]
        public void TestParseFacebookBindSignedResponse()
        {
            string xml =
            @"<roar tick=""135510457230"">
				<facebook>
					<bind_signed status=""ok""/>
				</facebook>
			</roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml);

            Roar.WebObjects.Facebook.BindSignedResponse response = (new Roar.DataConversion.Responses.Facebook.BindSigned()).Build(nn);

            Assert.IsNotNull(response);

        }

        [Test()]
        public void TestFriendsAccept()
        {
            string xml =
            @"<roar tick=""135510457230"">
				<friends>
					<accept status=""ok""/>
				</friends>
			</roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml);

            Roar.WebObjects.Friends.AcceptResponse response = new Roar.DataConversion.Responses.Friends.Accept().Build(nn);

            Assert.IsNotNull(response);
        }

        [Test()]
        public void TestFriendsDecline()
        {
            string xml =
            @"<roar tick=""135510457230"">
				<friends>
					<decline status=""ok""/>
				</friends>
			</roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml);

            Roar.WebObjects.Friends.DeclineResponse response = new Roar.DataConversion.Responses.Friends.Decline().Build(nn);

            Assert.IsNotNull(response);
        }

        [Test()]
        public void TestFriendsInvite()
        {
            string xml =
            @"<roar tick=""135510457230"">
				<friends>
					<invite status=""ok""/>
				</friends>
			</roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml);

            Roar.WebObjects.Friends.InviteResponse response = new Roar.DataConversion.Responses.Friends.Invite().Build(nn);

            Assert.IsNotNull(response);
        }

        [Test()]
        public void TestFriendsInfo()
        {
            string xml =
            @"<roar tick=""135510457230"">
				<friends>
					<info>
                        <from player_id=""12345"" name=""asdf"" level=""9000"">
                        <message value=""message test"">
					</info>
				</friends>
			</roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml);
            
            Roar.WebObjects.Friends.InviteInfoResponse response = new Roar.DataConversion.Responses.Friends.InviteInfo().Build(nn);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.info.player_id, "12345");
            Assert.AreEqual(response.info.name, "asdf");
            Assert.AreEqual(response.info.level, 9000);
            Assert.AreEqual(response.info.message, "message test");
            //confirm the exact xml response.
        }

        [Test()]
        public void TestFriendsList()
        {
            string xml =
            @"<roar tick=""130868353269"">
              <friends>
                <list status=""ok"">
                  <friend>
                    <row_id/>
                    <player_id>19000494933</player_id>
                    <name>player_username</name>
                    <level>5</level>
                  </friend>
                  <friend>
                    <row_id/>
                    <player_id>90210</player_id>
                    <name>leroy</name>
                    <level>80</level>
                  </friend>
                </list>
              </friends>
            </roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml);

            Roar.WebObjects.Friends.ListResponse response = new Roar.DataConversion.Responses.Friends.List().Build(nn);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.friends.Count, 2);
            Assert.AreEqual(response.friends[0].name, "player_username");
            Assert.AreEqual(response.friends[1].name, "leroy");
            Assert.AreEqual(response.friends[1].level, "80");
            Assert.AreEqual(response.friends[0].level, "5");
            Assert.AreEqual(response.friends[0].player_id, "19000494933");
            Assert.AreEqual(response.friends[1].player_id, "90210");

            //confirm the exact xml response.
        }

        [Test()]
        public void TestFriendsRemove()
        {
            string xml =
            @"<roar tick=""135510457230"">
				<friends>
					<remove status=""ok""/>
				</friends>
			</roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml);

            Roar.WebObjects.Friends.RemoveResponse response = new Roar.DataConversion.Responses.Friends.Remove().Build(nn);

            Assert.IsNotNull(response);
        }

        [Test()]
        public void TestFriendsListInvites()
        {
            string xml =
            @"<roar tick=""135510457230"">
				<friends>
					<list_invites status=""ok"">
                      <friend_invite invite_id=""123"">
                        <player_id>19000494933</player_id>
                      </friend_invite>
                      <friend_invite invite_id=""456"">
                        <player_id>90210</player_id>
                      </friend_invite>
                    </list_invites>
				</friends>
			</roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml);

            Roar.WebObjects.Friends.ListInvitesResponse response = new Roar.DataConversion.Responses.Friends.ListInvites().Build(nn);
            
            Assert.IsNotNull(response);

            Assert.AreEqual(response.invites.Count, 2);

            Assert.AreEqual(response.invites[0].invite_id, "123");
            Assert.AreEqual(response.invites[1].invite_id, "456");
            Assert.AreEqual(response.invites[0].player_id, "19000494933");
            Assert.AreEqual(response.invites[1].player_id, "90210");
        }

    }
}
