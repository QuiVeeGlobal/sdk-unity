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
        [SetUp]
        public void TestInitialise()
        {
        }

        [Test()]
        public void TestFriendsAccept()
        {
            string xml = "<friend>\n" +
                "  <player_id>ABCDEF</player_id>\n" +
                "  <name>some dude</name>\n" +
                "  <level>7</level>" +
                "</friend>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml).GetFirstChild("friend");

            Roar.DomainObjects.Friend friend = Roar.DomainObjects.Friend.CreateFromXml(nn);

            Assert.AreEqual("ABCDEF", friend.player_id);
            Assert.AreEqual("some dude", friend.name);
            Assert.AreEqual(7, friend.level);
        }

        [Test()]
        public void TestFriendsInvite()
        {
            string xml = "<roar tick=\"130868381316\">"+
                          "<friends>"+
                            "<invite status=\"ok\">"+
                              "<invite_id id=\"1138654978\"/>"+
                            "</invite>"+
                          "</friends>"+
                        "</roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml).GetFirstChild("friends");

            Roar.DomainObjects.Friend friend = Roar.DomainObjects.Friend.CreateFromXml(nn);

            Assert.AreEqual("ABCDEF", friend.player_id);
            Assert.AreEqual("some dude", friend.name);
            Assert.AreEqual(7, friend.level);
        }

    }
}
