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
	}
}

