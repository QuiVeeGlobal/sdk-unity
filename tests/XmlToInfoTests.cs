using System;
using NUnit.Framework;
using Roar.WebObjects.Info;

namespace Testing
{

	[TestFixture()]
	public class XmlToInfoTests
	{
	
		[SetUp]
		public void TestInitialise ()
		{
		}
		
		[Test()]
		public void TestGet_bulk_playerInfoXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""130081353655"">
				<info>
					<get_bulk_player_info status=""ok"">
						<player_info id=""748994158"">
							<stats>
								<stat ikey=""health"" value=""100""/>
								<stat ikey=""cash"" value=""10""/>
							</stats>
							<properties>
								<property ikey=""player_bio"" value=""Scary hobo dude""/>
							</properties>
						</player_info>
						<player_info id=""1706065893"">
							<stats>
								<stat ikey=""health"" value=""80""/>
								<stat ikey=""cash"" value=""100""/>
							</stats>
							<properties>
								<property ikey=""player_bio"" value=""Some punk""/>
							</properties>
						</player_info>
					</get_bulk_player_info>
				</info>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			
			GetBulkPlayerInfoResponse response = (new Roar.DataConversion.Responses.Info.GetBulkPlayerInfo()).Build(nn);
			
			Assert.IsNotNull(response.players);
			Assert.AreEqual(response.players.Count, 2);
			Assert.AreEqual(response.players["748994158"].stats["health"], "100");
			Assert.AreEqual(response.players["748994158"].stats["cash"], "10");
			Assert.AreEqual(response.players["748994158"].properties["player_bio"], "Scary hobo dude");
			Assert.AreEqual(response.players["1706065893"].stats["health"], "80");
			Assert.AreEqual(response.players["1706065893"].stats["cash"], "100");
			Assert.AreEqual(response.players["1706065893"].properties["player_bio"], "Some punk");
		}
		
		[Test()]
		public void TestPingInfoXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455529480"">
				<info>
					<ping status=""ok"">
						<text>hello</text>
					</ping>
				</info>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			
			PingResponse ping_response = (new Roar.DataConversion.Responses.Info.Ping()).Build(nn);
			Assert.AreEqual(ping_response.text, "hello");			
		}
	
		[Test()]
		public void TestUserInfoXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""135509459014"">
				<info>
					<user status=""ok"">
						<attribute type=""special"" ikey=""id"" value=""10857066722104040268""/>
						<attribute type=""special"" ikey=""xp"" value=""8"" level_start=""4"" next_level=""20""/>
						<attribute type=""special"" ikey=""level"" value=""7""/>
						<attribute type=""special"" ikey=""name"" value=""Brenda Lear""/>
						<attribute ikey=""aud"" label=""AUD"" value=""100"" type=""currency"" min=""0""/>
						<attribute ikey=""energy"" label=""Energy"" value=""10"" type=""resource"" min=""2"" max=""10"" regen_amount=""1"" regen_every=""18000"" next_regen=""135595635357""/>
						<attribute name=""status"" value=""No current status update available"" type=""custom""/>
					</user>
				</info>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			
			UserResponse user_response = (new Roar.DataConversion.Responses.Info.User()).Build(nn);
			
			Assert.IsNotNull(user_response.player);
			Assert.AreEqual(user_response.player.id, "10857066722104040268");
			Assert.AreEqual(user_response.player.name, "Brenda Lear");
			Assert.AreEqual(user_response.player.level, 7);
			Assert.AreEqual(user_response.player.xp.value, 8);
			Assert.AreEqual(user_response.player.xp.next_level, 20);
			Assert.AreEqual(user_response.player.xp.level_start, 4);
			Assert.AreEqual(user_response.player.attributes["aud"].ikey, "aud");
			Assert.AreEqual(user_response.player.attributes["aud"].label, "AUD");
			Assert.AreEqual(user_response.player.attributes["aud"].value, "100");
			Assert.AreEqual(user_response.player.attributes["aud"].type, "currency");
			Assert.AreEqual(user_response.player.attributes["aud"].min, "0");
			Assert.AreEqual(user_response.player.attributes["energy"].ikey, "energy");
			Assert.AreEqual(user_response.player.attributes["energy"].label, "Energy");
			Assert.AreEqual(user_response.player.attributes["energy"].value, "10");
			Assert.AreEqual(user_response.player.attributes["energy"].type, "resource");
			Assert.AreEqual(user_response.player.attributes["energy"].min, "2");
			Assert.AreEqual(user_response.player.attributes["energy"].max, "10");
			Assert.AreEqual(user_response.player.attributes["energy"].regen_amount, "1");
			Assert.AreEqual(user_response.player.attributes["energy"].regen_every, "18000");
			Assert.AreEqual(user_response.player.attributes["energy"].next_regen, "135595635357");
			Assert.AreEqual(user_response.player.attributes["status"].name, "status");
			Assert.AreEqual(user_response.player.attributes["status"].type, "custom");
			Assert.AreEqual(user_response.player.attributes["status"].value, "No current status update available");
		}
		
		[Test()]
		public void TestPollInfoXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""135510457230"">
				<info>
					<poll status=""ok""/>
				</info>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			
			PollResponse poll_response = (new Roar.DataConversion.Responses.Info.Poll()).Build(nn);
			
			Assert.IsNotNull( poll_response );
			
		}

	}
	
}

