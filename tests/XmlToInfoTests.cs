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
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml).GetFirstChild("roar");
			
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
						<attribute ikey=""energy"" label=""Energy"" value=""10"" type=""resource"" min=""2"" max=""10"" regen_amount=""1"" regen_every=""18000""/>
						<attribute name=""status"" value=""No current status update available"" type=""custom""/>
					</user>
				</info>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml).GetFirstChild("roar");
			
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
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml).GetFirstChild("roar");
			
			PollResponse poll_response = (new Roar.DataConversion.Responses.Info.Poll()).Build(nn);
			
			Assert.IsNotNull( poll_response );
			
		}

	}
	
}

