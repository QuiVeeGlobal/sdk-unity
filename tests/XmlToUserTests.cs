using System;
using NUnit.Framework;
using Roar.WebObjects.User;

namespace Testing
{
	[TestFixture]
	public class XmlToUserTests
	{
	
		[Test()]
		public void TestUserCreateXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<user>
					<create status=""ok""/>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.Create create_parser = new Roar.DataConversion.Responses.User.Create();
			CreateResponse response = create_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestUserChangePasswordXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""135589197910"">
				<user>
					<change_password status=""ok""/>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.ChangePassword change_password_parser = new Roar.DataConversion.Responses.User.ChangePassword();
			ChangePasswordResponse response = change_password_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestUserLoginXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455461333"">
				<user>
					<login status=""ok"">
						<auth_token>17248630753098207878</auth_token>
						<player_id>635902077904630318</player_id>
					</login>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.Login login_parser = new Roar.DataConversion.Responses.User.Login();
			LoginResponse response = login_parser.Build(nn);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.auth_token, "17248630753098207878");
			Assert.AreEqual(response.player_id, "635902077904630318");
		}
		
		[Test()]
		public void TestUserChangeNameXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455461333"">
				<user>
					<change_password status=""ok""/>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.ChangeName change_name_parser = new Roar.DataConversion.Responses.User.ChangeName();
			ChangeNameResponse response = change_name_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestUserNetdriveSetXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""135589359998"">
				<user>
					<netdrive_set status=""ok""/>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.NetdriveSave netdrive_save_parser = new Roar.DataConversion.Responses.User.NetdriveSave();
			NetdriveSaveResponse response = netdrive_save_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestUserNetDriveGetXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""135589388345"">
				<user>
					<netdrive_get status=""ok"">
						<netdrive_field ikey=""sonda"">
							<data>mariner</data>
						</netdrive_field>
					</netdrive_get>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.NetdriveFetch netdrive_fetch_parser = new Roar.DataConversion.Responses.User.NetdriveFetch();
			NetdriveFetchResponse response = netdrive_fetch_parser.Build(nn);
			Assert.AreEqual(response.ikey, "sonda");
			Assert.AreEqual(response.data, "mariner");
		}
		
		[Test()]
		public void TestUserLogoutXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""135589507667"">
				<user>
					<logout status=""ok""/>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.Logout logout_parser = new Roar.DataConversion.Responses.User.Logout();
			LogoutResponse response = logout_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestUserViewXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""135595331857"">
				<user>
					<view status=""ok"">
						<attribute type=""special"" ikey=""id"" value=""635902077904630318""/>
						<attribute type=""special"" ikey=""xp"" value=""16807"" level_start=""5"" next_level=""7""/>
						<attribute ikey=""energy"" label=""Energy"" value=""10"" type=""resource"" min=""2"" max=""10"" regen_amount=""1"" regen_every=""18000""/>
						<attribute name=""status"" value=""No current status update available"" type=""custom""/>
					<regen_scripts/>
					</view>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.View view_parser = new Roar.DataConversion.Responses.User.View();
			ViewResponse response = view_parser.Build(nn);
			
			Assert.IsNotNull(response.attributes);
			Assert.AreEqual(response.attributes.Count, 4);
			Assert.AreEqual(response.attributes[0].type, "special");
			Assert.AreEqual(response.attributes[0].ikey, "id");
			Assert.AreEqual(response.attributes[0].value, "635902077904630318");
			Assert.AreEqual(response.attributes[1].level_start, "5");
			Assert.AreEqual(response.attributes[1].next_level, "7");
			Assert.AreEqual(response.attributes[2].label, "Energy");
			Assert.AreEqual(response.attributes[2].min, "2");
			Assert.AreEqual(response.attributes[2].max, "10");
			Assert.AreEqual(response.attributes[2].regen_amount, "1");
			Assert.AreEqual(response.attributes[2].regen_every, "18000");
			Assert.AreEqual(response.attributes[3].name, "status");
		}
		
		[Test()]
		public void TestUserSetXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""125554966267"">
				<user>
					<set status=""ok""/>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.Set set_parser = new Roar.DataConversion.Responses.User.Set();
			SetResponse response = set_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestUserAchievementsXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128755501434"">
				<user>
					<achievements status=""ok"">
						<achievement>
							<ikey>the_big_one</ikey>
							<status>active</status>
							<label>The Big One</label>
							<progress>0/3</progress>
							<description>Find the dragon three times!</description>
						</achievement>
						<achievement>
							<ikey>the_small_one</ikey>
							<status>completed</status>
							<label>The Small One</label>
							<progress>2/2</progress>
							<description>Find the jewel two times!</description>
						</achievement>
					</achievements>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.Achievements achievements_parser = new Roar.DataConversion.Responses.User.Achievements();
			AchievementsResponse response = achievements_parser.Build(nn);
			
			Assert.IsNotNull(response.achievements);
			Assert.AreEqual(response.achievements.Count, 2);
			
			Assert.AreEqual(response.achievements[0].ikey, "the_big_one");
			Assert.AreEqual(response.achievements[0].status, "active");
			Assert.AreEqual(response.achievements[0].label, "The Big One");
			Assert.AreEqual(response.achievements[0].progress, "0/3");
			Assert.AreEqual(response.achievements[0].description, "Find the dragon three times!");
			
			Assert.AreEqual(response.achievements[1].ikey, "the_small_one");
			Assert.AreEqual(response.achievements[1].status, "completed");
			Assert.AreEqual(response.achievements[1].label, "The Small One");
			Assert.AreEqual(response.achievements[1].progress, "2/2");
			Assert.AreEqual(response.achievements[1].description, "Find the jewel two times!");
		}
		
		[Test()]
		public void TestPrivateSetXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455475133"">
				<user>
					<private_set status=""ok"" />
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.PrivateSet private_set_parser = new Roar.DataConversion.Responses.User.PrivateSet();
			PrivateSetResponse response = private_set_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestPrivateGetXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455475133"">
				<user>
					<private_get status=""ok"">
						<private_field ikey=""gold"" data=""5 pieces""/>
					</private_get>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.PrivateGet private_get_parser = new Roar.DataConversion.Responses.User.PrivateGet();
			PrivateGetResponse response = private_get_parser.Build(nn);
			Assert.AreEqual(response.ikey, "gold");
			Assert.AreEqual(response.data, "5 pieces");
		}
	}
}

