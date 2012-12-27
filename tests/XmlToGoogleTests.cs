using System;
using NUnit.Framework;
using Roar.WebObjects.Google;

namespace Testing
{
	[TestFixture]
	public class XmlToGoogleTests
	{
		[Test()]
		public void TestGoogleBindUserXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<bind_user status=""ok""/>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.BindUser bind_user_parser = new Roar.DataConversion.Responses.Google.BindUser();
			BindUserResponse response = bind_user_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestGoogleBindUserTokenXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<bind_user status=""ok""/>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.BindUserToken bind_user_token_parser = new Roar.DataConversion.Responses.Google.BindUserToken();
			BindUserTokenResponse response = bind_user_token_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestGoogleCreateUserXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<create_user status=""ok"">
						<auth_token>ABCDEF</auth_token>
						<player_id>1231231</player_id>
					</create_user>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.CreateUser create_user_parser = new Roar.DataConversion.Responses.Google.CreateUser();
			CreateUserResponse response = create_user_parser.Build(nn);
			
			Assert.AreEqual(response.auth_token, "ABCDEF");
			Assert.AreEqual(response.player_id, "1231231");
		}
		
		[Test()]
		public void TestGoogleCreateUserTokenXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<create_user status=""ok"">
						<auth_token>ABCDEF</auth_token>
						<player_id>1231231</player_id>
					</create_user>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.CreateUserToken create_user_token_parser = new Roar.DataConversion.Responses.Google.CreateUserToken();
			CreateUserTokenResponse response = create_user_token_parser.Build(nn);
			
			Assert.AreEqual(response.auth_token, "ABCDEF");
			Assert.AreEqual(response.player_id, "1231231");
		}
	}
}

